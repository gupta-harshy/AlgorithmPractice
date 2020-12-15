using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Sort
    {
        public void ShellSort(int []arr)
        {
            for (int gap = arr.Length/2; gap > 0; gap = gap/2)
            {
                for (int i = 0; i < arr.Length - gap; i++)
                {
                    if (arr[i] > arr[i + gap])
                    {
                        for(int k = i; k >= 0; k = k- gap)
                        {
                            if(arr[k + gap] < arr[k])
                            {
                                int temp = arr[k];
                                arr[k] = arr[k + gap];
                                arr[k + gap] = temp;
                            }
                        }
                    }
                }
            }
        }

        public void QuickSort(int []arr, int l, int h)
        {
            // 5, 2, 8, 1, 3, 7, 10, 4, 6, 9, 11, 12
            if (l < h)
            {
                int k = Partition(arr, l, h);
                QuickSort(arr, l, k);
                QuickSort(arr, k + 1, h);
            }
        }

        public void MergeSort(int []arr)
        {
            foreach (int a in MergeSort(arr, 0, arr.Length - 1))
            {
                Console.Write(a + " ");
            };
        }

        private int[] MergeSort(int []arr, int l, int r)
        {
            int mid = l + (r - l) / 2;
            if (l >= r)
            {
                return SliceArray(arr, l, r);
            }
            int []leftArray = MergeSort(arr, l, mid);
            int []rightArray = MergeSort(arr, mid + 1, r);
            // combine 
            int []arrNew = new int[leftArray.Length + rightArray.Length];
            int i = 0, j = 0, k = 0;
            while (i < leftArray.Length && j < rightArray.Length)
            {
                if (leftArray[i] < rightArray[j])
                {
                    arrNew[k++] = leftArray[i++];
                }
                else
                {
                    arrNew[k++] = rightArray[j++];
                }
            }
            while(i < leftArray.Length)
            {
                arrNew[k++] = leftArray[i++];
            }
            while(i <  rightArray.Length)
            {
                arrNew[k++] = rightArray[j++];
            }
            return arrNew;
        }

        private int[] SliceArray(int []arr, int l, int r)
        {
            int[] temp = new int[r - l + 1];
            int i = 0;
            for (int k = l; k <= r; k++)
            {
                temp[i++] = arr[k];
            }
            return temp;
        }

        private int Partition(int []arr, int l, int h)
        {
            int p = l + ((h - l) / 2);
            while (l < h)
            {
                while (arr[l] <= arr[p])
                {
                    ++l;
                }
                while (arr[h] > arr[p])
                {
                    --h;
                }
                if (l < h)
                {
                    Swap(arr, l, h);
                }
            }
            Swap(arr, p, h);
            return h;
        }

        private void Partition2(int []arr)
        {
            int l = 0, r = arr.Length - 1;
            int p = l + ((r - l) / 2);
            while (l < r)
            {

            }
        }

        // Helper Methods
        private void Swap(int[] arr, int source, int destination)
        {
            int temp = arr[source];
            arr[source] = arr[destination];
            arr[destination] = temp;
        }
    }
}
