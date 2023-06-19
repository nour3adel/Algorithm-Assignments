using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class MatrixMultiplication
    {

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        public static int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {
            if (N <= 500)
            {
                return StandardAlgorithm(M1, M2, N);
            }
            else
            {
                return StrassensAlgorithm(M1, M2, N);
            }
        }

        #region Standard Multiplication
        private static int[,] StandardAlgorithm(int[,] M1, int[,] M2, int N)
        {
            int[,] result = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int k = 0; k < N; k++)
                {
                    int firstMultiplier = M1[i, k];
                    int j = 0;
                    while (j < N - 4)
                    {
                        result[i, j] += firstMultiplier * M2[k, j];
                        result[i, j + 1] += firstMultiplier * M2[k, j + 1];
                        result[i, j + 2] += firstMultiplier * M2[k, j + 2];
                        result[i, j + 3] += firstMultiplier * M2[k, j + 3];
                        j += 4;
                    }
                    while (j < N)
                    {
                        result[i, j] += firstMultiplier * M2[k, j];
                        j++;
                    }
                }
            }

            return result;
        }
        #endregion

        #region Strassens Algorithm
        private static int[,] StrassensAlgorithm(int[,] M1, int[,] M2, int N)
        {
            int Nos = N / 2;
            int[,] A11 = new int[Nos, Nos];
            int[,] A12 = new int[Nos, Nos];
            int[,] A21 = new int[Nos, Nos];
            int[,] A22 = new int[Nos, Nos];
            int[,] B11 = new int[Nos, Nos];
            int[,] B12 = new int[Nos, Nos];
            int[,] B21 = new int[Nos, Nos];
            int[,] B22 = new int[Nos, Nos];
            //Split into submatrices
            SplitSubmatrices(M1, M2, Nos, ref A11, ref A12, ref A21, ref A22, ref B11, ref B12, ref B21, ref B22);               
            //Conquer
            // compute submatrices recursively in parallel 
            int[,] P1 = null, P2 = null, P3 = null, P4 = null, P5 = null, P6 = null, P7 = null;
            Parallel.For(0, 7, i =>
            {
                switch (i)
                {
                    case 0:
                        P1 = MatrixMultiply(A11, MatrixSubstract(B12, B22, Nos), Nos);
                        break;
                    case 1:
                        P2 = MatrixMultiply(MatrixAddition(A11, A12, Nos), B22, Nos);
                        break;
                    case 2:
                        P3 = MatrixMultiply(MatrixAddition(A21, A22, Nos), B11, Nos);
                        break;
                    case 3:
                        P4 = MatrixMultiply(A22, MatrixSubstract(B21, B11, Nos), Nos);
                        break;
                    case 4:
                        P5 = MatrixMultiply(MatrixAddition(A11, A22, Nos), MatrixAddition(B11, B22, Nos), Nos);
                        break;
                    case 5:
                        P6 = MatrixMultiply(MatrixSubstract(A12, A22, Nos), MatrixAddition(B21, B22, Nos), Nos);
                        break;
                    case 6:
                        P7 = MatrixMultiply(MatrixSubstract(A11, A21, Nos), MatrixAddition(B11, B12, Nos), Nos);
                        break;
                }
            });

            //Combine  
            int[,] C11 = MatrixSubstract(MatrixAddition(MatrixAddition(P5, P4, Nos), P6, Nos), P2, Nos);
            int[,] C12 = MatrixAddition(P1, P2, Nos);
            int[,] C21 = MatrixAddition(P3, P4, Nos);
            int[,] C22 = MatrixSubstract(MatrixSubstract(MatrixAddition(P5, P1, Nos), P3, Nos), P7, Nos);
            int[,] result = new int[N, N];
            // combine submatrices into result
            CombineSubmatricesintoResult(C11, C12, C21, C22, ref result, Nos);
            return result;
        }

        #endregion

        #region Split matrices 
        private static void SplitSubmatrices(int[,] M1, int[,] M2, int Nos, ref int[,] A11, ref int[,] A12, ref int[,] A21, ref int[,] A22, ref int[,] B11, ref int[,] B12, ref int[,] B21, ref int[,] B22)
        {
            int size = Nos * 2;
            for (int i = 0; i < Nos; i++)
            {
                Array.Copy(M1, i * size, A11, i * Nos, Nos);
                Array.Copy(M1, i * size + Nos, A12, i * Nos, Nos);
                Array.Copy(M1, (i + Nos) * size, A21, i * Nos, Nos);
                Array.Copy(M1, (i + Nos) * size + Nos, A22, i * Nos, Nos);

                Array.Copy(M2, i * size, B11, i * Nos, Nos);
                Array.Copy(M2, i * size + Nos, B12, i * Nos, Nos);
                Array.Copy(M2, (i + Nos) * size, B21, i * Nos, Nos);
                Array.Copy(M2, (i + Nos) * size + Nos, B22, i * Nos, Nos);
            }
        }


        #endregion

        #region Combine matrices
        private static void CombineSubmatricesintoResult(int[,] C11, int[,] C12, int[,] C21, int[,] C22, ref int[,] result, int Nos)
        {
            for (int i = 0; i < Nos; i++)
            {
                // Copy C11
                Array.Copy(C11, i * Nos, result, i * 2 * Nos, Nos);

                // Copy C12
                Array.Copy(C12, i * Nos, result, (i * 2 + 1) * Nos, Nos);

                // Copy C21
                Array.Copy(C21, i * Nos, result, ((i + Nos) * 2) * Nos, Nos);

                // Copy C22
                Array.Copy(C22, i * Nos, result, ((i + Nos) * 2 + 1) * Nos, Nos);
            }
        }


        #endregion

        #region substract MaTrices
        public static int[,] MatrixSubstract(int[,] M1, int[,] M2, int N)
        {
            int[,] result = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result[i, j] = M1[i, j] - M2[i, j];
                }
            }
            return result;
        }
        #endregion     

        #region Add Matrices
        public static int[,] MatrixAddition(int[,] M1, int[,] M2, int N)
        {
            int[,] result = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result[i, j] = M1[i, j] + M2[i, j];
                }
            }
            return result;
        }
        #endregion

    }
}