using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class SubArray
    {
        public string FindSubArrayOfGivenSum(int []arr, int sum)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int s = 0;
                for (int k = i; k < arr.Length; k++)
                {
                    s += arr[k];
                    if (s == sum)
                    {
                        return "Sum found between indexes "+i+" and "+k;
                    }
                }
            }
            return "No Subarray Found";
        }
        public string FindSubArrayOfGivenSum2(int[] arr, int sum)
        {
            int i = 0, k = 0, s = arr[k];
            while(k < arr.Length){
                if (s == sum)
                {
                    return "Sum found between indexes " + i + " and " + k;
                }
                else if (s < sum)
                {
                    s += arr[++k];
                }
                else
                {
                    s -= arr[i++];
                }
            }
            return "No Subarray Found";
        }
        public void SortArray(int[] arr)
        {
            int placement = 0;
            for (int i = 0; i< arr.Length - 1; i++)
            {
                if (arr[i] < 1)
                {
                    Swap(arr, i, placement);
                    ++placement;
                }
            }
            placement = arr.Length - 1;
            for (int i = arr.Length -1; i >= 0 ; i--)
            {
                if (arr[i] > 1)
                {
                    Swap(arr, i, placement);
                    --placement;
                }
            }
        }

        private void Swap(int []arr, int source, int destination)
        {
            int temp = arr[source];
            arr[source] = arr[destination];
            arr[destination] = temp;
        }

        //Input: nums = [-2,1,-3,4,-1,2,1,-5,4]
        //Output: 6
        //Explanation: [4,-1,2,1] has the largest sum = 6.
        public int MaxSubArray(int[] nums)
        {
            int max;
            int maxima = max = nums[0];
            for (int i = 1; i<nums.Length; i++)
            {
                maxima = Math.Max(maxima + nums[i], nums[i]);
                max = max < maxima ? maxima : max;
            }
            return max;
        }
    }
}
