using System;

namespace OptimizationAlgorithms.SimulatedAnnealing.CoolingSchedule
{
    public class LogarithmicCoolingSchedule : CoolingSchedule
    {
        private int Iteration { get; set; }
        private double StartTemperature { get; set; }

        private double C { get; set; }
        private double D { get; set; }
        public LogarithmicCoolingSchedule(double startTemperature, double c, double d = 1.0) : base(startTemperature)
        {
            this.StartTemperature = startTemperature;
            this.C = c;
            this.D = d;
        }

        public override bool CoolDown()
        {
            this.Iteration++;
            if (this.CurrentTemperature > 0.0)
            {
                this.CurrentTemperature = this.C/Math.Log10(this.Iteration + this.D);
            }
            return this.CheckTemperature();
        }

        public override bool CheckTemperature()
        {
            return this.CurrentTemperature <= 0.0;
        }
    }
}
