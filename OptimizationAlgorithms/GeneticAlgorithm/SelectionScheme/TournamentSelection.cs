using System.Collections.Generic;
using System.Linq;
using CommonTools.Common;
using CommonTools.Util.RandomNumberGeneration;

namespace OptimizationAlgorithms.GeneticAlgorithm.SelectionScheme
{
    public class TournamentSelection<T> : SelectionScheme<T>
    {
        private readonly RandomNumberGenerator Random;

        private readonly int TournamentSize;

        public TournamentSelection(RandomNumberGenerator random, int tournamentSize)
        {
            this.Random = random;
            this.TournamentSize = tournamentSize;
        }

        public override Solution<T> Select(List<Solution<T>> solutions)
        {
            Solution<T> res = null;

            for (int i = 0; i < this.TournamentSize; i++)
            {
                int randomIndex = this.Random.Next(solutions.Count());

                Solution<T> sel = solutions.ElementAt(randomIndex);
                if (res == null || sel.Quality < res.Quality)
                {
                    res = sel;
                }
            }

            return res;
        }
    }
}
