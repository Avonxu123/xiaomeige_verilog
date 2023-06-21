/*
 * @lc app=leetcode.cn id=27 lang=csharp
 *
 * [27] 移除元素
 */

// @lc code=start
public class Solution {
    public int RemoveElement(int[] nums, int val) {

        if(nums.Length==0)
        {
            return 0;
        }
        int n=nums.Length;
        int i=0;
        int j=0;
        while(i<n)
        {
            if(nums[i]!=val)
            {
                nums[j]=nums[i];
                j++;
            }
            i++;
        }
        return j;
    }
}
// @lc code=end

