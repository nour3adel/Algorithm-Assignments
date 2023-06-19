using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class IntegerMultiplication
    {
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            Array.Reverse(X);
            Array.Reverse(Y);

            if (X.Length > Y.Length)
            {
                Y = appendZero(Y, X.Length - Y.Length);
            }
            else
            {
                X = appendZero(X, Y.Length - X.Length);
            }

            int Final_Digits = N * 2;
            byte[] result = Karatsuba(X, Y);
            if (Final_Digits > result.Length)
            {
                result = appendZero(result, Final_Digits - result.Length);

            }
            else if (Final_Digits < result.Length)
            {
                result = removeZero(result);
            }

            Array.Reverse(result);
            return result;
        }
        //# X × Y = 10N × B×D + 10N/2 × (B×C + A×D) + A×C                                        Divide and Conquer ( SAME AS NAÏVE )  ❎
        // TODO (p2 × 10 ^ (n)) + ((p3 - p1 - p2) × 10 ^ (n/2) + p1                                   Karatsuba  ✅

        #region Karatsuba
        static public byte[] Karatsuba(byte[] x, byte[] y)
        {
            // Standard Multiplication if Two numbers are small
            if (x.Length <= 128 || y.Length <= 128)
            {
                return multiplyXY(x, y);
            }

            // Kartsuba 

            //make number of digits same in both X & Y
            if (x.Length > y.Length)
            {
                y = appendZero(y, x.Length - y.Length);
            }
            else
            {
                x = appendZero(x, y.Length - x.Length);
            }

            //Make Two Numbers Even
            if (x.Length % 2 != 0)
            {
                x = appendZero(x, 1);
                y = appendZero(y, 1);
            }

            int mid = x.Length / 2;
            byte[] x_left = new byte[mid];
            Array.Copy(x, 0, x_left, 0, mid);
            byte[] x_right = new byte[mid];
            Array.Copy(x, mid, x_right, 0, mid);
            byte[] y_left = new byte[mid];
            Array.Copy(y, 0, y_left, 0, mid);
            byte[] y_right = new byte[mid];
            Array.Copy(y, mid, y_right, 0, mid);
            int n = x.Length;

            byte[] p1 = null, p2 = null, p3 = null;

     
            var tasksPlists = new List<Task>{
                    Task.Run(() => p1 = Karatsuba(x_left, y_left)),
                    Task.Run(() => p2 = Karatsuba(x_right, y_right)),
                    Task.Run(() => p3 = Karatsuba(addXY(x_left, x_right), addXY(y_left, y_right)))
                };
            Task.WaitAll(tasksPlists.ToArray());
            byte[] sub1 = subtractXY(p3, p1);
            byte[] sub2 = subtractXY(sub1, p2);
            byte[] mul1 = multiply10(sub2, n / 2);
            byte[] mul2 = multiply10(p1, n);
            byte[] add1 = addXY(mul2, mul1);
            byte[] Final = addXY(add1, p2);

            return Final;
        }

        #endregion

        #region Append Zero
        static public byte[] appendZero(byte[] bytes, int count)
        {
            if (count <= 0)
            {
                return bytes;
            }
            byte[] newBytes = new byte[bytes.Length + count];
            Array.Copy(bytes, 0, newBytes, count, bytes.Length);
            return newBytes;
        }
        #endregion

        #region Standard Multiply
        public static byte[] multiplyXY(byte[] X, byte[] Y)
        {
            int N = X.Length + Y.Length;
            byte[] result = new byte[N];

            for (int i = X.Length - 1; i >= 0; i--)
            {
                byte carry = 0;
                for (int j = Y.Length - 1; j >= 0; j--)
                {
                    int prod = X[i] * Y[j] + carry + result[i + j + 1];
                    carry = (byte)(prod / 10);
                    result[i + j + 1] = (byte)(prod % 10);
                }
                result[i] += carry;
            }

            return result;
        }
        #endregion

        #region Addition
        static public byte[] addXY(byte[] x, byte[] y)
        {
            if (x.Length > y.Length)
            {
                y = appendZero(y, x.Length - y.Length);
            }
            else
            {
                x = appendZero(x, y.Length - x.Length);
            }

            byte carry = 0;
            byte[] result = new byte[x.Length];
            for (int i = x.Length - 1; i >= 0; i--)
            {
                byte sum = (byte)(x[i] + y[i] + carry);

                if (sum > 9)
                {
                    result[i] = (byte)(sum % 10);
                    carry = 1;
                }
                else
                {
                    result[i] = sum;
                    carry = 0;
                }
            }

            if (carry != 0)
            {
                byte[] newResult = new byte[x.Length + 1];
                newResult[0] = carry;
                Array.Copy(result, 0, newResult, 1, result.Length);
                result = newResult;
            }

            return result;
        }

        #endregion

        #region Substraction
        public static byte[] subtractXY(byte[] x, byte[] y)
        {

            if (x.Length > y.Length)
            {
                y = appendZero(y, x.Length - y.Length);
            }
            else
            {
                x = appendZero(x, y.Length - x.Length);
            }


            byte[] result = new byte[x.Length];
            int borrow = 0, a, b;
            for (int i = x.Length - 1; i >= 0; i--)
            {
                a = x[i];
                b = y[i];
                if (borrow == 1)
                {
                    a = a - 1;
                }
                if (a >= b)
                {

                    result[i] = (byte)(a - b);
                    borrow = 0;
                }
                else
                {
                    a = a + 10;
                    result[i] = (byte)(a - b);
                    borrow = 1;
                }
            }
            return result;
        }

        #endregion

        #region Removing Leading Zero
        public static byte[] removeZero(byte[] bytes)
        {
            if (bytes[0] != 0)
            {
                return bytes;
            }
            else
            {
                byte[] newBytes = new byte[bytes.Length - 1];
                Array.Copy(bytes, 1, newBytes, 0, newBytes.Length);
                return newBytes;
            }
        }

        #endregion

        #region Multiply By 10^N
        static public byte[] multiply10(byte[] bytes, int count)
        {

            byte[] newBytes = new byte[bytes.Length + count];
            Array.Copy(bytes, newBytes, bytes.Length);
            return newBytes;
        }

        #endregion
    }
}
