/*
 * @lc app=leetcode.cn id=35 lang=csharp
 *
 * [35] 搜索插入位置
 */

// @lc code=start
public class Solution {
    public int SearchInsert(int[] nums, int target) {
        return BinarySearch(nums, target,0,nums.Length-1);
    }

      public int BinarySearch(int[] nums, int target,int left,int right)
    {
        if(target>nums[nums.Length-1])
        {
            return nums.Length;
        }


        if(nums[left]==target)
        {
            return left;
        }


        if(left==right)
        {
            return left;
        }

        int mid=(left+right)/2;

        return target>nums[mid]?BinarySearch(nums,target,mid+1,right): BinarySearch(nums,target,left,mid);
    }
}
// @lc code=end

