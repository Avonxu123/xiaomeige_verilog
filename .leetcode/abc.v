
module usart_rx1 (
    Clk,
    Reset_n,
    Baud_set,
    usart_rx,
    Data,
    Rx_Done
);
  input Clk;
  input Reset_n;
  input [2:0]Baud_set;
  input usart_rx;
  output reg [7:0] Data;
  output reg Rx_Done;


  //两个D触发器 边沿检测
  reg [1:0] usart_rx_r;
  always @(posedge Clk) begin
    usart_rx_r[0] <= usart_rx;
    usart_rx_r[1] <= usart_rx_r[0];
  end
  wire pedge_usart_tx;  //上升沿检测 先0后1
  //assign pedge_usart_tx=(usart_rx_r[1]==0)&&(usart_rx_r[0]==1);
  //前一种时刻的值为0 后一种时刻的值为1 
  assign pedge_usart_tx = (usart_rx_r == 2'b01);  //这样写也是一样的  省事

  wire nedge_usart_tx;  //下降沿检测 先1后0
  //assign nedge_usart_tx=(usart_rx_r[1]==0)&&(usart_rx_r[0]==1);
  //前一种时刻的值为1 后一种时刻的值为0 
  assign nedge_usart_tx = (usart_rx_r == 2'b10);  //这样写也是一样的  省事

  //接收 一位检测16次（一般情况下） 那么就是
  //1000000000/115200/16/20 =27次
  //1000000000/9600/16/20=325次
  reg [8:0] bps_DR;
  always @(*)
    case (Baud_set)
      0: bps_DR = 1000000000 / 9600 / 16 / 20;
      1: bps_DR = 1000000000 / 19200 / 16 / 20;
      2: bps_DR = 1000000000 / 38400 / 16 / 20;
      3: bps_DR = 1000000000 / 57600 / 16 / 20;
      4: bps_DR = 1000000000 / 115200 / 16 / 20;
      default: bps_DR = 1000000000 / 9600 / 16 / 20;
    endcase

  wire bps_clk_16x;//主时钟记到那么多次以后  那么就可以进行采样的标志位
  assign bps_clk_16x = (div_cnt == (bps_DR / 2));  //记到一半的时候 采样


  reg Rx_en;
  always @(posedge Clk or negedge Reset_n)
    if (!Reset_n) Rx_en <= 0;
    else if (nedge_usart_tx) Rx_en <= 1;
    else if (Rx_Done || (sta_data >= 4))  //干扰 或者发送完成
      Rx_en <= 0;
  
  reg [8:0] div_cnt;//主时钟 记到27 也就是那么多个脉冲 采一次数
  always @(posedge Clk or negedge Reset_n)
    if (!Reset_n) div_cnt <= 0;
    else if (Rx_en) begin
      //这里不能直接写成else if(nedge_usart_tx)  因为这个玩意就只有一个时钟
      if (div_cnt == bps_DR) div_cnt <= 0;
      else div_cnt <= div_cnt + 1'b1;
    end else div_cnt <= 0;


  reg [7:0] bps_cnt;
  always @(posedge Clk or negedge Reset_n)
    if (!Reset_n) 
        bps_cnt <= 0;
    else if (Rx_en) begin
      if (bps_clk_16x) begin
        if (bps_cnt == 159) 
            bps_cnt <= 0;//这里的159就是 160个脉冲嘛 每个采样16次
        else 
            bps_cnt <= bps_cnt + 1'b1;
      end else 
        bps_cnt <= bps_cnt;
    end else 
        bps_cnt <= 0;
  //二维数组
  //reg[位宽-1：0]  寄存器名字 [数组宽度-1:0] 其实这也就是一个一维数组 只能一个一个
  reg [2:0] r_data[7:0];
  reg [2:0] sta_data;
  reg [2:0] sto_data;
  always @(posedge Clk or negedge Reset_n)
    if (!Reset_n) begin
      sta_data  <= 0;
      sto_data  <= 0;
      r_data[0] <= 0;
      r_data[1] <= 0;
      r_data[2] <= 0;
      r_data[3] <= 0;
      r_data[4] <= 0;
      r_data[5] <= 0;
      r_data[6] <= 0;
      r_data[7] <= 0;
    end else if (bps_clk_16x) begin
      case (bps_cnt)
        0:begin//这里要去清0 因为如果不清0就会一直加了
        sta_data  <= 0;
        sto_data  <= 0;
        r_data[0] <= 0;
        r_data[1] <= 0;
        r_data[2] <= 0;
        r_data[3] <= 0;
        r_data[4] <= 0;
        r_data[5] <= 0;
        r_data[6] <= 0;
        r_data[7] <= 0;
        end
        5, 6, 7, 8, 9, 10, 11: sta_data <= sta_data + usart_rx;
        21, 22, 23, 24, 25, 26, 27: r_data[0] <= r_data[0] + usart_rx;
        37, 38, 39, 40, 41, 42, 43: r_data[1] <= r_data[1] + usart_rx;
        53, 54, 55, 56, 57, 58, 59: r_data[2] <= r_data[2] + usart_rx;
        69, 70, 71, 72, 73, 74, 75: r_data[3] <= r_data[3] + usart_rx;
        85, 86, 87, 88, 87, 90, 91: r_data[4] <= r_data[4] + usart_rx;
        101, 102, 103, 104, 105, 106, 107: r_data[5] <= r_data[5] + usart_rx;
        117, 118, 119, 120, 121, 122, 123: r_data[6] <= r_data[6] + usart_rx;
        133, 134, 135, 136, 137, 138, 139: r_data[7] <= r_data[7] + usart_rx;
        149, 150, 151, 152, 153, 154, 155: sto_data <= sto_data + usart_rx;
        default: ;
      endcase
    end
  always @(posedge Clk or negedge Reset_n)
    if (!Reset_n) 
      Data <= 0;
    else if(bps_clk_16x && (bps_cnt==159))begin
      Data[0] <= (r_data[0]>=4)? 1'b1: 1'b0;
      Data[1] <= (r_data[1]>=4)? 1'b1: 1'b0;
      Data[2] <= (r_data[2]>=4)? 1'b1: 1'b0;
      Data[3] <= (r_data[3]>=4)? 1'b1: 1'b0;
      Data[4] <= (r_data[4]>=4)? 1'b1: 1'b0;
      Data[5] <= (r_data[5]>=4)? 1'b1: 1'b0;
      Data[6] <= (r_data[6]>=4)? 1'b1: 1'b0;
      Data[7] <= (r_data[7]>=4)? 1'b1: 1'b0;
    end
  always @(posedge Clk or negedge Reset_n)
    if (!Reset_n) 
      Rx_Done <= 0;
    else if(bps_clk_16x && (bps_cnt==159))
      Rx_Done <= 1;
    else
      Rx_Done <= 0;   

endmodule




`timescale 1ns / 1ns



module usart_rx_tb();


   reg Clk;
   reg Reset_n;
   wire [2:0]Baud_set;
   reg usart_rx;
   wire [7:0]Data; 
   wire Rx_Done;
   
   assign Baud_set = 4;
   
    usart_rx1 usart_rx1(
        Clk,
        Reset_n,
        Baud_set,
        usart_rx,
        Data,
        Rx_Done
    );
  
  initial Clk=1;
  always #10 Clk=~Clk;
  
  initial begin
     Reset_n = 0;
     usart_rx = 1;
     #201;
     Reset_n = 1;
     #200;
     uart_tx_byte(8'h5a);
     @(posedge Rx_Done);
     #5000;
     uart_tx_byte(8'ha5);
     @(posedge Rx_Done);
     #5000;
     uart_tx_byte(8'h86);
     @(posedge Rx_Done);
     #5000;
     $stop;
  end
  
  task uart_tx_byte;//新语法 Task：Task语法用于定义一个可重复使用的硬件任务或操作。
  //它是一种将硬件功能分解为更小的模块并以可重用的方式组合它们的方法。
    input[7:0] tx_byte;
    begin
        usart_rx = 1;
        #20;
        usart_rx = 0;
        #8680;
        usart_rx = tx_byte[0];
        #8680;
        usart_rx = tx_byte[1];
        #8680;
        usart_rx = tx_byte[2];
        #8680;
        usart_rx = tx_byte[3];
        #8680;
        usart_rx = tx_byte[4];
        #8680;
        usart_rx = tx_byte[5];
        #8680;
        usart_rx = tx_byte[6];
        #8680;
        usart_rx = tx_byte[7];
        #8680;
        usart_rx = 1;
        #8680;
    end
  endtask;
endmodule
