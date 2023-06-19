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
    public static class AliBabaAndNHouses
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the maximum amount of money that Ali baba can get, given the number of houses (N) and a list of the net gained value for each consecutive house (V)
        /// </summary>
        /// <param name="values">Array of the values of each given house (ordered by their consecutive placement in the city)</param>
        /// <param name="N">The number of the houses</param>
        /// <returns>the maximum amount of money the Ali Baba can get </returns>
        public static int SolveValue(int[] values, int N)
        {
            // BASE CASE
            if (values == null || N == 0)
            {
                return 0;
            }

            if (N == 1)
            {
                return values[0];
            }

            int[] osamaList = new int[N];
            osamaList[0] = values[0];
            osamaList[1] = Math.Max(values[0], values[1]);

            int indi = 2;

            while (indi < N)
            {
                switch (indi)
                {
                    //if N == 2
                    case 2:
                        osamaList[indi] = Math.Max(osamaList[indi - 2] + values[indi], osamaList[indi - 1]);
                        break;
                    //if N > 2
                    default:
                        osamaList[indi] = Math.Max(osamaList[indi - 2] + values[indi], osamaList[indi - 1]);
                        break;
                }

                indi++;
            }

            return osamaList[N - 1];
        }

        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>Array of the indices of the robbed houses (1-based and ordered from left to right) </returns>
        public static int[] ConstructSolution(int[] values, int N)
        {

            switch (N)
            {
                case 0: 
                        return new int[0];
                   
                case 1:
                        return new int[] { 1 };
                
                case 2:
                        if (values[0] > values[1])
                        {
                            return new int[] { 1 };
                        }
                        else
                        {
                            return new int[] { 2 };
                        }
                   

                default:
                        int[] osos_list = osos_listCompute(values, N);
                        bool[] picked = pickIndice_Compute(osos_list, values, N);

                        int[] finaaal = new int[N];
                        int idx1 = N - 1;
                        int idx2 = 0;

                        while (idx1 >= 0)
                        {
                            if (picked[idx1])
                            {
                                finaaal[idx2] = idx1 + 1;
                                idx2++;
                                idx1 -= 2;
                            }
                            else
                            {
                             idx1--;
                            }
                        }

                        Array.Resize(ref finaaal, idx2);
                        return finaaal;
                  
            }
           
        }

        #region Dynamic Programmig Calcualting (OSOS_LIST)
        public static int[] osos_listCompute(int[] values, int N)
        {
            int[] osos_list = new int[N];
            osos_list[0] = values[0];
            osos_list[1] = Math.Max(values[0], values[1]);

            int f = 2;
            while (f < N)
            {
                switch (f)
                {
                    case 2:
                        osos_list[f] = Math.Max(osos_list[f - 2] + values[f], osos_list[f - 1]);
                        break;

                    default:
                        osos_list[f] = Math.Max(osos_list[f - 2] + values[f], osos_list[f - 1]);
                        break;
                }

                f++;
            }

            return osos_list;
        }
        #endregion

        #region Chosen Indices
        public static bool[] pickIndice_Compute(int[] osos_list, int[] values, int N)
        {
            bool[] picked = new bool[N];
            picked[0] = true;

            if (osos_list[1] == values[1])
            {
                picked[1] = true;
            }

            int o = 2;
            while (o < N)
            {
                switch (o)
                {
                    case 2:
                        if (osos_list[o] == osos_list[o - 2] + values[o])
                        {
                            picked[o] = true;
                        }
                        break;


                    default:
                        if (osos_list[o] == osos_list[o - 2] + values[o])
                        {
                            picked[o] = true;
                        }
                        break;
                }

                o++;
            }

            return picked;
         
        }
        #endregion


        #endregion

        #endregion

    }
}
