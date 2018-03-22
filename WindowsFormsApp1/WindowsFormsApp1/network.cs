using System; // include Random library
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; // include array
using System.Collections.Generic;

/* https://en.wikipedia.org/wiki/NumPy */

namespace WindowsFormsApp1
{
    /*  
     *  A module to implement the stochastic gradient descent learning
     *  algorithm for a feedforward neural network.Gradients are calculated
     *  using backpropagation.  Note that I have focused on making the code
     *  simple, easily readable, and easily modifiable.It is not optimized,
     *  and omits many desirable features. 
     *  **/
    class Network
    {
        private int num_layers;
        private List<int> sizes; // contains the number of neurons in the respective layers of the network.
        private int biases; // randomly initialized
        private int weights; // randomly initialized

        public Network(int num, List<int> size)
        {
            this.num_layers = num;
            this.sizes = size;
            //this.biases = bias;
            //this.weights = wh;
        }

        public int GetRandomNum(int n) // implementing Gauss...
        {
            //private int num;


            //return num;
        }
    }
}