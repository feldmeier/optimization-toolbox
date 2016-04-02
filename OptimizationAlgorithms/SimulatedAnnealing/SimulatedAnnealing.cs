using CommonTools.Common;
using CommonTools.Common.InitializationSchemes;
using CommonTools.Common.UpdateScheme;
using OptimizationBenchmarks;

namespace OptimizationAlgorithms.SimulatedAnnealing
{
    public class SimulatedAnnealing<T>
    {
        private readonly CoolingSchedule.CoolingSchedule CoolingSchedule;
        private readonly AcceptanceCriterion.AcceptanceCriterion AcceptanceCriterion;
        private readonly UpdateScheme<T> UpdateScheme;

        public readonly Benchmark<T> Benchmark;

        private readonly InitializationScheme<T> InitializationScheme; 

        private readonly int Dimension;

        public SimulatedAnnealing(int dimension, UpdateScheme<T> updateScheme, Benchmark<T> benchmark, InitializationScheme<T> initializationScheme, CoolingSchedule.CoolingSchedule coolingSchedule, AcceptanceCriterion.AcceptanceCriterion acceptanceCriterion)
        {
            this.Dimension = dimension;
            this.CoolingSchedule = coolingSchedule;
            this.AcceptanceCriterion = acceptanceCriterion;
            this.UpdateScheme = updateScheme;
            this.Benchmark = benchmark;
            this.InitializationScheme = initializationScheme;
        }

        public Solution<T> Iterate(Solution<T> solution, int iterationsPerTemperature)
        {
            int iteration = 0;
            do
            {
                Solution<T> candidate = this.UpdateScheme.Update(solution);
                this.Benchmark.Run(solution);
                this.Benchmark.Run(candidate);
                if (this.AcceptanceCriterion.AcceptProposal(solution.Quality,
                    candidate.Quality, this.CoolingSchedule.CurrentTemperature))
                {
                    solution = candidate;
                }
            } while (++iteration < iterationsPerTemperature);
            this.CoolingSchedule.CoolDown();
            return solution;
        }

        public double GetTemperature()
        {
            return this.CoolingSchedule.CurrentTemperature;
        }

        public Solution<T> Run(Solution<T> solution, int iterationsPerTemperature)
        {
            solution = solution ?? new Solution<T>(this.Dimension);
            this.InitializationScheme.Initialize(solution);
            do
            {
                solution = this.Iterate(solution, iterationsPerTemperature);
            } while (!this.CoolingSchedule.CheckTemperature());
            return solution;
        }
    }
}
