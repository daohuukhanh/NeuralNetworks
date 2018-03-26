using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class PythonFunc
    {
        public PythonFunc()
        {




        }

        public int[,] Reshape(int[,] mat, int z0, int z1)
        {
            int x = z0;
            int y = z1;
            int[,] matr = mat;

            int count = 0;
            int[] array = new int[784];
            int[,] newMatrix = new int[x, y];

            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    array[count] = matr[i, j];
                    count++;
                }
            }
            count = 0;


            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    newMatrix[i, j] = array[count];
                    count++;
                }
            }

            return newMatrix;
        }

        //public int[,] Zeros(int z0, int z1)
        //{
        //    int x = z0;
        //    int y = z1;
        //    int[,] zeroarray = new int[x, y];

        //    for (int i = 0; i < x; i++)
        //    {
        //        for (int j = 0; j < y; j++)
        //        {
        //            zeroarray[i, j] = 0;
        //        }
        //    }

        //    return zeroarray;
        //}

        //public float[,] Zeros(int z0, int z1)
        //{
        //    int x = z0;
        //    int y = z1;
        //    float[,] zeroarray = new float[x, y];

        //    for (int i = 0; i < x; i++)
        //    {
        //        for (int j = 0; j < y; j++)
        //        {
        //            zeroarray[i, j] = 0;
        //        }
        //    }

        //    return zeroarray;
        //}

        public float[] Zeros(int z0, int z1)
        {
            int x = z0;
            int y = z1;
            float[] zeroarray = new float[x];

            for (int i = 0; i < x; i++)
            {
                zeroarray[i] = 0;
            }

            return zeroarray;
        }


        public double Exponential(double z0)
        {
            double x = z0;
            double e = 2.718281;

            return Math.Pow(e, x);
        }

        public int Argmax(int[,] matrix0, int z0)
        {
            int[,] mat = matrix0;
            int x = z0;

            // needs to be implemented...
        }

        public int[,] Transpose(int[,] mat)
        {
            int[,] matrix = mat;
            int[,] newMat = new int[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    newMat[j, i] = matrix[i, j];
                }
            }

            return newMat;
        }
    }
}
