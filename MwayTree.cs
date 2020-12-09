using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class MwayTree
    {
        Node root;
        public class Node
        {
            public int []keys = new int[3] { int.MinValue, int.MinValue, int.MinValue };
            public Node[] pointers = new Node[4];
            public Node(int key)
            {
               this.keys[0] = key;
            }
        }

        public void Addkey(int key)
        {
            Node node = root;
            if (root == null)
            {
                root = new Node(key);
            }
            else
            {
                Addkey(key, node);
            }
        }

        private void Addkey(int key, Node node)
        {
            for (int i = 0; i < node.keys.Length; i++)
            {
                if (key > node.keys[i])
                {
                    // Add the key if not present in the same node
                    if (node.keys[i] == int.MinValue)
                    {
                        node.keys[i] = key;
                        break;
                    }
                    // for the last pointer
                    else if (i == node.keys.Length -1)
                    {
                        AddkeyMovingToNextNode(node, node.keys.Length, key);
                        break;
                    }
                }
                else
                {
                    AddkeyMovingToNextNode(node, i, key);
                    break;
                }
            }
        }

        private void AddkeyMovingToNextNode(Node node, int index, int key)
        {
            if (node.pointers[index] == null)
            {
                node.pointers[index] = new Node(key);
            }
            else
            {
                Addkey(key, node.pointers[index]);
            }
        }

        public bool SearchElement(int key)
        {
            return SearchElement(key, root) == null ? false: true;
        }

        private Node SearchElement(int key, Node node)
        {
            if (node == null)
            {
                return null;
            }
            for (int i = 0; i < node.keys.Length; i++)
            {
                if (key == node.keys[i])
                {
                    return node;
                }
                else if (key > node.keys[i])
                {
                    if (i == node.keys.Length - 1)
                    {
                        return SearchElement(key, node.pointers[3]);
                    }
                }
                else
                {
                    return SearchElement(key, node.pointers[i]);
                }
            }
            return null;
        }

        public void DeleteElement(int key)
        {
            Node node = SearchElement(key, root);
            if (node == null)
            {
                // No node to delete
            }
            else
            {
                DeleteElement(key, node);
            }
        }

        private void DeleteElement(int key, Node node)
        {
            int index = 0;
            for (int i = 0; i < node.keys.Length; i++)
            {
                if (node.keys[i] == key)
                {
                    index = i;
                }
            }
            Node left = node.pointers[index];
            Node right = node.pointers[index+1];
            if (left == null && right == null)
            {
                node.keys[index] = int.MinValue;
            }
            else if (left != null)
            {
                int leftMaxKey = FindMaxElementInNode(left);
                node.keys[index] = leftMaxKey;
                DeleteElement(leftMaxKey, left);
            }
            else if (right != null)
            {
                int rightMaxKey = FindMaxElementInNode(right);
                node.keys[index] = rightMaxKey;
                DeleteElement(rightMaxKey, right);
            }
        }

        private int FindMaxElementInNode(Node node)
        {
            int max = int.MinValue;
            foreach(int n in node.keys)
            {
                max = (max < n) ? n : max;
            }
            return max;
        }

        public void DisplayTree()
        {
            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(root);
            while (nodes.Count != 0)
            {
                Node node = nodes.Dequeue();
                foreach (int n in node.keys)
                {
                    Console.Write(n == int.MinValue ? "X " : n  + " ");
                }
                Console.WriteLine();
                foreach (Node n in node.pointers)
                {
                    if (n != null)
                    {
                        nodes.Enqueue(n);
                    }
                }
            }
        }

        public static void Start()
        {
            MwayTree mwayTree = new MwayTree();
            //int[] nums = {1, 6, 9, 12, 7, 14, 50, 78, 2, 95, 99, 93, 8, 10, 11, 5, 13, 40, 43, 48 };
            int[] nums = {10, 45, 84, 5, 8, 32, 50, 60, 70, 92, 99, 110, 21, 52, 55, 59, 95 };
            foreach (int n in nums)
            {
               mwayTree.Addkey(n);
            }
            mwayTree.DisplayTree();
            Console.WriteLine(mwayTree.SearchElement(99));
            Console.WriteLine(mwayTree.SearchElement(121));
            mwayTree.DeleteElement(45);
            Console.WriteLine("Tree After Deletion");
            mwayTree.DisplayTree();
        }
    }
}
