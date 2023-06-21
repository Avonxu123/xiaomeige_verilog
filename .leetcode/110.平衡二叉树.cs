/*
 * @lc app=leetcode.cn id=110 lang=csharp
 *
 * [110] 平衡二叉树
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
public class Solution {
    public bool IsBalanced(TreeNode root) {
        return isBST(root).isBalanced;
    }

    public  ReturnType isBST(TreeNode head)
        {
            if (head == null)
            {
                return new ReturnType(true, 0);
            }

            ReturnType returnTypeLeft = isBST(head.left);
            ReturnType returnTypeRight = isBST(head.right);

            int height = Math.Max(returnTypeLeft.height, returnTypeRight.height) + 1;//要加上他自己

            bool isBalanced = returnTypeRight.isBalanced && returnTypeLeft.isBalanced &&
                              Math.Abs(returnTypeLeft.height - returnTypeRight.height) <= 1;
            return new ReturnType(isBalanced, height);
        }
}
public class ReturnType
    {
        public bool isBalanced;
        public int height;

        public ReturnType(bool isBalanced, int height)
        {
            this.isBalanced = isBalanced;
            this.height = height;
        }
    }


// @lc code=end

