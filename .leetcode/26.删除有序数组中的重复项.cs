/*
 * @lc app=leetcode.cn id=26 lang=csharp
 *
 * [26] 删除有序数组中的重复项
 */

// @lc code=start
public class Solution {
    public int RemoveDuplicates(int[] nums) {

        if(nums.Length==0)
        {
            return 0;
        }

        if(nums.Length==1)
        {
            return 1;
        }


        int n=nums.Length;
        int j=1;
        for (int i = 1; i < n ; i++)
        {           
            if(nums[i]!=nums[i-1])
            {
                nums[j]=nums[i];
                j++;
            }                          
        }
        return j;
    }
}
// @lc code=end

