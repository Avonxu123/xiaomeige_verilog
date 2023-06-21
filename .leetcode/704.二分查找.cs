/*
 * @lc app=leetcode.cn id=704 lang=csharp
 *
 * [704] 二分查找
 */

// @lc code=start
public class Solution {
    public int Search(int[] nums, int target) {

        return BinarySearch(nums, target,0,nums.Length-1);


    }

    public int BinarySearch(int[] nums, int target,int left,int right)
    {
        if(nums[left]==target)
        {
            return left;
        }


        if(left==right)
        {
            return -1;
        }

        int mid=(left+right)/2;

        return target>nums[mid]?BinarySearch(nums,target,mid+1,right): BinarySearch(nums,target,left,mid);
    }

}
// @lc code=end

