using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /*
     * A C# format of numpy.dot() in Python.
     * Dot of two arrays.
     */
    class Dot
    {
        Dot()
        {

        }

        public double[,] mulMat(double[,] array1, double[,] array2)
        {
            double[,] z0 = array1;
            double[,] z1 = array2;
            
            int new_row = array1.GetLength(1);
            int row2 = array2.GetLength(1);

            double[,] z2 = new double[array1.Length, row2];

            for (int i = 0; i < array1.Length; i++)
            {
                for(int j = 0; j < row2; j++)
                {
                    for(int k = 0; k < new_row; k++)
                    {
                        z2[i,j] = array1[i, k] * array2[k,j];
                    }
                    
                }
            }
            return z2;
        }
    }
}
