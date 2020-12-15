using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class BTree
    {
        Node root;
        public class Node
        {
            public int []keys = new int[3];
            public Node[] pointers = new Node[4];
            public Node parent;
        }

        public void AddKey(int key)
        {
            if (root == null)
            {
                root = new Node();
                root.keys[0] = key;
            }
            else
            {
                AddKey(key, root);
            }
        }

        private void AddKey(int key, Node node)
        {
            int index = node.keys.Length;
            // getting the index of the element
            for (int i = 0; i < node.keys.Length; i++)
            {
                if (key > node.keys[i] && node.keys[i] != 0)
                {
                    continue;
                    // could be removed now with default value of index
                    //if(i == node.keys.Length - 1)
                    //{
                    //    index = i + 1;
                    //}
                }
                else
                {
                    index = i;
                }
            }

            // Adding element in the node
            if (node.pointers[index] == null)
            {
                bool isEmpty = CheckNodeHasEmptySpace(node);
                // if empty add the value or re-arrange
                if (isEmpty)
                {
                    if (node.keys[index] == 0)
                    {
                        node.keys[index] = key;
                    }
                    else
                    {
                        RearranageNode(key, node);
                    }
                }
                else
                {
                    // split the node in two and perform operation
                    AddNodeBySplitting(key, node);
                }
            }
            else
            {
                // move to next node
                AddKey(key, node.pointers[index]);
            }
        }

        private void AddNodeBySplitting(int key, Node node)
        {
            Node left = new Node();
            Node right = new Node();
            Node parent = new Node();
            int mid = node.keys.Length/2;
            bool keyAdded = false;
            for (int i = 0; i <= mid; i++)
            {
                left.keys[i] = node.keys[i];
                left.pointers[i] = node.pointers[i];
            }
            for (int i = mid + 1; i < node.keys.Length; i++)
            {
                left.keys[i] = node.keys[i];
                left.pointers[i] = node.pointers[i];
            }

        }

        private bool CheckNodeHasEmptySpace(Node node)
        {
            foreach (int n in node.keys)
            {
                if (n == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void RearranageNode(int key, Node node)
        {
            for (int i = node.keys.Length - 1, j = node.keys.Length; i >= 0; i++, j--)
            {
                if (node.keys[i] == 0)
                {
                    continue;
                }
                else if (node.keys[i] < key)
                {
                    node.keys[j] = node.keys[i];
                    node.pointers[j] = node.pointers[i];
                }
                else
                {
                    node.keys[i] = key;
                    node.pointers[i] = null;
                    break;
                }
            }
        }

    }
}
