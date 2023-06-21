/*
 * @lc app=leetcode.cn id=222 lang=csharp
 *
 * [222] 完全二叉树的节点个数
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
    public int CountNodes(TreeNode root) {
        if(root==null)
        {
            return 0;
        }
        
        Queue<TreeNode> quene=new Queue<TreeNode>();
        quene.Enqueue(root);
        
        int res=1;
        while(quene.Count!=0)
        {
            TreeNode treeNode=quene.Dequeue();

            if(treeNode.left!=null)
            {
                quene.Enqueue(treeNode.left);
                res++;
            }

            if(treeNode.right!=null)
            {
                quene.Enqueue(treeNode.right);
                res++;
            }

        }

        return res;
    }
}
// @lc code=end

