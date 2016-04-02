using System;

namespace OptimizationAlgorithms.SimulatedAnnealing.CoolingSchedule
{
    public class ExponentialCoolingSchedule : CoolingSchedule
    {
        private int Iteration { get; set; }
        private double StartTemperature { get; set; }

        private double Alpha { get; set; }
        public ExponentialCoolingSchedule(double startTemperature, double alpha) : base(startTemperature)
        {
            this.StartTemperature = startTemperature;
            this.Alpha = alpha;
        }

        public override bool CoolDown()
        {
            this.Iteration++;
            if (this.CurrentTemperature > 0.0)
            {
                this.CurrentTemperature = this.StartTemperature*Math.Pow(this.Alpha, this.Iteration);
            }
            return this.CheckTemperature();
        }

        public override bool CheckTemperature()
        {
            return this.CurrentTemperature <= 0.0;
        }
    }
}
