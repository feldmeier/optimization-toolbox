using System.Collections.Generic;
using System.Linq;
using CommonTools.Common;
using CommonTools.Common.InitializationSchemes;
using CommonTools.Common.UpdateScheme;
using CommonTools.Util.RandomNumberGeneration;
using OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes;
using OptimizationAlgorithms.GeneticAlgorithm.SelectionScheme;
using OptimizationBenchmarks;
using System;

namespace OptimizationAlgorithms.GeneticAlgorithm
{
    public class GeneticAlgorithm<T>
    {
        private readonly InitializationScheme<T> InitializationScheme;
        private readonly SelectionScheme<T> SelectionScheme;
        private readonly CrossoverScheme<T> CrossoverScheme;
        private readonly UpdateScheme<T> MutationScheme;

        private readonly double Crossoverrate;
        private readonly double Mutationrate;
        private readonly double ElitismPercentage;

        private readonly Benchmark<T> Benchmark;
        private readonly int Dimension;

        private readonly RandomNumberGenerator Random;

        public GeneticAlgorithm(InitializationScheme<T> initializationScheme, SelectionScheme<T> selectionScheme, CrossoverScheme<T> crossoverScheme,
            UpdateScheme<T> updateScheme, double crossoverrate, double mutationrate, double elitismPercentage, Benchmark<T> benchmark, int dimension, RandomNumberGenerator random)
        {
            this.InitializationScheme = initializationScheme;
            this.SelectionScheme = selectionScheme;
            this.CrossoverScheme = crossoverScheme;
            this.MutationScheme = updateScheme;
            this.Crossoverrate = crossoverrate;
            this.Mutationrate = mutationrate;
            this.ElitismPercentage = elitismPercentage;
            this.Benchmark = benchmark;
            this.Dimension = dimension;
            this.Random = random;
        }

        public SolutionSet<T> Iterate(SolutionSet<T> population)
        {
            this.Benchmark.EvaluateAll(population);
            population.Order();

            //elitism
            List<Solution<T>> elites = population.GetTop((int)Math.Floor(population.GetCount() * this.ElitismPercentage));

            SolutionSet<T> newPopulation = new SolutionSet<T>(elites);

            while (newPopulation.GetCount() < population.GetCount())
            {
                //selection
                Solution<T>[] selected = this.SelectionScheme.SelectMultiple(population, 2);

                //crossover
                Solution<T> crossovered;
                if (this.Random.NextDouble() < this.Crossoverrate)
                {
                    crossovered = this.CrossoverScheme.Crossover(selected[0], selected[1]);
                }
                else
                {
                    crossovered = selected[0];
                }

                //mutation
                if (this.Random.NextDouble() < this.Mutationrate)
                {
                    crossovered = this.MutationScheme.Update(crossovered);
                }

                //evaluate new solution
                this.Benchmark.Evaluate(crossovered);
                newPopulation.Add(crossovered);
            }
            return newPopulation;
        }

        public SolutionSet<T> Run(int iterations, int populationSize)
        {
            SolutionSet<T> population = new SolutionSet<T>(this.InitializationScheme, populationSize, this.Dimension);

            for (int i = 0; i < iterations; i++)
            {
                population = this.Iterate(population);
            }

            return population;
        }
    }
}
