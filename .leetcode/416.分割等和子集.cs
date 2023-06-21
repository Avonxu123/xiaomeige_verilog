/*
 * @lc app=leetcode.cn id=416 lang=csharp
 *
 * [416] 分割等和子集
 */

// @lc code=start
class Solution {
public:
  bool canPartition(vector<int>& nums) {
    int sum = 0;
    for(int e : nums) sum += e;
    if(sum & 1) return false;
    vector<bool> d((sum>>=1)+1, false);//sum/=2
    for(int i = 0 ; i < nums.size() ; i++){
      for(int s = sum ; s >= nums[i] ; s--){//从后往前，因为前面的元素我们已经求过了(i>0时确实已经求过了，i==0时我们求一遍即可，下面的代码也确实给出了i==0的情况)，可以直接用
        if(!i) d[s] = (nums[i]==s);//i==0要单独求{ nums[0]一个元素和为s }
        else d[s] = d[s] || d[s-nums[i]];
      }
    }
    return d[sum];
  }
};

// @lc code=end

