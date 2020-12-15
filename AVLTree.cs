using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class AVLTree
    {
        public Node root;
        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node parent;
            public Node(int val, Node parent)
            {
                this.val = val;
                this.parent = parent;
            }
        }
        public void Insert(int val)
        {
            if (root == null)
            {
                root = new Node(val, null);
            }
            else
            {
                // recieving grand parent node
                Node node = Insert(val, root);
                if (node != null)
                {
                    BalanceFactor(node);
                }
            }
        }
        private Node Insert(int val, Node node)
        {
            if (val < node.val)
            {
                if (node.left == null)
                {
                    node.left = new Node(val, node);
                    return node.parent;
                }
                else
                {
                    return Insert(val, node.left);
                }
            }
            else
            {
                if (node.right == null)
                {
                    node.right = new Node(val, node);
                    return node.parent;
                }
                else
                {
                    return Insert(val, node.right);
                }
            }
        }
        public void Delete(int val)
        {
            Delete(val, root);
        }
        private void Delete(int val, Node node)
        {
            if (node == null)
            {
                // no step required, you are reading whole tree which is not good
            }
            else if(node.val == val)
            {
                Delete(node);
            }
            else if (node.val > val)
            {
                Delete(val, node.left);
            }
            else if (node.val < val)
            {
                Delete(val, node.right);
            }
        }
        private void Delete(Node node)
        {
            Node temp = node.parent;
            // if node is a leaf
            if (node.left == null && node.right == null)
            {
                if (temp.left == node)
                {
                    temp.left = null;
                }
                else
                {
                    temp.right = null;
                }
                node.parent = null;
                BalanceFactor(temp);
            }
            else if (node.left != null && node.right == null)
            {
                Node temp2 = node.left;
                temp.left = temp2;
                node.parent = null;
                temp2.parent = temp;
                node.left = null;
                BalanceFactor(temp);
            }
            else if (node.right != null && node.left == null)
            {
                Node temp2 = node.right;
                temp.right = temp2;
                node.parent = null;
                temp2.parent = temp;
                node.right = null;
                BalanceFactor(temp);
            }
            else if(node.right != null && node.left != null)
            {
                Node node1 = FindMinElement(node.right);
                node.val = node1.val;
                Delete(node1);
            }
        }
        private void BalanceFactor(Node node)
        {
            if (node != null)
            {
                Node node1 = node.parent;
                int diff = TreeHeight(node.left) - TreeHeight(node.right);
                if (diff > 1)
                {
                    if (TreeHeight(node.left.left) > TreeHeight(node.left.right))
                    {
                        if (node1 != null)
                        {
                            // check if the activity is left or right
                            if (node1.left == node)
                            {
                                node1.left = RRRotation(node);
                            }
                            else
                            {
                                node1.right = RRRotation(node);
                            }
                        }
                        else
                        {
                            RRRotation(node);
                        }
                    }
                    else
                    {
                        if (node1 != null)
                        {
                            if (node1.left == node)
                            {
                                node1.left = LRRotation(node);
                            }
                            else
                            {
                                node1.right = LRRotation(node);
                            }
                        }
                        else
                        {
                            LRRotation(node);
                        }
                    }
                }
                else if (diff < -1)
                {
                    if (TreeHeight(node.right.right) > TreeHeight(node.right.left))
                    {
                        if (node1 != null)
                        {
                            if (node1.left == node)
                            {
                                node1.left = LLRotation(node);
                            }
                            else
                            {
                                node1.right = LLRotation(node);
                            }
                        }
                        else
                        {
                            LLRotation(node);
                        }
                    }
                    else
                    {
                        if (node1 != null)
                        {
                            if (node1.left == node)
                            {
                                node1.left = RLRotation(node);
                            }
                            else
                            {
                                node1.right = RLRotation(node);
                            }
                        }
                        else
                        {
                            RLRotation(node);
                        }
                    }
                }
                BalanceFactor(node.parent);
            }
        }
        private int TreeHeight(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + Math.Max(TreeHeight(node.left), TreeHeight(node.right));
        }
        private Node RRRotation(Node node) // grandparent
        {
            Node temp = node.left;
            Node p = node.parent;
            node.left = temp.right;
            if (temp.right != null)
            {
                temp.right.parent = node;
            }
            temp.right = node;
            node.parent = temp;
            temp.parent = p;
            if (temp.parent == null)
            {
                root = temp;
            }
            return temp;
        }
        private Node LLRotation(Node node)
        {
            Node temp = node.right;
            Node p = node.parent;
            node.right = temp.left;
            if (temp.left != null)
            {
                temp.left.parent = node;
            }
            temp.left = node;
            node.parent = temp;
            temp.parent = p;
            if (temp.parent == null)
            {
                root = temp;
            }
            return temp;
        }
        private Node LRRotation(Node node)
        {
            node.left = LLRotation(node.left);
            return RRRotation(node);
        }
        private Node RLRotation(Node node)
        {
            node.right = RRRotation(node.right);
            return LLRotation(node);
        }
        private Node FindMinElement(Node node)
        {
            return (node.left != null) ? FindMinElement(node.left) : node; 
        }
        public void ReadTree(Node node)
        {
            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(node);
            while (nodes.Count > 0)
            {
                Node node1 = nodes.Dequeue();
                Console.WriteLine("Value of the node - " + node1.val);
                if (node1.left != null)
                {
                    nodes.Enqueue(node1.left);
                }
                if (node1.right != null)
                {
                    nodes.Enqueue(node1.right);
                }
            }
        }
        public int CalculateLeafNodes(Node node)
        {
            int sum = 0;
            if (node == null)
            {
                return 0;
            }
            else if (node.left == null && node.right == null)
            {
                return 1;
            }
            else
            {
                sum = sum + CalculateLeafNodes(node.left) + CalculateLeafNodes(node.right);
                return sum;
            }
        }
    }
}
