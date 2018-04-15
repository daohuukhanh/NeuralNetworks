using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NeuralNetwork.Problems
{
    public static class HandwrittenDigits
    {
        internal static bool success = true;
        internal static NeuralNetwork network;

        public static void Run()
        {
            Console.WriteLine("Creating neural network...");

            network = new NeuralNetwork(756, 300, 3, 5, 10.0);

            String[] dataset = null;

            try
            {
                dataset = File.ReadAllLines("TrainingData.csv");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No train data found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Console.WriteLine(ex.Message);
                Console.WriteLine();
                success = false;
            }

            if (success)
            {
                var allInputs = dataset
                    .Select(x => x.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

                //remove the first line which contain texts
                string[][] validInputs = new string[allInputs.Length - 1][];

                for (int i = 0; i < allInputs.Length - 1; i++)
                {
                    validInputs[i] = new string[allInputs[0].Length];

                    for (int j = 0; j < allInputs[0].Length; j++)
                        validInputs[i][j] = allInputs[i + 1][j];
                }

                for (int epoch = 0; epoch < network._numEpoch; epoch++)
                {
                    Console.WriteLine($"Training network with {validInputs.Length} samples...");

                    var normalizedInputs = validInputs.Select(x => new
                    {
                        Answer = x[0],
                        Inputs = NormalizeInput(x.Skip(1).ToArray())
                    }).ToArray();

                    var s = new Stopwatch();
                    s.Start();

                    foreach (var input in normalizedInputs)
                    {
                        var targets = Enumerable.Range(0, 3).Select(x => 0.0).Select(x => x + 0.01).ToArray();
                        targets[int.Parse(input.Answer)] = 0.99;

                        network.Train(input.Inputs, targets);
                    }
                    s.Stop();

                    Console.WriteLine($"Training complete in {s.ElapsedMilliseconds}ms{Environment.NewLine}");
                    Console.WriteLine("Querying network...");

                    var queryDataset = File.ReadAllLines("TrainingData.csv"); ;

                    var queryInputs = queryDataset
                        .Select(x => x.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

                    //remove the first line which contain texts
                    string[][] validQueryInputs = new string[queryInputs.Length - 1][];

                    for (int i = 0; i < queryInputs.Length - 1; i++)
                    {
                        validQueryInputs[i] = new string[queryInputs[0].Length];

                        for (int j = 0; j < queryInputs[0].Length; j++)
                            validQueryInputs[i][j] = queryInputs[i + 1][j];
                    }

                    var scoreCard = new List<bool>();

                    foreach (var input in validQueryInputs)
                    {
                        var normalizedInput = NormalizeInput(input.Skip(1).ToArray());
                        var correctAnswer = int.Parse(input[0]);
                        var response = network.Query(normalizedInput);

                        var max = response.Max(x => x);
                        var f = response.ToList().IndexOf(max);

                        scoreCard.Add(correctAnswer == f);
                    }

                    Console.WriteLine($"Performed {scoreCard.Count} queries. Correct answers were {scoreCard.Count(x => x)}.");
                    Console.WriteLine($"Network has a performance of {scoreCard.Count(x => x) / Convert.ToDouble(scoreCard.Count)}");
                    Console.WriteLine();
                }
            }
        }

        internal static double[] NormalizeInput(string[] input)
        {
            return input
                .Select(double.Parse)
                .Select(y => (y / 255) * 0.99 + 0.01)
                .ToArray();
        }
    }
}
