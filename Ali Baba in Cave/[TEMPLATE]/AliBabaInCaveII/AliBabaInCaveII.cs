using System;
using System.Collections;
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
    public static class AliBabaInCaveII
    {

        #region your code is here

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given the Camels possible load and N items, each with its weight, profit and number of instances, 
        /// Calculate the max total profit that can be carried within the given camels' load
        /// </summary>
        /// <param name="camelsLoad">max load that can be carried by camels</param>
        /// <param name="itemsCount">number of items</param>
        /// <param name="weights">weight of each item [ONE-BASED array]</param>
        /// <param name="profits">profit of each item [ONE-BASED array]</param>
        /// <param name="instances">number of instances for each item [ONE-BASED array]</param>
        /// <returns>Max total profit</returns>
        /// 

        public static int SolveValue(int maxLoad, int itemCount, int[] itemWeights, int[] itemProfits, int[] itemInstances)
        {
            // Initialize the dp table
            int[] no_dpTable = new int[maxLoad + 1];

            // Iterate over all item types
            for (int i = 1; i <= itemCount; i++)
            {
                int nour_weight = itemWeights[i];
                int nour_profit = itemProfits[i];
                int nour_instance = itemInstances[i];

                // Iterate over all possible weights
                int w = maxLoad;

                while (w >= nour_weight)
                {
                    // Calculate the maximum number of instances that can be taken for this weight
                    int maxInstances = Math.Min(nour_instance, w / nour_weight);

                    // Update the dp table
                    for (int k = 1; k <= maxInstances; k++)
                    {
                        switch (k)
                        {
                            case 1:
                                no_dpTable[w] = Math.Max(no_dpTable[w], no_dpTable[w - nour_weight] + nour_profit);
                                break;
                            case 2:
                                no_dpTable[w] = Math.Max(no_dpTable[w], no_dpTable[w - 2 * nour_weight] + 2 * nour_profit);
                                break;
                            // Add more cases as needed for higher values of k
                            default:
                                no_dpTable[w] = Math.Max(no_dpTable[w], no_dpTable[w - k * nour_weight] + k * nour_profit);
                                break;
                        }
                    }

                    w--;
                }
            }

            return no_dpTable[maxLoad];
        }



        #endregion

        #region Get Taken Instance
        private static int GettakenInsta(int i, int remainL, int[,] maxProfit, int[] weights, int[] profits, int[] instances)
        {
            int ins = 0;
            int k = 1;
            do
            {
                if (maxProfit[i, remainL] == maxProfit[i - 1, remainL - k * weights[i]] + k * profits[i])
                {
                    ins = k;
                }
                k++;

            } while (k <= instances[i] && k * weights[i] <= remainL);

            return ins;
        }

        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>Tuple array of the selected items to get MAX profit (stored in Tuple.Item1) together with the number of instances taken from each item (stored in Tuple.Item2)
        /// OR NULL if no items can be selected</returns>
        /// 

        public static Tuple<int, int>[] ConstructSolution(int camelsLoad, int itemsCount, int[] weights, int[] profits, int[] instances)
        {
            List<Tuple<int, int>> nour_final = new List<Tuple<int, int>>();
            int[,] dpMax = Calc_MAX(camelsLoad, itemsCount, weights, profits, instances);

            for (int i = itemsCount; i > 0; i--)
            {
                if (dpMax[i, camelsLoad] != dpMax[i - 1 , camelsLoad])
                {
                    int insta = GettakenInsta(i, camelsLoad, dpMax, weights, profits, instances);
                    nour_final.Add(Tuple.Create(i, insta));
                    camelsLoad -= insta * weights[i];
                }
            }

            Tuple<int, int>[] res = nour_final.ToArray();
            return res;
        }



        #endregion

        #region Get Max Profit

        private static int[,] Calc_MAX(int camelsLoad, int itemsCount, int[] weights, int[] profits, int[] instances)
        {
            int[,] Max_Profit = new int[itemsCount + 1, camelsLoad + 1];

            for (int i = 1; i <= itemsCount; i++)
            {
                for (int j = 1; j <= camelsLoad; j++)
                {
                    int max = Max_Profit[i - 1, j];

                    for (int k = 1; k <= instances[i] && k * weights[i] <= j; k++)
                    {
                        switch (k)
                        {
                            case 1:
                                {
                                    int Max_t = Max_Profit[i - 1, j - weights[i]] + profits[i];
                                    if (Max_t > max)
                                    {
                                        max = Max_t;
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    int Max_t = Max_Profit[i - 1, j - 2 * weights[i]] + 2 * profits[i];
                                    if (Max_t > max)
                                    {
                                        max = Max_t;
                                    }
                                    break;
                                }
                            // Add more cases as needed for higher values of k
                            default:
                                {
                                    int Max_t = Max_Profit[i - 1, j - k * weights[i]] + k * profits[i];
                                    if (Max_t > max)
                                    {
                                        max = Max_t;
                                    }
                                    break;
                                }
                        }
                    }

                    Max_Profit[i, j] = max;
                }
            }

            return Max_Profit;
        }


        #endregion

        #endregion

    }
}
