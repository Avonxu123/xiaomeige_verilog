
// @lc code=start
public class Solution
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode head = new ListNode(0);
        ListNode cur = head;
        int c = 0;
        while (l1 != null || l2 != null)
        {
            int value = c;
            value += l1 != null ? l1.val : 0;
            value += l2 != null ? l2.val : 0;

            c = value / 10;


            ListNode temp = new ListNode(value % 10);
            cur.next = temp;
            cur = temp;


            if (l1 != null) l1 = l1.next;
            if (l2 != null) l2 = l2.next;

        }

        //如果最后两个数，相加的时候有进位数的时候，就将进位数，赋予链表的新节点。
        //两数相加最多小于20，所以的的值最大只能时1
        if (c != 0) cur.next = new ListNode(c); //这一行 放在循环最后也可以，因为每次都是新建的temp 然后cur.next 指向他 然后把cur过去 等于每次cur.next 都等于空的
        //当然 直至末尾 就不会被下一次顶掉了  放进去其实也是多一步操作  不好

        return head.next;
    }
}
// @lc code=end

