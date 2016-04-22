using CommonTools.Common.InitializationSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Common
{
    public class RealValuedSolutionSet : SolutionSet<double>
    {
        public RealValuedSolutionSet(List<Solution<double>> solutions) : base(solutions)
        {

        }

        public RealValuedSolutionSet(List<RealValuedSolution> solutions)
            : base(solutions.Select(x => new Solution<double>(x.Dimension) { DecisionVariables = x.DecisionVariables, Quality = x.Quality }).ToList())
        {

        }

        public RealValuedSolutionSet(InitializationScheme<double> initializationScheme, int number, int dimension)
            : base(initializationScheme, number, dimension)
        {

        }

        public RealValuedSolutionSet(SolutionSet<double> solutionSet)
            : base(solutionSet.Solutions)
        {
        }

        public RealValuedSolutionSet() : base()
        {

        }
    }
}
