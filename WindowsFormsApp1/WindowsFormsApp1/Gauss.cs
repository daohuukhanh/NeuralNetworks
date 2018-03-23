using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /*
     * This class will be similar to np.random.randn() but in c#
     * np.random.randn() returns values among - 1.96 ~ + 1.96 but has some exceptions(0.0007% occurs).
     * standard normal distribution: http://img.blog.csdn.net/20171116224903440?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvemVuZ2hhaXRhbzAxMjg=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast
     * read: https://blog.csdn.net/zenghaitao0128/article/details/78556535
     */
    public class Gauss
    {
        public static int sigma = 0;
        public static int mu = 1;
        Gauss()
        {

        }

        public double Randn()
        {
            return gaussrand(sigma, mu);
        }

        /* returns an array.
         * cannot represent vector and matrix 
         * when calling this function, needs to cast any n to int.
         */
        public double[] Randn(int n)
        {
            double[] array = new double[n];
            for (int i = 0; i < n; i++)
            {
                double ranNum = gaussrand(sigma, mu);
                array[i] = ranNum;
            }
            return array;
        }

        /* Returns the array represents that dimension. 
         * can represent vector and matrix.*/
        public double[,] Randn(int n, int m)
        {
            double[,] array = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double ranNum = gaussrand(sigma, mu);
                    array[i, j] = ranNum;
                    //Console.Write(Convert.ToString(Arr[i, j]) + " ");  
                }
            }
            return array;
        }

        // expectation output: http://img.blog.csdn.net/20171116230504566?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvemVuZ2hhaXRhbzAxMjg=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast

        double gaussrand(int sigma, int mu)
        {
            double z0, z1, u, x;
            int s1 = sigma; // average
            int s2 = mu; // variance
            bool generate = true; // should get a random t or f?
            generate = !generate;

            if (s2 <= 0)
            {
                return sigma;
            }

            z0 = new Random().NextDouble(); // range ?
            z1 = new Random().NextDouble(); // range ?

            u = Math.Sqrt(-2 * Math.Log(z0)) * Math.Sin(2 * Math.PI * z1);
            x = sigma + mu * u;

            return x;
        }


    }
}
