using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Utilities
{
    public static class ArrayUtilities
    {
        public static double ArrayMagnitude(double[] a)
        {
            return Math.Sqrt(Math.Pow(a[0], 2) + Math.Pow(a[1], 2) + Math.Pow(a[2], 2));
        }

        public static double[] ArraySubtract(double[] a, double[] b)
        {
            double[] ret = new double[3];

            ret[0] = a[0] - b[0];
            ret[1] = a[1] - b[1];
            ret[2] = a[2] - b[2];

            return ret;
        }

        public static double[] ArrayMultiply(double[] a, double scalar)
        {
            double[] ret = new double[3];

            ret[0] = a[0] * scalar;
            ret[1] = a[1] * scalar;
            ret[2] = a[2] * scalar;

            return ret;
        }

        // cross product of two 3-element arrays
        public static double[] ArrayCross(double[] a, double[] b)
        {
            double[] ret = new double[3];

            ret[0] = a[1] * b[2] - a[2] * b[1];
            ret[1] = a[2] * b[0] - a[0] * b[2];
            ret[2] = a[0] * b[1] - a[1] * b[0];

            return ret;
        }

        // dot product of two 3-element arrays
        public static double ArrayDot(double[] a, double[] b)
        {
            return (a[0] * b[0]) + (a[1] * b[1]) + (a[2] * b[2]);
        }

        // angle between two 3-element arrays representing vectors
        public static double ArrayAngleBetween(double[] a, double[] b)
        {
            return Math.Acos(ArrayDot(a, b) / (ArrayMagnitude(a) * ArrayMagnitude(b)));
        }

        // angle between two 3-element arrays in degrees
        public static double ArrayAngleBetweenDegrees(double[] a, double[] b)
        {
            return ArrayAngleBetween(a, b) * 180.0 / Math.PI;
        }
      public static double[,] MatrixMult(double[,] a, double[,] b)
      {
         int rows = a.GetLength(0);
         int col = b.GetLength(1);
         double[,] c = new double[rows, col];
         if (a.GetLength(1) == b.GetLength(0))
         {
            for (int i = 0; i < rows; i++)
            {
               for (int j = 0; j < col; j++)
               {
                  double sum = 0;
                  for (int k = 0; k < b.GetLength(0); k++)
                  {
                     sum = sum + a[i, k] * b[k, j];
                  }
                  c[i, j] = sum;
               }
            }
         }
         return c;



      }
      public static double[,] Transpose(double[,] a)
      {
         double[,] b = new double[a.GetLength(1), a.GetLength(0)];
         for (int i = 0; i < a.GetLength(0); i++)
         {
            for (int j = 0; j < a.GetLength(1); j++)
            {
               b[j, i] = a[i, j];
            }
         }
         return b;
      }

      public static double[,] Array1DTo2D(double[] a, int r, int c)
      {
         double[,] b = new double[r, c];
         for (int i = 0; i < r; i++)
         {
            for (int j = 0; j < c; j++)
            {
               b[i, j] = a[i * c + j];
            }
         }
         return b;
      }

      public static void SetRow(double[,] dest, int r, double[] src)
      {
         if (src.Length != dest.GetLength(1))
         {
            throw new ArgumentException("Row length mismatch");
         }

         for (int c = 0; c < src.Length; c++)
         {
            dest[r, c] = src[c];
         }
      }

      public static double[] GetRow(double[,] array, int row)
      {
         int cols = array.GetLength(1);
         double[] result = new double[cols];
         Buffer.BlockCopy(array, row * cols * sizeof(double), result, 0, cols * sizeof(double));
         return result;
      }

      public static void SetRow(int[,] dest, int r, int[] src)
      {
         if (src.Length != dest.GetLength(1))
         {
            throw new ArgumentException("Row length mismatch");
         }

         for (int c = 0; c < src.Length; c++)
         {
            dest[r, c] = src[c];
         }
      }

      public static void SetRow(byte[,] dest, int r, byte[] src)
      {
         if (src.Length != dest.GetLength(1))
         {
            throw new ArgumentException("Row length mismatch");
         }

         for (int c = 0; c < src.Length; c++)
         {
            dest[r, c] = src[c];
         }
      }
   }

   public static class MatrixExtensions
   {
      public static T[] GetRow<T>(this T[,] matrix, int row)
      {
         var rowLength = matrix.GetLength(1);
         var rowVector = new T[rowLength];

         for (var i = 0; i < rowLength; i++)
            rowVector[i] = matrix[row, i];

         return rowVector;
      }
      public static T[,] Up3x3<T>(this T[,] matrix)
      {
         int rows = matrix.GetLength(0);
         int cols = matrix.GetLength(1);
         var up3x3 = new T[3, 3];
         if (rows > 2 && cols > 2)
         {
            for (var i = 0; i < 3; i++)
               for (int j = 0; j < 3; j++)
                  up3x3[i, j] = matrix[j, i];
         }


         return up3x3;
      }
      public static T[,] Low3x3<T>(this T[,] matrix)
      {
         int rows = matrix.GetLength(0);
         int cols = matrix.GetLength(1);
         var low3x3 = new T[rows, cols];
         if (rows > 2 && cols > 2)
         {
            for (var i = 3; i < 6; i++)
               for (int j = 3; j < 6; j++)
                  low3x3[i, j] = matrix[j, i];
         }


         return low3x3;
      }
   }

   

    
}
