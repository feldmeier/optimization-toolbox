using CommonTools.Common.InitializationSchemes;
using CommonTools.Util.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Common
{
    public class SolutionSet<T>
    {
        public List<Solution<T>> Solutions { get; set; }

        public SolutionSet(List<Solution<T>> solutions)
        {
            this.Solutions = solutions;
        }

        public SolutionSet(InitializationScheme<T> initializationScheme, int number, int dimension)
        {
            this.Solutions = new List<Solution<T>>(number);
            for(int i = 0; i < number; i++)
            {
                Solution<T> sol = new Solution<T>(dimension);
                initializationScheme.Initialize(sol);
                this.Solutions.Add(sol);
            }
        }

        public SolutionSet()
        {
            this.Solutions = new List<Solution<T>>();
        }

        public void Order()
        {
            this.Solutions = this.Solutions.OrderBy(x => x.Quality).ToList();
        }

        public void Add(Solution<T> solution)
        {
            this.Solutions.Add(solution);
        }

        public void AddRange(IEnumerable<Solution<T>> solutions)
        {
            this.Solutions.AddRange(solutions);
        }

        public Solution<T> GetBest()
        {
            return this.GetTop(1).FirstOrDefault();
        }

        public int GetCount()
        {
            return this.Solutions.Count;
        }

        public Solution<T> GetRandom(RandomNumberGenerator random)
        {
            int index = random.Next(this.GetCount());
            return this.Solutions.ElementAt(index);
        }

        public List<Solution<T>> GetTop(int number)
        {
            return this.Solutions.OrderBy(x => x.Quality).Take(number).ToList();
        }
    }
}
