/*
 * @lc app=leetcode.cn id=98 lang=csharp
 *
 * [98] 验证二叉搜索树
 */

// @lc code=start
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    public bool IsValidBST(TreeNode root)
    {
        return process(root).isBST;

    }

    public ReturnData process(TreeNode head)
    {
        if (head == null) return null;

        ReturnData leftData = process(head.left);
        ReturnData rightData = process(head.right);

        int min = head.val;
        int max = head.val;
        if (leftData != null)
        {
            min = Math.Min(min, leftData.min);
            max = Math.Max(max, leftData.max);
        }

        if (rightData != null)
        {
            min = Math.Min(min, rightData.min);
            max = Math.Max(max, rightData.max);
        }

        bool isBST = true;

        if (leftData != null && (!leftData.isBST || leftData.max >= head.val))
        {
            isBST = false;
        }

        if (rightData != null && (!rightData.isBST || rightData.min <= head.val))
        {
            isBST = false;
        }

        return new ReturnData(max, isBST, min);
    }
}

public class ReturnData
{
    public int max;
    public int min;
    public bool isBST;

    public ReturnData(int max, bool isBst, int min)
    {
        this.max = max;
        isBST = isBst;
        this.min = min;
    }
}
// @lc code=end

