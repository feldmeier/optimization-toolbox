using System;
using CommonTools.Common;
using System.IO;
using System.Reflection;
using OptimizationBenchmarks.Properties;
using OptimizationBenchmarks.IntegerValuedBenchmarks.TSPInstances;
using System.Collections.Generic;
using CommonTools.Util;

namespace OptimizationBenchmarks.IntegerValuedBenchmarks
{
    public class TravelingSalesmanProblemBenchmark : IntegerValuedBenchmark
    {
        private readonly double[][] Distances;


        public TravelingSalesmanProblemBenchmark(string instanceName, int dimension) : base(dimension)
        {
            this.Distances = new double[dimension][];
            for(int i = 0; i < dimension; i++)
            {
                this.Distances[i] = new double[dimension];
            }

            //Check dimension of benchmark instance
            string outPutDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string instance = Resources.Circle8;

            //read benchmark instance from file
            List<TSPCity> cities = this.readInstance("Circle8");
            
            //compute pairwise distance values
            this.ComputeDistances(cities);
        }

        private List<TSPCity> readInstance(string instanceName)
        {
            List<TSPCity> instance = new List<TSPCity>();
            string s = Resources.ResourceManager.GetString(instanceName);
            string csv = s.Replace("\r\n", ";");
            string[] lines = csv.Split(';');
            foreach(string line in lines)
            {
                string[] comp = line.Split(' ');
                instance.Add(new TSPCity(0, double.Parse(comp[0]), double.Parse(comp[1]), comp[2]));
            }

            return instance;
        }

        public void ComputeDistances(List<TSPCity> cities)
        { 
            for(int i = 0; i < cities.Count; i++)
            {
                for(int j = 0; j < cities.Count; j++)
                {
                    double[] v1 = new[]{cities[i].X, cities[i].Y};
                    double[] v2 = new[]{cities[j].X, cities[j].Y};
                    this.Distances[i][j] = MathStuff.EuclideanDistance(v1, v2);
                }
            }
        }

        public override void Run(Solution<int> solution)
        {
            double tourLength = 0.0;
            for (int i = 0; i < solution.Dimension; i++)
            {
                tourLength += this.Distances[i][(i + 1) % solution.Dimension];
            }
            solution.Quality = tourLength;
        }
    }
}
