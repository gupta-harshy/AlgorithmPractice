using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    class Btree2
    {
        private int M;
        private int minP;
        private int mink;
        private Node root;
        public Node Root
        {
            get
            {
                return root;
            }
        }
        public Btree2(int m)
        {
            M = m;
            minP = (int)Math.Ceiling((decimal)M / 2);
            mink = minP - 1;
            root = new Node(this);
        }
        public class Node
        {
            public int n; // number of keys
            public bool isLeaf = true;
            public int[] keys;
            public Node[] pointers;

            public Node(Btree2 btree2)
            {
                keys = new int[btree2.M];
                pointers = new Node[btree2.M];
            }
        }

        public void Insert(int key)
        {
            // root full hai
            Node r = root;
            if (r.n == M - 1)
            {
                Node s = new Node(this);
                root = s;
                s.isLeaf = false;
                s.n = 0;
                s.pointers[0] = r;
                Split(s, 0, r);
                _insert(key, s);
            }
            else
            {
                // simply insert 
                _insert(key, root);
            }
        }

        private void _insert(int key, Node node)
        {
            if (node.isLeaf)
            {
                int i = 0;
                for (i = node.n - 1; i>= 0 && node.keys[i] > key; i--)
                {
                    node.keys[i + 1] = node.keys[i];
                }
                node.keys[i + 1] = key;
                node.n = node.n + 1;
            }
            else
            {
                Node temp;
                int i = 0;
                for (i = node.n - 1; i >= 0 && node.keys[i] > key; i--)
                {
                    
                }
                i++;
                temp = node.pointers[i];
                if (temp.n == M - 1)
                {
                    Split(node, i, temp);
                }
                _insert(key, temp);
            }
        }

        private void Split(Node parent, int pos, Node child)
        {
            Node z = new Node(this);
            for (int i = 0; i < M - minP - 1; i++)
            {
                z.keys[i] = child.keys[i + minP];
                z.n++;
            }
            if (!child.isLeaf)
            {
                for (int i = 0; i < M - minP; i++)
                {
                    z.pointers[i] = child.pointers[i + minP];
                }
            }
            z.isLeaf = child.isLeaf;
            child.n -= z.n;
            for (int i = parent.n; i > pos; i--)
            {
                parent.keys[i] = parent.keys[i - 1];
            }
            for (int i = parent.n + 1; i > pos + 1; i--)
            {
                parent.pointers[i] = parent.pointers[i - 1];
            }
            parent.keys[pos] = child.keys[child.n - 1];
            parent.n += 1;
            parent.pointers[pos + 1] = z;
            child.n -= 1;
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

        public void Start(int n)
        {
            var b = new Btree2(n);
            b.Insert(8);
            b.Insert(9);
            b.Insert(10);
            b.Insert(11);
            b.Insert(15);
            b.Insert(20);
            b.Insert(17);

            b.display(b.root);
        }
    }
}
