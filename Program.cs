using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConsoleApp1
{
    class Queue
    {
        int[] queue;
        int front = -1;
        int tail = -1;
        int size;

        Queue(int length)
        {
            size = length;
            queue = new int[size];
        }

        void EnQueue(int value)
        {
            if(front == -1)
            {
                ++front;
                ++tail;
                queue[front] = value;
            }
            else if (front == size - 1 && tail == 0)
            {
                // Stack is full
            }
            else if (front == size - 1 && tail != 0 )
            {
                front = 0;
                queue[front] = value;
            }
            else if (tail == front + 1)
            {
                // stack is full
            }
            else
            {
                ++front;
                queue[front] = value;
            }
        }

        void DeQueue()
        {
            if(tail == -1)
            {
                // queue is empty
            }
            else if (tail + 1 == size)
            {
                // get the element 
                tail = 0;
            }
            else if (tail > front)  // fetch number of elements in queue)
            {
                // there is no element to pull
            }

        }

    }

    public class Solution
    {
        Node head;
        private class Node
        {
            public char val;
            public Node next;
            public Node(char val, Node next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public void Push(char x)
        {
            if (head == null)
            {
                head = new Node(x, null);
            }
            else
            {
                head = new Node(x, head);
            }
        }

        public void Pop()
        {
            head = head.next;
        }

        public char Top()
        {
            return (head == null) ? head.val : char.MinValue;
        }

        public bool IsValid(string s)
        {
            head = null;
            char[] arr = s.ToCharArray();
            
            foreach (char ch in arr)
            {
                if (ch == '(' && Top() == ')')
                {
                    Pop();
                }
                else if (ch == '[' && Top() == ']')
                {
                    Pop();
                }
                else if (ch == '{' && Top() == '}')
                {
                    Pop();
                }
                else
                {
                    Push(ch);
                }
            }
            return (head == null);
        }
    }

    class Record
    {
        public static int MySqrt(int x)
        {
            // 2,14,73,95,599
            if (x > 0 && x < 3)
            {
                return 1;
            }
            int[] arr = new int[x];
            for (int i = 0; i < x; i++)
            {
                arr[i] = i * i;
            }
            int low = 0, high = x, mid = (high + low) / 2;
            while (mid != high || mid != low)
            {
                if (arr[mid] == x)
                {
                    return mid;
                }
                else if (arr[mid] < x && arr[mid + 1] > x)
                {
                    return mid;
                }
                else if (arr[mid] > x && arr[mid - 1] < x)
                {
                    return mid - 1;
                }
                else if (arr[mid] < x)
                {
                    low = mid;
                }
                else
                {
                    high = mid;
                }
                mid = (high + low) / 2;
            }
            return 0;
        }
        public static int MySqrt2(int x)
        {
            int count = 0;
            for (long i = 1; i * i <= x; i++)
            {
                count = (int)i;
            }
            return count;
        }
        public int[] MergeSort(int[] arr)
        {
            int l = 0, r = arr.Length - 1;
            while (l < r)
            {
                int mid = l + (r - l) / 2;
                var arr1 = MergeSort(Split(arr, l, mid));
                var arr2 = MergeSort(Split(arr, mid + 1, r));
                return MergeCombine(arr1, arr2);
            }
            return arr;
        }
        public int[] MergeCombine(int[] arr1, int[] arr2)
        {
            int[] arr = new int[arr1.Length + arr2.Length];
            int i = 0, j = 0, k = 0;
            while (i < arr1.Length && j < arr2.Length)
            {
                if (arr1[i] < arr2[j]) arr[k++] = arr1[i++];
                else arr[k++] = arr2[j++];
            }
            while (i < arr1.Length) arr[k++] = arr1[i++];
            while (j < arr2.Length) arr[k++] = arr2[j++];

            return arr;
        }
        public int[] Split(int[] arr, int startIndex, int lastIndex)
        {
            int[] ar = new int[lastIndex - startIndex + 1];
            int count = 0;
            for (int i = startIndex; i <= lastIndex; i++)
            {
                ar[count++] = arr[i];
            }
            return ar;
        }
        public int MaxSubArray(int[] nums)
        {
            int l = 0, r = nums.Length - 1;
            if (l == r)
            {
                return nums[0];
            }
            else
            {
                int mid = (l + r) / 2;
                int leftMax = MaxSubArray(Split(nums, 0, mid));
                int rightMax = MaxSubArray(Split(nums, mid + 1, r));
                int centerMax = CenterMax(nums, mid, l, r);
                return Math.Max(Math.Max(leftMax, rightMax), centerMax);
            }
        }
        public int CenterMax(int[] nums, int mid, int l, int r)
        {
            int leftSum = 0, leftMax = int.MinValue, rightSum = 0, rightMax = int.MinValue;
            for (int i = mid; i >= l; i--)
            {
                leftSum += nums[i];
                if (leftSum > leftMax)
                {
                    leftMax = leftSum;
                }
            }
            for (int i = mid + 1; i <= r; i++)
            {
                rightSum += nums[i];
                if (rightSum > rightMax)
                {
                    rightMax = rightSum;
                }
            }
            return Math.Max(Math.Max(leftMax, rightMax), leftMax + rightMax);
        }
        public int ClimbStairs(int n)
        {

            int sum = 0;
            if (n == 1)
            {
                return 1;
            }
            if (n == 2)
            {
                return 2;
            }
            sum += ClimbStairs(n - 1);
            sum += ClimbStairs(n - 2);

            return sum;
        }
        public int MaxProfit(int[] prices)
        {
            if (prices.Length < 2) return 0;
            int max = int.MinValue; int index = int.MaxValue;
            for (int i = 1; i < prices.Length; i++)
            {
                int newMax = prices[i] - prices[0];
                if (max < newMax)
                {
                    max = newMax;
                    index = i;

                }
            }
            for (int i = 1; i < index; i++)
            {
                int newdiff = prices[index] - prices[i];
                max = (max < newdiff) ? newdiff : max;
            }
            return max < 0 ? 0 : max;
        }
        public int Rob(int[] nums)
        {
            if (nums.Length == 0) return 0;
            if (nums.Length == 1) return nums[0];
            int[] arr = new int[nums.Length];
            arr[0] = nums[0];
            arr[1] = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < nums.Length; i++)
            {
                arr[i] = Math.Max(arr[i - 2] + nums[i], arr[i - 1]);
            }
            return arr[nums.Length - 1];
        }
    }

    public class RecentCounter
    {
        Node head, bottom;
        int count;
        private class Node
        {
            internal int val;
            internal Node next;
            public Node(int val, Node next)
            {
                this.val = val;
                this.next = next;
            }
        }

        private void push(int t)
        {
            if (head == null && bottom == null)
            {
                head = bottom = new Node(t, null);
            }
            else
            {
                bottom.next = new Node(t, bottom);
                bottom = bottom.next;
            }
            ++count;
        }

        private void pop(int min)
        {
            while (head.val < min)
            {
                head = head.next;
                --count;
            }
        }

        public RecentCounter()
        {
            count = 0;
        }

        public int Ping(int t)
        {
            push(t);
            pop(t - 3000);
            return count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Program p = new Program();
            //RecentCounter recentCounter = new RecentCounter();
            //int[] nums = new int[] { 1, 100, 3001, 3002 };
            //foreach (int a in nums)
            //{
            //    Console.WriteLine(recentCounter.Ping(a));
            //}
            //Console.WriteLine(new Test().createTree());
            //Console.ReadKey();

            //string str = string.Empty;
            //char[] arr = str.ToCharArray();
            //int[] nums = new int[] { 7, 6, 4, 3, 1};
            // Solution solution = new Solution();
            //int []nums = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

            //AVLTree aVLTree = new AVLTree();
            //aVLTree.Insert(10);
            //aVLTree.Insert(9);
            //aVLTree.Insert(8);
            //aVLTree.Insert(7);
            //aVLTree.Insert(6);
            //aVLTree.Insert(5);
            //aVLTree.Insert(4);
            //aVLTree.Insert(3);
            //aVLTree.Insert(2);
            //aVLTree.Insert(1);
            //int[] nums = new int[] { 5, 2, 8, 1, 3, 7, 10, 4, 6, 9, 11, 12 };
            //foreach (int a in nums)
            //{
            //    aVLTree.Insert(a);
            //}
            //aVLTree.ReadTree(aVLTree.root);
            //aVLTree.Delete(5);
            //aVLTree.ReadTree(aVLTree.root);

            //HEAP SORT
            //HeapSort heapSort = new HeapSort();
            //int[] nums = new int[] { 5, 2, 8, 1, 3, 7, 10, 4, 6, 9, 11, 12 };
            //foreach (int a in nums)
            //{
            //    heapSort.Add(a);
            //}
            //Console.WriteLine(heapSort.MinValue());
            //Console.WriteLine(heapSort.MinValue());
            //Console.WriteLine(heapSort.MinValue());
            //Console.WriteLine(heapSort.MinValue());
            //Console.WriteLine(heapSort.MinValue());
            //Console.ReadKey();

            //Linked List Reverse
            //LinkedListReverse<int> linkedListReverse = new LinkedListReverse<int>();
            //linkedListReverse.LinkedListMain();

            //sorting
            int[] nums = new int[] { 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            //int[] nums = new int[] { 0, 1, 1, 0, 1, 2, 1, 2, 0, 0, 0, 1 };
            Sort sort = new Sort();
            //sort.QuickSort(nums, 0, nums.Length - 1);
            sort.MergeSort(nums);
            //foreach (int a in nums)
            //{
            //    Console.Write(a + " ");
            //}

            //SubArray
            //int[] nums = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            //int[] nums = new int[] { 1, 4, 0, 0, 3, 10, 5 };
            //SubArray subArray = new SubArray();
            //subArray.SortArray(nums);
            //Console.WriteLine(subArray.FindSubArrayOfGivenSum2(nums, 7));
            //foreach (int a in nums)
            //{
            //    Console.Write(a + " ");
            //}
            //Console.WriteLine(subArray.MaxSubArray(nums));
            //MwayTree.Start();
            Console.ReadKey();
        }
    }
}
