using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Btree3
    {
        private Node root;
        private int M;
        private int minK;
        private int minP;
        public Node Root
        {
            get
            {
                return root;
            }
        }
        public class Node
        {
            public int[] keys;
            public Node[] pointers;
            public int n; // number of keys in the node
            public bool isLeaf;
            public Node parent;

            public Node(Btree3 btree3, Node p = null)
            {
                keys = new int[btree3.M];
                pointers = new Node[btree3.M + 1];
                isLeaf = true;
                n = 0;
                this.parent = p;
            }
        }
        public Btree3(int m)
        {
            M = m;
            root = new Node(this);
            minP = (int)Math.Ceiling((decimal)M / 2);
            minK = minP - 1;
        }
        public void Insert(int key)
        {
            _insert(root, key);
        }
        public void Delete(int key)
        {
            Node node = _search(root, key);
            if (node.isLeaf)
            {
                _deleteFromLeafNode(node, key);
            }
            else
            {
                _deleteFromNonLeafNode(node, key);
            }
        }
        private void _insert(Node node, int key)
        {
            if (node.isLeaf)
            {
                // add the key
                int i = 0;
                for (i = node.n - 1; i >= 0 && node.keys[i] > key; i--)
                {
                    node.keys[i + 1] = node.keys[i];
                }
                i++;
                node.keys[i] = key;
                node.n++;

                // Check for splitting
                if (node.n > M - 1)
                {
                    _split(node);
                }
            }
            else
            {
                // move down
                int i = 0;
                for (i = node.n - 1; i >= 0 && node.keys[i] > key; i--)
                {

                }
                i++;
                Node temp = node.pointers[i];
                _insert(temp, key);
            }
        }
        private void _split(Node node)
        {
            // check for root condition
            if (node == root)
            {
                Node p = new Node(this);
                root = p;
                node.parent = p;
                p.isLeaf = false;
                p.pointers[0] = node;
            }
            // added some part in right
            Node right = new Node(this, node.parent);
            right.isLeaf = node.isLeaf;
            for (int i = 0; i < minK; i++)
            {
                right.keys[i] = node.keys[i + minK + 1];
                node.n--;
                right.n++;
            }
            if (!node.isLeaf)
            {
                for (int i = 0; i < minP; i++)
                {
                    right.pointers[i] = node.pointers[i + minP];
                    right.pointers[i].parent = right;
                }
            }
            // extract the mid part and add it in parent
            Node parent = node.parent;
            int key = node.keys[node.n - 1]; // last key of left node
            node.n--;
            int k = 0;
            for (k = parent.n - 1; k >= 0 && parent.keys[k] > key; k--)
            {
                parent.keys[k + 1] = parent.keys[k];
                parent.pointers[k + 2] = parent.pointers[k + 1];
            }
            k++;
            parent.keys[k] = key;
            parent.pointers[k + 1] = right;
            parent.n++;

            // check if the parent has access items
            if (parent.n > M - 1)
            {
                _split(parent);
            }

        }
        private Node _search(Node node, int key)
        {
            int i = 0;
            for (i = node.n - 1; i >= 0 && node.keys[i] > key ; i--)
            {

            }
            i++;
            if (i == node.n)
            {
                --i;
            }
            if (node.keys[i] == key)
            {
                return node;
            }
            else if (node.keys[i] > key)
            {
                return _search(node.pointers[i], key);
            }
            else
            {
                return _search(node.pointers[++i], key);
            }
        }
        private void _deleteFromLeafNode(Node node, int key)
        {
            int i = 0; 
            for(i = 0; i < node.n; i++)
            {
                if (node.keys[i] == key)
                {
                    break;
                }
            }
            while (i < node.n - 1)
            { 
                node.keys[i] = node.keys[i + 1];
                i++;
            }
            node.n--;
            if (node.n < minK)
            {
                _deleteFromLeafNodeMovement(node);
            }
        }
        private void _deleteFromLeafNodeMovement(Node node)
        {
            Node parent = node.parent;
            int k = 0;
            for (k = 0; k <= parent.n; k++)
            {
                if (parent.pointers[k] == node)
                {
                    break;
                }
            }
            // check if left key exist or not, with extra key
            if (k - 1 >= 0)
            {
                Node left = parent.pointers[k - 1];
                if (left.n > minK)
                {
                    // take it from left key
                    for (int i = node.n; i > 0; i--)
                    {
                        node.keys[i] = node.keys[i - 1];
                    }
                    node.keys[0] = parent.keys[k - 1];
                    node.n++;
                    parent.keys[k - 1] = left.keys[left.n - 1];
                    left.n--;
                    return;
                }
            }
            // check if right key exist or not, with extra key
            if (k + 1 <= parent.n)
            {
                // check for right child keys
                Node right = parent.pointers[k + 1];
                if (right.n > minK)
                {
                    node.keys[node.n] = parent.keys[k];
                    node.n++;
                    parent.keys[k] = right.keys[0];
                    for (int i = 0; i < right.n - 1; i++)
                    {
                        right.keys[i] = right.keys[i + 1];
                    }
                    right.n--;
                    return;
                }
            }

            _mergeCellsAfterDeletion(node);
            #region commenting merge
            // Merge the cells from left pointer
            if (k - 1 >= 0)
            {
                Node left = parent.pointers[k - 1];
                left.keys[left.n] = parent.keys[k - 1];
                left.n++;
                for (int i = 0; i < node.n; i++)
                {
                    left.keys[i + left.n] = node.keys[i];
                    left.n++;
                }
                for (int i = k - 1; i < parent.n - 1; i++)
                {
                    parent.keys[i] = parent.keys[i + 1];
                }
                for (int i = k; i < parent.n; i++)
                {
                    parent.pointers[i] = parent.pointers[i + 1];
                }
                parent.n--;
                node.parent = null;
                return;
            }
            // Merge the cells from right pointer
            if (k + 1 <= parent.n)
            {
                Node right = parent.pointers[k + 1];
                node.keys[node.n] = parent.keys[k];
                node.n++;
                for (int i = 0; i < right.n; i++)
                {
                    node.keys[node.n] = right.keys[i];
                    node.n++;
                }
                for (int i = k; i < parent.n - 1; i++)
                {
                    parent.keys[i] = parent.keys[i + 1];
                }
                for (int i = k + 1; i < parent.n; i++)
                {
                    parent.pointers[i] = parent.pointers[i + 1];
                }
                parent.n--;
                right.parent = null;
                return;
            }
            #endregion

        }
        private void _mergeCellsAfterDeletion(Node node)
        {
            if (node != root || node.n < minK)
            {
                Node parent = node.parent;
                int k = 0;
                for (k = 0; k <= parent.n; k++)
                {
                    if (parent.pointers[k] == node)
                    {
                        break;
                    }
                }
                Node z = new Node(this, node.parent);
                z.isLeaf = node.isLeaf;
                // Merge the cells from left pointer
                if (k - 1 >= 0)
                {
                    Node left = parent.pointers[k - 1];
                    for (int i = 0; i < left.n; i++)
                    {
                        z.keys[i] = left.keys[i];
                        z.n++;
                    }
                    for (int i = 0; i <= left.n; i++)
                    {
                        z.pointers[i] = left.pointers[i];
                    }
                    z.keys[z.n] = parent.keys[k - 1];
                    z.n++;
                    int pos = z.n;
                    for (int i = 0; i < node.n; i++)
                    {
                        z.keys[z.n++] = node.keys[i];
                    }
                    for (int i = 0; i <= node.n; i++)
                    {
                        z.pointers[pos++] = node.pointers[i];
                    }
                    if (parent.n > 1)
                    {
                        for (int i = k - 1; i < parent.n - 1; i++)
                        {
                            parent.keys[i] = parent.keys[i + 1];
                        }
                        parent.pointers[k - 1] = z;
                        for (int i = k; i <= parent.n; i++)
                        {
                            parent.pointers[i] = parent.pointers[i + 1];
                        }
                        parent.n--;
                    }
                    else
                    {
                        root = z;
                        z.parent = null;
                        return;
                    }
                    _mergeCellsAfterDeletion(parent);
                }
                // Merge the cells from right pointer
                if (k + 1 <= parent.n)
                {
                    Node right = parent.pointers[k + 1];
                    for (int i = 0; i < node.n; i++)
                    {
                        z.keys[i] = node.keys[i];
                        z.n++;
                    }
                    for (int i = 0; i <= node.n; i++)
                    {
                        z.pointers[i] = node.pointers[i];
                    }
                    z.keys[z.n] = parent.keys[k];
                    z.n++;
                    int pos = z.n;
                    for (int i = 0; i < right.n; i++)
                    {
                        z.keys[z.n++] = right.keys[i];
                    }
                    for (int i = 0; i <= right.n; i++)
                    {
                        z.pointers[pos++] = right.pointers[i];
                    }
                    if (parent.n > 1)
                    {
                        for (int i = k; i < parent.n - 1; i++)
                        {
                            parent.keys[i] = parent.keys[i + 1];
                        }
                        parent.pointers[k] = z;
                        for (int i = k + 1; i < parent.n; i++)
                        {
                            parent.pointers[i] = parent.pointers[i + 1];
                        }
                        parent.n--;
                    }
                    else
                    {
                        root = z;
                        z.parent = null;
                        return;
                    }
                    _mergeCellsAfterDeletion(parent);
                }
            }
            else
            {
                return;
            }
        }
        private void _deleteFromNonLeafNode(Node node, int key)
        {
            int i = 0;
            for (i = 0; i < node.n; i++)
            {
                if (node.keys[i] == key)
                {
                    break;
                }
            }
            // check if we can get the key fro left
            int? left = _deleteRightMostKeyFromLeftPointer(node.pointers[i]);
            if (left != null)
            {
                node.keys[i] = left.Value;
                return;
            }
            // check if we can get the key from right
            int? right = _deleteLeftMostKeyFromRightPointer(node.pointers[i + 1]);
            if (right != null)
            {
                node.keys[i] = right.Value;
                return;
            }
            //Merge the nodes 
            // delete the key from node 
            _deleteFromSibling(node);

        }
        private int? _deleteRightMostKeyFromLeftPointer(Node node)
        {
            if (node.isLeaf)
            {
                if (node.n > minK)
                {
                    return node.keys[node.n--]; // read about post and pre 
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return _deleteRightMostKeyFromLeftPointer(node.pointers[node.n]);
            }
        }
        private int? _deleteLeftMostKeyFromRightPointer(Node node)
        {
            if (node.isLeaf)
            {
                if (node.n > minK)
                {
                    int key = node.keys[0];
                    node.n--;
                    for (int i = 0; i < node.n; i++)
                    {
                        node.keys[i] = node.keys[i + 1];
                    }
                    return key;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return _deleteLeftMostKeyFromRightPointer(node.pointers[0]);
            }
        }
        private void _deleteFromSibling(Node node)
        {
            Node parent = node.parent;
            int k = 0;
            for (k = 0; k <= parent.n; k++)
            {
                if (parent.pointers[k] == node)
                {
                    break;
                }
            }
            // check if left key exist or not, with extra key
            if (k - 1 >= 0)
            {
                Node left = parent.pointers[k - 1];
                if (left.n > minK)
                {
                    // take it from left key
                    for (int i = node.n; i > 0; i--)
                    {
                        node.keys[i] = node.keys[i - 1];
                    }
                    for (int i = node.n + 1; i > 0; i--)
                    {
                        node.pointers[i] = node.pointers[i - 1];
                    }
                    node.keys[0] = parent.keys[k - 1];
                    node.pointers[0] = left.pointers[left.n];
                    node.n++;
                    parent.keys[k - 1] = left.keys[left.n - 1];
                    left.n--;
                    return;
                }
            }
            // check if right key exist or not, with extra key
            if (k + 1 <= parent.n)
            {
                // check for right child keys
                Node right = parent.pointers[k + 1];
                if (right.n > minK)
                {
                    node.keys[node.n] = parent.keys[k];
                    node.n++;
                    node.pointers[node.n] = right.pointers[0];
                    parent.keys[k] = right.keys[0];
                    for (int i = 0; i < right.n - 1; i++)
                    {
                        right.keys[i] = right.keys[i + 1];
                    }
                    for (int i = 0; i <= right.n - 1; i++)
                    {
                        right.pointers[i] = right.pointers[i + 1];
                    }
                    right.n--;
                    return;
                }
            }

            _mergeCellsAfterDeletion(node);

        }

        public void display(Node x)
        {
            for (int i = 0; i < x.n; i++)
            {
                Console.Write(x.keys[i] + " ");
            }
            if (!x.isLeaf)
            {
                for (int i = 0; i < x.n + 1; i++)
                {
                    Console.WriteLine();
                    display(x.pointers[i]);
                }
            }
        }

    }
}
