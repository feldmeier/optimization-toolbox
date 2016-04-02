namespace OptimizationAlgorithms.SimulatedAnnealing.CoolingSchedule
{
    public abstract class CoolingSchedule
    {
        public double CurrentTemperature { get; protected set; }
        public abstract bool CoolDown();

        public abstract bool CheckTemperature();

        protected CoolingSchedule(double startTemperature)
        {
            this.CurrentTemperature = startTemperature;
        }
    }
}