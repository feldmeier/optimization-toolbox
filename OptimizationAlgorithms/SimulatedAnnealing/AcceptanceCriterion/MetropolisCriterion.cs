using System;
using CommonTools.Util.RandomNumberGeneration;

namespace OptimizationAlgorithms.SimulatedAnnealing.AcceptanceCriterion
{
    public class MetropolisCriterion : AcceptanceCriterion
    {
        private readonly RandomNumberGenerator Random;
        public MetropolisCriterion(RandomNumberGenerator random)
        {
            this.Random = random;
        }

        public override bool AcceptProposal(double current, double proposal, double temperature)
        {
            if (proposal < current)
                return true;
            if (temperature <= 0.0)
                return false;
            double probability = Math.Exp(-(proposal - current)/temperature);

            return this.Random.NextDouble() < probability;
        }
    }
}
