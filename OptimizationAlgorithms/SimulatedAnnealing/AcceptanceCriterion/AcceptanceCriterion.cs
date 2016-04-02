namespace OptimizationAlgorithms.SimulatedAnnealing.AcceptanceCriterion
{
    public abstract class AcceptanceCriterion
    {
        public abstract bool AcceptProposal(double current, double proposal, double temperature);
    }
}