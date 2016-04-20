using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationBenchmarks.IntegerValuedBenchmarks.TSPInstances
{
    public class TSPCity
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }

        public TSPCity(int id, double x, double y, string name)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Name = name;
        }
    }
}
