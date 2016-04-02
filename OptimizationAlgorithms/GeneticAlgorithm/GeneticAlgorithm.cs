using System.Collections.Generic;
using System.Linq;
using CommonTools.Common;
using CommonTools.Common.InitializationSchemes;
using CommonTools.Common.UpdateScheme;
using CommonTools.Util.RandomNumberGeneration;
using OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes;
using OptimizationAlgorithms.GeneticAlgorithm.SelectionScheme;
using OptimizationBenchmarks;

namespace OptimizationAlgorithms.GeneticAlgorithm
{
    public class GeneticAlgorithm<T>
    {
        private readonly InitializationScheme<T> InitializationScheme; 
        private readonly SelectionScheme<T> SelectionScheme;
        private readonly CrossoverScheme<T> CrossoverScheme;
        private readonly UpdateScheme<T> UpdateScheme;

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
            this.UpdateScheme = updateScheme;
            this.Crossoverrate = crossoverrate;
            this.Mutationrate = mutationrate;
            this.ElitismPercentage = elitismPercentage;
            this.Benchmark = benchmark;
            this.Dimension = dimension;
            this.Random = random;
        }


        public List<Solution<T>> Iterate(List<Solution<T>> population)
        {
            List<Solution<T>> newPop = new List<Solution<T>>();
            List<Solution<T>> orderedPop = population.OrderBy(x => x.Quality).ToList();
            //elitism
            for (int i = 0; i < orderedPop.Count()*this.ElitismPercentage; i++)
            {
                newPop.Add(orderedPop[i]);
            }
            foreach (Solution<T> individual in population)
            {
                this.Benchmark.Run(individual);
            }
            while (newPop.Count() < orderedPop.Count())
            {
                List<Solution<T>> selected = new List<Solution<T>>();

                //selection
                for (int i = 0; i < 2; i++)
                {
                    selected.Add(this.SelectionScheme.Select(orderedPop));
                }
                
                //crossover
                if (this.Random.NextDouble() < this.Crossoverrate)
                {
                    selected = this.CrossoverScheme.Crossover(selected);
                }

                //mutation
                for (int i = 0; i < selected.Count(); i++)
                {
                    if (this.Random.NextDouble() < this.Mutationrate)
                    {
                        selected[i] = this.UpdateScheme.Update(selected[i]);
                    }
                    if (newPop.Count() < orderedPop.Count())
                    {
                        this.Benchmark.Run(selected[i]);
                        newPop.Add(selected[i]);
                    }
                }

            }
            return newPop;
        }

        public IEnumerable<Solution<T>> Run(int iterations, int populationSize)
        {

            //Initialize
            List<Solution<T>> pop = new List<Solution<T>>();
            for (int i = 0; i < populationSize; i++)
            {
                pop[i] = new Solution<T>(this.Dimension);
                this.InitializationScheme.Initialize(pop[i]);
            }

            for (int i = 0; i < iterations; i++)
            {
                pop = this.Iterate(pop);
            }

            return pop;
        }
    }
}
