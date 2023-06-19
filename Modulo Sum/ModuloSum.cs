using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class ModuloSum
    {
        #region YOUR CODE IS HERE    

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// Fill this function to check whether there's a subsequence numbers in the given array that their sum is divisible by M
        /// </summary>
        /// <param name="items">array of integers</param>
        /// <param name="N">array size </param>
        /// <param name="M">value to check against it</param>
        /// <returns>true if there's subsequence with sum divisible by M... false otherwise</returns>
        static public bool SolveValue(int[] items, int N, int M)
        {
            bool[] dp = new bool[M];
            dp[0] = false;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (dp[j])
                    {
                        int sum = (j + items[i]) % M;
                        if (sum == 0)
                            return true;

                        dp[sum] = true;
                    }
                }

                // Check if the current item is divisible by M
                if (items[i] % M == 0)
                    return true;

                dp[items[i] % M] = true;
            }

            return dp[0];
        }



        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>if exists, return the numbers themselves whose sum is divisible by ‘M’ else, return null</returns>
        public static int[] ConstructSolution(int[] items, int N, int M)
        {
            // Create an array to store the cumulative sums modulo M.
            int[] cumSum = new int[N + 1];

            // Initialize the cumulative sum to 0.
            cumSum[0] = 0;

            // Iterate through the items and calculate the cumulative sums.
            int i = 1;
            while (i <= N)
            {
                cumSum[i] = (cumSum[i - 1] + items[i - 1]) % M;

                // If the current cumulative sum is divisible by M, return the subsequence.
                if (cumSum[i] == 0)
                {
                    int[] subsequence = new int[i];
                    Array.Copy(items, 0, subsequence, 0, i);
                    return subsequence;
                }

                i++;
            }

            // Create a boolean array to store if a remainder has been seen before.
            bool[] seenRemainder = new bool[M];
            int j = 0;
            while (j < seenRemainder.Length)
            {
                seenRemainder[j] = false;
                j++;
            }

            // Iterate through the cumulative sums and check for divisibility.
            i = 1;
            while (i <= N)
            {
                if (seenRemainder[cumSum[i]])
                {
                    // Find the first occurrence of the remainder.
                    int k = 0;
                    while (k < i)
                    {
                        if (cumSum[k] == cumSum[i])
                        {
                            break;
                        }
                        k++;
                    }

                    // Return the subsequence between those indices.
                    int[] subsequence = new int[i - k];
                    Array.Copy(items, k, subsequence, 0, i - k);
                    return subsequence;
                }
                else
                {
                    seenRemainder[cumSum[i]] = true;
                }

                i++;
            }

            // No subsequence found, return null.
            return null;
        }





        #endregion
        #endregion

    }
}
