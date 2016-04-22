using CommonTools.Common.InitializationSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Common
{
    public class IntegerValuedSolutionSet : SolutionSet<int>
    {
        public IntegerValuedSolutionSet(List<Solution<int>> solutions) : base(solutions)
        {

        }

        public IntegerValuedSolutionSet(List<IntegerValuedSolution> solutions)
            : base(solutions.Select(x => new Solution<int>(x.Dimension) { DecisionVariables = x.DecisionVariables, Quality = x.Quality }).ToList())
        {

        }

        public IntegerValuedSolutionSet(InitializationScheme<int> initializationScheme, int number, int dimension) : base(initializationScheme, number, dimension)
        {

        }

        public IntegerValuedSolutionSet(SolutionSet<int> solutionSet) : base(solutionSet.Solutions)
        {
        }

        public IntegerValuedSolutionSet() : base()
        {

        }
    }
}
