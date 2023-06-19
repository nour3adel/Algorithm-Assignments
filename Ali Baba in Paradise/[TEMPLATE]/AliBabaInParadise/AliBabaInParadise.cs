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
    public static class AliBabaInParadise
    {
        #region YOUR CODE IS HERE
        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given the Camels maximum load and N items, each with its weight and profit 
        /// Calculate the max total profit that can be carried within the given camels' load
        /// </summary>
        /// <param name="camelsLoad">max load that can be carried by camels</param>
        /// <param name="itemsCount">number of items</param>
        /// <param name="weights">weight of each item</param>
        /// <param name="profits">profit of each item</param>
        /// <returns>Max total profit</returns>
        public static int SolveValue(int camelsLoad, int itemsCount, int[] weights, int[] profits)
        {
            int[] saha = new int[camelsLoad + 1];
            int  saha_idx = 0;
            while (saha_idx <= camelsLoad)
            {
                int saha_idx2 = 0;
                while (saha_idx2 < itemsCount)
                {
                    if (weights[saha_idx2] <= saha_idx)
                    {
                        saha[saha_idx] = Math.Max(saha[saha_idx], saha[saha_idx - weights[saha_idx2]] + profits[saha_idx2]);
                    }
                    saha_idx2++;
                }
                saha_idx++;
            }
            return saha[camelsLoad];
        }


        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>Tuple array of the selected items to get MAX profit (stored in Tuple.Item1) together with the number of instances taken from each item (stored in Tuple.Item2)
        /// OR NULL if no items can be selected</returns>
        public static Tuple<int, int>[] ConstructSolution(int camelsLoad, int itemsCount, int[] weights, int[] profits)
        {

            int[] saha_list = new int[camelsLoad + 1];
            int[] saha_choose = new int[camelsLoad + 1];
            for (int i = 0; i <= camelsLoad; i++)
            {
                for (int j = 0; j < itemsCount; j++)
                {
                    if (weights[j] <= i)
                    {
                        int newProfit = saha_list[i - weights[j]] + profits[j];
                        if (newProfit > saha_list[i])
                        {
                            saha_list[i] = newProfit;
                            saha_choose[i] = j + 1;
                        }
                    }
                }
            }
            if (saha_list[camelsLoad] == 0)
            {
                return null;
            }
            else
            {
                Dictionary<int, int> solutionDict = new Dictionary<int, int>();
                int currentLoad = camelsLoad;
                while (currentLoad > 0)
                {
                    int selectedItem = saha_choose[currentLoad] - 1;
                    if (selectedItem >= 0)
                    {
                        int count = 1;
                        int remainingLoad = currentLoad - weights[selectedItem];
                        while (remainingLoad >= 0 && saha_choose[remainingLoad] == saha_choose[currentLoad])
                        {
                            count++;
                            remainingLoad -= weights[selectedItem];
                        }
                        solutionDict[selectedItem + 1] = count;
                        currentLoad = remainingLoad;
                    }
                    else
                    {
                        break;
                    }
                }
                Tuple<int, int>[] solutionArray = solutionDict.Select(kv => new Tuple<int, int>(kv.Key, kv.Value)).ToArray();
                return solutionArray;
            }
        }




        #endregion

        #endregion
    }
}
