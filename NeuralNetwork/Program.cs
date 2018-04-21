using System;
using System.Windows.Forms;

namespace NeuralNetwork
{
    class Program
    {        
        [STAThread]
        static void Main(string[] args)
        {           
            Application.Run(new Problems.Form1());

            //Problems.HandwrittenDigits.Run();            
            //Console.ReadLine();
        }
    }
}
