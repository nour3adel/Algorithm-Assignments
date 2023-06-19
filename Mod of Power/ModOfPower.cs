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
    public static class ModOfPow
    {
        #region YOUR CODE IS HERE
        /// <summary>
        /// Calculate Mod of Power (B^P mod M) in an efficient way
        /// </summary>
        /// <param name="B">Base</param>
        /// <param name="P">Power</param>
        /// <param name="M">Modulus</param>
        /// <returns>Result of B^P mod M </returns>
        public static long ModOfPower(long B, long P, long M)
        {
            long result = 1;
            B %= M;

            while (P > 0)
            {
                if ((P & 1) == 1)
                {
                    result = (result * B) % M;
                }
                P >>= 1;
                B = (B * B) % M;
            }

            return result;
        }

        #endregion
    }
}
