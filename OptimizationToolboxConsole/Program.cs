using System;
using System.Collections.Generic;
using System.Linq;
using CommonTools.Common;
using CommonTools.Common.InitializationSchemes;
using CommonTools.Common.InitializationSchemes.RealValued;
using CommonTools.Common.UpdateScheme;
using CommonTools.Util.RandomNumberGeneration;
using OptimizationAlgorithms.FireflyAlgorithm;
using OptimizationAlgorithms.GeneticAlgorithm;
using OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes;
using OptimizationAlgorithms.GeneticAlgorithm.SelectionScheme;
using OptimizationAlgorithms.GradientDescent;
using OptimizationAlgorithms.SimulatedAnnealing;
using OptimizationAlgorithms.SimulatedAnnealing.AcceptanceCriterion;
using OptimizationAlgorithms.SimulatedAnnealing.CoolingSchedule;
using OptimizationBenchmarks;
using OptimizationBenchmarks.RealValuedBenchmarks;

namespace OptimizationToolboxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SimulatedAnnealingExample();

            //FireflyAlgorithmExample();
            Console.Read();
        }

        static void FireflyAlgorithmExample()
        {
            RealValuedBenchmark benchmark = new Michalewicz(5);
            RandomNumberGenerator random = new StandardRandomNumberGenerator();
            InitializationScheme<double> initializationScheme = new UniformBoundedInitializationScheme(random, -3, 3);
            FireflyAlgorithm fa = new FireflyAlgorithm(benchmark, initializationScheme, random);
            RealValuedSolution res = fa.Solve(35, 5, 1000);
            ConsoleOutput.PrintRealValuedSolutionWithQuality(res);
        }

        static void SimulatedAnnealingExample()
        {
            int dimension = 5;

            RealValuedBenchmark benchmark = new Michalewicz(dimension);

            RandomNumberGenerator random = new StandardRandomNumberGenerator();

            UpdateScheme<double> update = new UniformBoundedRandomUpdateScheme(random, -3, 3, 0.1);

            InitializationScheme<double> initializationScheme = new UniformBoundedInitializationScheme(random, -3, 3);

            CoolingSchedule coolingSchedule = new LinearCoolingSchedule(100, 0.001);

            AcceptanceCriterion acceptanceCriterion = new MetropolisCriterion(random);

            SimulatedAnnealing<double> sa = new SimulatedAnnealing<double>(dimension, update, benchmark, initializationScheme, coolingSchedule, acceptanceCriterion);

            Solution<double> solution = new Solution<double>(dimension);
            initializationScheme.Initialize(solution);

            while (!coolingSchedule.CheckTemperature())
            {
                solution = sa.Iterate(solution, 100);
                ConsoleOutput.PrintRealValuedSolutionWithQuality(solution);
            }
        }

        static void GeneticAlgorithmExample()
        {
            int dimension = 20;
            RealValuedBenchmark benchmark = new Schwefel(dimension);
            RandomNumberGenerator random = new StandardRandomNumberGenerator();
            InitializationScheme<double> initializationScheme = new UniformBoundedInitializationScheme(random, -500, 500);
            SelectionScheme<double> selection = new TournamentSelection<double>(random, 2);
            CrossoverScheme<double> crossover = new SinglePointCrossover<double>(random);
            UpdateScheme<double> mutation = new UniformBoundedRandomUpdateScheme(random, -500.0, 500.0, 10.0);

            GeneticAlgorithm<double> ga = new GeneticAlgorithm<double>(initializationScheme, selection, crossover, mutation, 0.8, 0.05, 0.05, benchmark, dimension, random);

            List<Solution<double>> population = new List<Solution<double>>();
            for (int i = 0; i < 500; i++)
            {
                Solution<double> individual = new Solution<double>(dimension);
                initializationScheme.Initialize(individual);
                population.Add(individual);
            }

            for (int i = 0; i < 500; i++)
            {
                population = ga.Iterate(population);
                ConsoleOutput.PrintRealValuedSolutionWithQuality(population.OrderBy(x => x.Quality).FirstOrDefault());
            }
        }

        static void GradientDescentExample()
        {
            int dimension = 10;
            RealValuedBenchmark benchmark = new Schwefel(dimension);
            RandomNumberGenerator random = new StandardRandomNumberGenerator();
            InitializationScheme<double> initializationScheme = new UniformBoundedInitializationScheme(random, -500, 500);
            NumericalGradientDescent gd = new NumericalGradientDescent(benchmark, initializationScheme, dimension);

            Solution<double> solution = new Solution<double>(dimension);
            initializationScheme.Initialize(solution);
            benchmark.Run(solution);

            for (int i = 0; i < 1000; i++)
            {
                solution = gd.Iterate(solution, 10e-2);
                
            }
            ConsoleOutput.PrintRealValuedSolutionWithQuality(solution);
        }
    }
}
