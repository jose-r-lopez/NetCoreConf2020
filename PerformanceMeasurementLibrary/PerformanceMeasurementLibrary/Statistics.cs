using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceMeasurementLibrary
{
    /// <summary>
    /// Auxiliary class with simple mean and standard deviation functions.
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// Gets the average of a set of values
        /// </summary>
        /// <param name="values">Set of values</param>
        /// <returns>Average of the values</returns>
        public static double GetMean(IEnumerable<double> values)
        {
            return values.Average();
        }

        /// <summary>
        /// Gets the average of a set of values, but using only the last k elements
        /// </summary>
        /// <param name="values">Set of values</param>
        /// <param name="k">Last k values to consider</param>
        /// <returns>Average of the last k values</returns>
        public static double GetMean(IEnumerable<double> values, int k)
        {
            var meanValues = values.TakeLast(k);
            return meanValues.Average();
        }

        /// <summary>
        /// Gets the standard deviation of a set of values
        /// </summary>
        /// <param name="valuesEn">Set of values</param>
        /// <returns>Standard deviation of the values</returns>
        public static double GetStandardDeviation(IEnumerable<double> valuesEn)
        {
            var values = valuesEn.ToArray();
            double avg = GetMean(values);
            double sum = values.Sum(v => (v - avg) * (v - avg));
            double denominator = values.Count() - 1;
            return denominator > 0.0 ? Math.Sqrt(sum / denominator) : -1;
        }
    }
}
