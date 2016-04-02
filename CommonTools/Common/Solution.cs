using System;

namespace CommonTools.Common
{
    public class Solution<T> : IComparable<Solution<T>>
    {
        public T[] DecisionVariables { get; set; }

        public int Dimension { get { return this.DecisionVariables.Length; } }

        public double Quality { get; set; }

        public Solution(int dimension)
        {
            this.DecisionVariables = new T[dimension];
            this.Quality = double.NaN;
        }


        public Solution<T> Copy()
        {
            Solution<T> copy = new Solution<T>(this.Dimension)
            {
                DecisionVariables = new T[this.Dimension],
                Quality = this.Quality
            };
            for (int i = 0; i < this.Dimension; i++)
            {
                copy.DecisionVariables[i] = this.DecisionVariables[i];
            }
            return copy;
        }

        public int CompareTo(Solution<T> other)
        {
            if (this.Quality < other.Quality)
            {
                return -1;
            }
            else if (this.Quality > other.Quality)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static bool operator <(Solution<T> s1, Solution<T> s2)
        {
            return s1.Quality < s2.Quality;
        }

        public static bool operator >(Solution<T> s1, Solution<T> s2)
        {
            return s1.Quality > s2.Quality;
        }
    }
}
