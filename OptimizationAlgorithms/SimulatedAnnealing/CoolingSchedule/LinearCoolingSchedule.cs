using System;

namespace OptimizationAlgorithms.SimulatedAnnealing.CoolingSchedule
{
    public class LinearCoolingSchedule : CoolingSchedule
    {

        private double TemperatureStepSize { get; set; }

        public LinearCoolingSchedule(double startTemperatur, double temperatureStepSize) : base(startTemperatur)
        {
            this.TemperatureStepSize = temperatureStepSize;
        }

        public override  bool CoolDown()
        {
            if (this.CurrentTemperature > 0.0)
            {
                this.CurrentTemperature = Math.Max(this.CurrentTemperature - this.TemperatureStepSize,
                    0.0);
            }
            return this.CheckTemperature();
        }

        public override bool CheckTemperature()
        {
            return this.CurrentTemperature <= 0.0;
        }
    }
}
