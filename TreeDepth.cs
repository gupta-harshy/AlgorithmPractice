using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
   
 public class TreeNode {
     public int val;
     public TreeNode left;
     public TreeNode right;
     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
         this.val = val;
         this.left = left;
         this.right = right;
     }
 }
 
    class TreeDepth<T>
    {
        private NodeQueue<T> front;
        private NodeQueue<T> rear;
        public class NodeQueue<T>
        {
            public T treeNode;
            public NodeQueue<T> next;

            public NodeQueue(T treeNode, NodeQueue<T> next = null)
            {
                this.treeNode = treeNode;
                this.next = next;
            }
        }
        public void push(T node)
        {
            if(front == null && rear == null)
            {
                front = rear = new NodeQueue<T>(node);
            }
            else
            {
                rear.next = new NodeQueue<T>(node);
                rear = rear.next;
            }
        }
        public T pop()
        {
            T temp;
            if (front == rear)
            {
                temp = front.treeNode;
                front = rear = null;
            }
            else
            {
                temp = front.treeNode;
                front = front.next;
            }
            return temp;
        }
        public bool IsEmpty()
        {
            return front == null;
        }
        public void CopyAll(TreeDepth<T> source, TreeDepth<T> target)
        {
            while (!source.IsEmpty())
            {
                target.push(source.pop());
            }
        }
        private string LeftSide(TreeNode root)
        {
            string s = "";
            s += root.val;
            if (root.left != null)
            {
                s += LeftSide(root.left);
            }
            if (root.right != null)
            {
                s += LeftSide(root.right);
            }
            return s;
        }
        private string createTree2()
        {
            TreeNode treeNode15 = new TreeNode(15);
            TreeNode treeNode7 = new TreeNode(7);
            TreeNode treeNode9 = new TreeNode(9);
            TreeNode treeNode20 = new TreeNode(20, treeNode15, treeNode7);
            TreeNode treeNode3 = new TreeNode(3, treeNode9, treeNode20);
            return LeftSide(treeNode3);
        }
    }

    public class Test
    {
        public int MaxDepth(TreeNode root)
        {
            int depth = 0;
            TreeDepth<TreeNode> queue1 = new TreeDepth<TreeNode>();
            TreeDepth<TreeNode> queue2 = new TreeDepth<TreeNode>();
            queue1.push(root);
            while (!queue1.IsEmpty() || !queue2.IsEmpty())
            {
                ++depth;
                while (!queue1.IsEmpty())
                {
                    var node = queue1.pop();
                    if(node.left != null)
                    {
                        queue2.push(node.left);
                    }
                    if (node.right != null)
                    {
                        queue2.push(node.right);
                    }
                }
                queue1.CopyAll(queue2, queue1);
            }

            return depth;
        }

        internal int createTree()
        {
            TreeNode treeNode15 = new TreeNode(15);
            TreeNode treeNode7 = new TreeNode(7);
            TreeNode treeNode9 = new TreeNode(9);
            TreeNode treeNode20 = new TreeNode(20, treeNode15, treeNode7);
            TreeNode treeNode3 = new TreeNode(3, treeNode9, treeNode20);
            return MaxDepth(treeNode3);
        }
    }
}
