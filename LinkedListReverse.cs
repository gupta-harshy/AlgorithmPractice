using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class LinkedListReverse<T>
    {
        private Node<T> start = null;
        public Node<T> Start
        {
            get;
            private set;
        }
        public class Node<T>
        {
            public T val;
            public Node<T> next;
            public Node(T val)
            {
                this.val = val;
            }
        }
        public void AddNode(T val)
        {
            if(start == null)
            {
                start = new Node<T>(val);
            }
            else
            {
                Node<T> temp = start;
                while (temp.next != null)
                {
                    temp = temp.next;
                }
                temp.next = new Node<T>(val);
            }
        }
        public void DisplayLinkedList()
        {
            Node<T> temp = start;
            while (temp != null)
            {
                Console.Write(temp.val + "---->");
                temp = temp.next;
            }
        }
        public void ReverseLinkedListIterative()
        {
            if (start.next == null)
            {
                // do nothing
            }
            else
            {
                Node<T> temp = null;
                Node<T> temp2 = start.next;
                while (temp2 != null)
                {
                    start.next = temp;
                    temp = start;
                    start = temp2;
                    temp2 = temp2.next;
                }
                start.next = temp;
            }
        }
        public void ReverseLinkedListRecursive()
        {
            Node<T> node = ReverseLinkedListRecur(start);
            node.next = null;
        }
        private Node<T> ReverseLinkedListRecur(Node<T> node)
        {
            if (node.next != null)
            {
                Node<T> temp = ReverseLinkedListRecur(node.next);
                temp.next = node;
                return node;
            }
            else
            {
                start = node;
                return start;
            }
        }
        public void LinkedListMain()
        {
            LinkedListReverse<int> linkedListReverse = new LinkedListReverse<int>();
            int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            foreach (int a in nums)
            {
                linkedListReverse.AddNode(a);
            }
            linkedListReverse.DisplayLinkedList();
            Console.WriteLine();
            //linkedListReverse.ReverseLinkedListIterative();
            //linkedListReverse.DisplayLinkedList();
            //Console.WriteLine();
            //linkedListReverse.ReverseLinkedListRecursive();
            //linkedListReverse.DisplayLinkedList();
            //linkedListReverse.NElementFromLast(3);
            linkedListReverse.PrintLinkedlistReverse();
            //Console.ReadKey();
        }
        public void NElementFromLast(int n)
        {
            Node<T> nnode = start, temp = start;
            for (int i = 1; i <= n; i++)
            {
                if (temp != null)
                {
                    temp = temp.next;
                }
                else
                {
                    Console.WriteLine("No Nth Element Exists");
                }
            }
            while (temp != null)
            {
                temp = temp.next;
                nnode = nnode.next;
            }
            Console.WriteLine(nnode.val);
        }
        public bool HasCycle(Node<T> head)
        {
            if (head == null)
            {
                return false;
            }
            if (head.next == null)
            {
                return false;
            }
            Node<T> one = head, two = head.next;
            while (two.next != null)
            {
                if (one == two)
                {
                    return true;
                }
                one = one.next;
                two = two.next.next;
                if (two == null)
                {
                    break;
                }
            }
            return false;
        }
        public Node<T> GetIntersectionNode(Node<T> headA, Node<T> headB)
        {
            Node<T> lt = null;
            Node<T> temp = headB;
            while (headA != null)
            {
                while (temp != null)
                {
                    if (headA == temp)
                    {
                        return headA;
                    }
                    temp = temp.next;
                }
                headA = headA.next;
                temp = headB;
            }
            return lt;
        }
        public void PrintLinkedlistReverse()
        {
            Node<T> temp = start;
            PrintLinkedlistReverse(temp);
        }
        private void PrintLinkedlistReverse(Node<T> node)
        {
            if (node != null)
            {
                PrintLinkedlistReverse(node.next);
                Console.Write(node.val + " ");
            }
        }
    }
}
