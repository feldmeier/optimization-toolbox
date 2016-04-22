using System;
using CommonTools.Common;
using System.IO;
using System.Reflection;
using OptimizationBenchmarks.IntegerValuedBenchmarks.TSPInstances;
using System.Collections.Generic;
using CommonTools.Util;
using System.Globalization;
using System.Linq;

namespace OptimizationBenchmarks.IntegerValuedBenchmarks
{
    public class TravelingSalesmanProblemBenchmark : IntegerValuedBenchmark
    {
        private readonly double[][] Distances;
        private readonly List<TSPCity> Cities;

        public TravelingSalesmanProblemBenchmark(string instanceName, bool isTSPLibInstance, int dimension) : base(dimension)
        {
            this.Distances = new double[dimension][];
            for(int i = 0; i < dimension; i++)
            {
                this.Distances[i] = new double[dimension];
            }

            //read benchmark instance from file
            if (isTSPLibInstance)
            {
                this.Cities = this.readTSPLibInstance(instanceName);
            }
            else
            {
                this.Cities = this.readInstance(instanceName);
            }
            //compute pairwise distance values
            this.ComputeDistances();
        }

        private List<TSPCity> readTSPLibInstance(string instanceName)
        {
            //TODO: integrate TSPLib
            List<TSPCity> instance = new List<TSPCity>();
            string s = Instances.ResourceManager.GetString(instanceName);
            CultureInfo culture = CultureInfo.InvariantCulture;
            string replaced = s.Replace("\r\n", ";").Replace("\n", ";").Replace("\r", ";").Replace("  ", " ");
            string[] lines = replaced.Split(';');
            int i = 0;
            while (!lines[i].Equals("NODE_COORD_SECTION"))
            {
                i++;
            }
            i++;
            while(!lines[i].Equals("EOF"))
            {
                string[] l = lines[i].Split(' ');
                string[] line = l.Where(a => !string.IsNullOrEmpty(a)).ToArray();

                string name = line[0];
                double x = double.Parse(line[1], culture.NumberFormat);
                double y = double.Parse(line[2], culture.NumberFormat);
                TSPCity city = new TSPCity(0, x, y, name);
                instance.Add(city);
                i++;
            }
            return instance;
        }

        private List<TSPCity> readInstance(string instanceName)
        {
            List<TSPCity> instance = new List<TSPCity>();
            string s = Instances.ResourceManager.GetString(instanceName);
            string csv = s.Replace("\r\n", ";");
            string[] lines = csv.Split(';');
            foreach(string line in lines)
            {
                string[] comp = line.Split(' ');
                instance.Add(new TSPCity(0, double.Parse(comp[0]), double.Parse(comp[1]), comp[2]));
            }

            return instance;
        }

        public void ComputeDistances()
        { 
            for(int i = 0; i < this.Cities.Count; i++)
            {
                for (int j = 0; j < this.Cities.Count; j++)
                {
                    double[] v1 = new[] { this.Cities[i].X, this.Cities[i].Y };
                    double[] v2 = new[] { this.Cities[j].X, this.Cities[j].Y };
                    this.Distances[i][j] = MathStuff.EuclideanDistance(v1, v2);
                }
            }
        }

        public override void Evaluate(Solution<int> solution)
        {
            double tourLength = 0.0;
            for (int i = 0; i < solution.Dimension; i++)
            {
                tourLength += this.Distances[solution.DecisionVariables[i]][solution.DecisionVariables[(i + 1) % solution.Dimension]];
            }
            solution.Quality = tourLength;
        }
    }
}
