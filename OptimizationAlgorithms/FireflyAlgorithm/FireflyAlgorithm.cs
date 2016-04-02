using System;
using CommonTools.Common;
using CommonTools.Common.InitializationSchemes;
using CommonTools.Util.RandomNumberGeneration;
using OptimizationBenchmarks.RealValuedBenchmarks;

namespace OptimizationAlgorithms.FireflyAlgorithm
{
    public class FireflyAlgorithm
    {
        private readonly RealValuedBenchmark Benchmark;
        private readonly InitializationScheme<double> InitializationScheme;
        private readonly RandomNumberGenerator Random;

        public FireflyAlgorithm(RealValuedBenchmark benchmark, InitializationScheme<double> initializationScheme, RandomNumberGenerator random)
        {
            this.Benchmark = benchmark;
            this.InitializationScheme = initializationScheme;
            this.Random = random;
        }

        private double Error(RealValuedSolution sol)
        {
            int dim = sol.DecisionVariables.Length;
            double trueMin = 0.0;
            if (dim == 2)
                trueMin = -1.8013; // Approx.
            else if (dim == 5)
                trueMin = -4.687658; // Approx.
            this.Benchmark.Run(sol);
            double calculated = sol.Quality;
            //return Math.Pow(calculated, 2);
            return (trueMin - calculated) * (trueMin - calculated);
        }

        public RealValuedSolution Solve(int numFireflies, int dim, int maxEpochs)
        {
            double minX = -100; // specific to Michalewicz function
            double maxX = 100;

            double B0 = 1.0;  // beta (attractiveness base)
            //double betaMin = 0.20;
            double g = 1.0;   // gamma (absorption for attraction)
            double a = 0.20;    // alpha
            //double a0 = 1.0;    // base alpha for decay
            int displayInterval = maxEpochs / 10;

            double bestError = double.MaxValue;
            double[] bestPosition = new double[dim]; // best ever

            RealValuedSolution[] swarm = new RealValuedSolution[numFireflies]; // all null
            double[] intensity = new double[numFireflies];

            // initialize swarm at random positions
            for (int i = 0; i < numFireflies; ++i)
            {
                swarm[i] = new RealValuedSolution(dim); // position 0, error and intensity 0.0
                this.InitializationScheme.Initialize(swarm[i]);
                swarm[i].Quality = Error(swarm[i]); // associated error
                intensity[i] = 1 / (swarm[i].Quality + 1); // +1 prevent div by 0
                if (swarm[i].Quality < bestError)
                {
                    bestError = swarm[i].Quality;
                    for (int k = 0; k < dim; ++k)
                        bestPosition[k] = swarm[i].DecisionVariables[k];
                }
            }

            

            int epoch = 0;
            while (epoch < maxEpochs) // main processing
            {
                //if (bestError < errThresh) break; // are we good?
                if (epoch % displayInterval == 0 && epoch < maxEpochs) // show progress?
                {
                    string sEpoch = epoch.ToString().PadLeft(6);
                    Console.Write("epoch = " + sEpoch);
                    Console.WriteLine("   error = " + bestError.ToString("F14"));
                }

                for (int i = 0; i < numFireflies; ++i) // each firefly
                {
                    for (int j = 0; j < numFireflies; ++j) // each other firefly. weird!
                    {
                        if (intensity[i] < intensity[j])
                        {
                            // curr firefly i is less intense (i is worse) so move i toward j
                            double r = swarm[i].EuclideanDistance(swarm[j]);
                            double beta = B0 * Math.Exp(-g * r * r); // original 
                            //double beta = (B0 - betaMin) * Math.Exp(-g * r * r) + betaMin; // better
                            //double a = a0 * Math.Pow(0.98, epoch); // better
                            for (int k = 0; k < dim; ++k)
                            {
                                swarm[i].DecisionVariables[k] += beta * (swarm[j].DecisionVariables[k] - swarm[i].DecisionVariables[k]);
                                swarm[i].DecisionVariables[k] += a * (this.Random.NextDouble() - 0.5);
                                if (swarm[i].DecisionVariables[k] < minX) swarm[i].DecisionVariables[k] = (maxX - minX) * this.Random.NextDouble() + minX;
                                if (swarm[i].DecisionVariables[k] > maxX) swarm[i].DecisionVariables[k] = (maxX - minX) * this.Random.NextDouble() + minX;
                            }
                            swarm[i].Quality = Error(swarm[i]);
                            intensity[i] = 1 / (swarm[i].Quality + 1);
                        }
                    } // j
                } // i each firefly

                Array.Sort(swarm); // low error to high
                if (swarm[0].Quality < bestError) // new best?
                {
                    bestError = swarm[0].Quality;
                    for (int k = 0; k < dim; ++k)
                        bestPosition[k] = swarm[0].DecisionVariables[k];
                }
                ++epoch;
            } // while
            RealValuedSolution res = new RealValuedSolution(dim);
            res.DecisionVariables = bestPosition;
            return res;
        } // Solve
    }
}
