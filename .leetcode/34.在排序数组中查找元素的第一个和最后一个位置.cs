/*
 * @lc app=leetcode.cn id=34 lang=csharp
 *
 * [34] 在排序数组中查找元素的第一个和最后一个位置
 */

// @lc code=start
public class Solution {

    public int[] SearchRange(int[] nums, int target) 
    {
        List<int> a= BinarySearch(nums,0,nums.Length-1,target);
        a.Sort();
        if(a.Count!=0)
        {
            return new int []{a[0],a[a.Count-1]};
        }
        else
        {
            return new int []{-1,-1};
        }
    }
    
   public List<int> BinarySearch(int[] arr, int left, int right, int value)
        {
            
            if (left > right)
            {
                return new List<int>();
            }

            int mid = (left + right) / 2;
            int midValue = arr[mid];


            //向右递归
            if (value > midValue)
            {
                return BinarySearch(arr, mid + 1, right, value);
            }
            else if (value < midValue)
            {
                return BinarySearch(arr, left, mid  -1, value);
            }
            else
            {

                //找到的时候 不要立马返回
                //先向左扫描 找到的结果全部加到List里面
                //然后右扫描  
                List<int> resList = new List<int>();
                //这里的temp的目的就是相当于 暂存一下 要不然等一下就顶掉了
                int temp = mid - 1;

                while (true)
                {
                    if (temp < 0 || arr[temp] != value)
                    {
                        break;
                    }
                    resList.Add(temp);
                    temp--;
                }
                resList.Add(mid);
                temp = mid + 1;
                while (true)
                {
                    if (temp > arr.Length - 1 || arr[temp] != value)
                    {
                        break;
                    }
                    resList.Add(temp);
                    temp++;
                }
                return resList;
            }
        }

}
// @lc code=end

