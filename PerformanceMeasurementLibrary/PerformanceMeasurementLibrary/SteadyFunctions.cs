using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PerformanceMeasurementLibrary
{
    /// <summary>
    /// Auxiliary functions for the Steady State performance measurements
    /// </summary>
    public static class SteadyFunctions
    {
        /// <summary>
        /// Uses the provided execution times to calculate if the last k times have a
        /// coefficient of variation less than the provided one
        /// </summary>
        /// <param name="executionTimes">Execution times so far</param>
        /// <param name="k">Number of execution times to consider (latest ones)</param>
        /// <param name="CoV">Maximum CoV allowed</param>
        /// <returns>true if the Coefficient of variation of the latest k times is less
        /// than CoV, false otherwise</returns>
        private static bool AreWeDone(List<double> executionTimes, int k, double CoV)
        {
            if (executionTimes.Count < k)
                return false;
            double summation = 0;
            var mean = Statistics.GetMean(executionTimes, k);
            int lenExecutionTimes = executionTimes.Count;
            for (int i = lenExecutionTimes - k; i < lenExecutionTimes; i++)
                summation += Math.Pow(executionTimes[i] - mean, 2);
            var stdDeviation = Math.Sqrt(summation / k);

            return (stdDeviation / mean) < CoV;
        }

        /// <summary>
        /// Runs the provided command a number of iterations between k and maxNumberIterations,
        /// depending if the latest k measured times have a coefficient of variation less that
        /// the specified CoV parameter
        /// </summary>
        /// <param name="command">Code to run</param>
        /// <param name="maxNumberIterations">Maximum amount of times the code will run</param>
        /// <param name="k">Minimum amount of times the code will run</param>
        /// <param name="CoV">Coefficient of variation limit to stop executions</param>
        /// <returns></returns>
        public static double RunAsSteady(Action command, int maxNumberIterations, int k, double CoV)
        {
            var timer = new Stopwatch();
            List<double> executionTimes = new List<double>();

            for (int i = 0; i < maxNumberIterations + 1; i++)
            {
                timer.Start();
                command();
                timer.Stop();
                var executionTime = timer.ElapsedMilliseconds;
                timer.Reset();

                executionTimes.Add(executionTime);
                if (AreWeDone(executionTimes, k, CoV))
                    break;
            }

            return Statistics.GetMean(executionTimes, k);
        }
    }
}
