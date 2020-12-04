using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class HeapSort
    {
        private int capacity = 12;
        private int size = -1;
        private int[] arr;
        public HeapSort()
        {
            arr = new int[capacity];
        }
        public void Add(int num)
        {
            if (size >= capacity - 1)
            {
                arr = IncreaseArraySize();
            }
            arr[++size] = num;
            HeapifyUp();
        }
        private void HeapifyUp()
        {
            int index = size;
            while (arr[ParentIndex(index)] > arr[index])
            {
                Swap(index, ParentIndex(index));
                index = ParentIndex(index);
            }
        }
        public int MinValue()
        {
            int minValue = arr[0];
            HeapifyDown();
            return minValue;
        }
        private void HeapifyDown()
        {
            int index = 0;
            arr[index] = arr[size--];
            arr[size + 1] = int.MaxValue;
            try
            {
                while (arr[index] > arr[LeftIndex(index)] || arr[index] > arr[RightIndex(index)])
                {
                    if (arr[RightIndex(index)] > arr[LeftIndex(index)])
                    {
                        Swap(index, LeftIndex(index));
                        index = LeftIndex(index);
                    }
                    else
                    {
                        Swap(index, RightIndex(index));
                        index = RightIndex(index);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private int[] IncreaseArraySize()
        {
            int[] temp = new int[capacity * 2];
            Array.Copy(arr, temp, capacity);
            capacity *= 2;
            return temp;
        }
        //Helper methods
        private int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        private int LeftIndex(int index)
        {
            return index * 2 + 1;
        }
        private int RightIndex(int index)
        {
            return index * 2 + 2;
        }
        private void Swap(int source, int destination)
        {
            int temp = arr[source];
            arr[source] = arr[destination];
            arr[destination] = temp;
        }

    }
}
