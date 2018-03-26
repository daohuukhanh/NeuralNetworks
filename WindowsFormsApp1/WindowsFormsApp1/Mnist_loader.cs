using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Mnist_loader
    {
        PythonFunc pyc;

        /*
         * return the MNIST data as a tuple containing the training data,
         * the validation data, and the test data.
         * */
        public load_data()
        {
            //var b = Tuple.Create<T1, T2, T3>(T1, T2, T3)
            //var a = Tuple.Create(1, 2, 3);
            int[,] pixels = new int[28, 28];
            int[] firstEntryDigit;
            int tuple1 = new Tuple<int[,], int>(pixels, firstEntryDigit);





        }

        public load_data_wrapper()
        {

        }

        /*
         * Return a 10-dimensional unit vector with a 1.0 in the jth
         * position and zeroes elsewhere.  This is used to convert a digit
         * (0...9) into a corresponding desired output from the neural
         * network
         */
        public float[,] Vectorized_result(int j) // unit vector type??
        {
            int z0 = j;
            float[,] e;
            e = pyc.Zeros(10, 1);
            e[z0] = 1.0;

            return e;
        }
    }
}
