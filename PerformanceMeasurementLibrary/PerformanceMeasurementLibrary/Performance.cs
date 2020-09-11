using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PerformanceMeasurementLibrary
{
    /// <summary>
    /// Main library front-end code
    /// </summary>
    public static class Performance
    {
        /// <summary>
        /// Give the test as good a chance as possible of avoiding garbage collection
        /// </summary>
        private static void LaunchGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        /// This function determines the confidence interval for a given set of samples,
        /// as well as the mean, the standard deviation,
        /// and the size of the confidence interval as a percentage of the mean.
        /// </summary>
        /// <param name="samplesEn">Set of samples</param>
        /// <param name="confidenceLevel">Confidence level</param>
        /// <returns></returns>
        private static ((double intervalLow, double intervalHigh), double, double, double)
            Confidence(IEnumerable<double> samplesEn, double confidenceLevel)
        {
            var samples = samplesEn.ToArray();
            var mean = Statistics.GetMean(samples);
            var sdev = Statistics.GetStandardDeviation(samples);
            var n = samples.Length;
            var df = n - 1;
            var t = TStudent.Student((1 + confidenceLevel) / 2.0, df);
            (double intervalLow, double intervalHigh) interval = (mean - t * sdev / Math.Sqrt(n), mean + t * sdev / Math.Sqrt(n));
            var intervalSize = interval.intervalHigh - interval.intervalLow;
            var intervalPercentage = intervalSize / mean * 100.0;
            return (interval, mean, sdev, intervalPercentage);
        }

        /// <summary>
        /// Run the command once and return the execution time (in milliseconds)
        /// </summary>
        /// <param name="command"> Code to run</param>
        /// <returns>Milliseconds that the command took to complete</returns>
        public static double SingleExecution(Action command)
        {
            var timer = Stopwatch.StartNew();
            command();
            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        /// <summary>
        /// Measure time of the command using Startup methodology
        /// </summary>
        /// <param name="command">Code to run</param>
        /// <param name="confidenceLevel">Confidence level of the time interval to return (default: 0.95)</param>
        /// <param name="pIterations">Program iterations (default: 30)</param>
        /// <param name="breakIfInternalPercentageIs">Maximum % of the interval size relative to the mean
        /// to stop measurements (default: 2)</param>
        /// <param name="verbose">Show extra debugging messages (default: true)</param>
        /// <returns>A tuple ((Minimum interval value, Maximum interval value), Internal Mean, Standard Deviation, % Interval size)</returns>
        public static ((double intervalLow, double intervalHigh), double mean, double sdev, double intervalPercentage)
            Startup(
            Action command,
            double confidenceLevel = 0.95,
            int pIterations = 30,
            double breakIfInternalPercentageIs = 2,
            bool verbose = true)
        {
            if (verbose)
                Console.WriteLine("Executing the command: " + command + " in startup mode ");

            var timer = new Stopwatch();
            var executionTimes = new List<double>();
            ((double intervalLow, double intervalHigh), double mean, double sdev, double intervalPercentage) result = ((0, 0), 1, 0, 0);

            for (int i = 1; i < pIterations + 1; i++)
            {
                try
                {
                    timer.Start();
                    command();
                    timer.Stop();
                    var after = timer.ElapsedMilliseconds;
                    timer.Reset();
                    var executionTime = after;

                    if (verbose)
                        Console.WriteLine("Iteration " + i + ". Times in millis " + executionTime + ".");

                    executionTimes.Add(executionTime);
                    result = Confidence(executionTimes, confidenceLevel);

                    if (verbose)
                        Console.WriteLine("Iteration " + i + ": " + result.intervalPercentage);

                    if (result.intervalPercentage <= breakIfInternalPercentageIs)
                        break;
                    LaunchGC();
                }
                catch (Exception)
                {
                    return ((0, 0), 1, 0, 0);
                }
            }

            return result;
        }

        /// <summary>
        /// Measure time of the command using Steady methodology
        /// </summary>
        /// <param name="command">Code to run</param>
        /// <param name="confidenceLevel">Confidence level of the time interval to return (default: 0.95)</param>
        /// <param name="pIterations">Program iterations (default: 30)</param>
        /// <param name="maxBenchIterations">Maximum amount of executions that the code is run
        /// on each iteration (default: 30)</param>
        /// <param name="k">Minimum amount of executions that the code is run
        /// on each iteration (default: 10)</param>
        /// <param name="CoV">Coefficient of variation limit to stop measuring a program inside an
        /// iteration (default: 0.02)</param>
        /// <param name="breakIfInternalPercentageIs">Maximum % of the interval size relative to the mean
        /// to stop measurements (default: 2)</param>
        /// <param name="verbose">Show extra debugging messages (default: true)</param>
        /// <returns>A tuple ((Minimum interval value, Maximum interval value), Internal Mean, Standard Deviation, % Interval size)</returns>
        public static ((double intervalLow, double intervalHigh), double mean, double sdev, double intervalPercentage)
            Steady(Action command,
            double confidenceLevel = 0.95,
            int pIterations = 30,
            int maxBenchIterations = 30,
            int k = 10,
            double CoV = 0.02,
            double breakIfInternalPercentageIs = 2,
            bool verbose = true)
        {
            if (verbose)
                Console.WriteLine("Executing the command: " + command + " in steady mode ");

            var timer = new Stopwatch();
            var executionTimes = new List<double>();
            ((double intervalLow, double intervalHigh), double mean, double sdev, double intervalPercentage) result = ((0, 0), 1, 0, 0);

            for (int i = 1; i < pIterations + 1; i++)
            {
                try
                {
                    var executionTime = SteadyFunctions.RunAsSteady(command, maxBenchIterations, k, CoV);
                    if (verbose)
                        Console.WriteLine("Iteration " + i + ". Times in millis " + executionTime + ".");

                    executionTimes.Add(executionTime);
                    result = Confidence(executionTimes, confidenceLevel);

                    if (verbose)
                        Console.WriteLine("Iteration " + i + ": " + result.intervalPercentage);

                    if (result.intervalPercentage <= breakIfInternalPercentageIs)
                        break;
                    LaunchGC();
                }
                catch (Exception)
                {
                    return ((0, 0), 1, 0, 0);
                }
            }

            return result;
        }

        /// <summary>
        /// Measure the memory consumption of the command using Startup methodology
        /// </summary>
        /// <param name="command">Code to run</param>
        /// <param name="confidenceLevel">Confidence level of the time interval to return (default: 0.95)</param>
        /// <param name="pIterations">Program iterations (default: 30)</param>
        /// <param name="breakIfInternalPercentageIs">Maximum % of the interval size relative to the mean
        /// to stop measurements (default: 2)</param>
        /// <param name="verbose">Show extra debugging messages (default: true)</param>
        /// <returns>A tuple ((Minimum interval value, Maximum interval value), Internal Mean, Standard Deviation, % Interval size)</returns>
        public static ((double intervalLow, double intervalHigh), double, double, double)
            Memory(Action command,
                double confidenceLevel = 0.95,
                int pIterations = 30,
                double breakIfInternalPercentageIs = 2,
                bool verbose = true)
        {
            if (verbose)
                Console.WriteLine("Executing the command: " + command + " in memory measurement mode ");
            var process = Process.GetCurrentProcess();
            var executionTimes = new List<double>();
            ((double intervalLow, double intervalHigh), double mean, double sdev, double intervalPercentage) result = ((0, 0), 1, 0, 0);

            for (int i = 1; i < pIterations + 1; i++)
            {
                try
                {
                    command();
                    var memoryValue = process.PeakWorkingSet64; ;

                    if (verbose)
                        Console.WriteLine("Iteration " + i + ". Memory in bytes " + memoryValue + ".");

                    executionTimes.Add(memoryValue);
                    result = Confidence(executionTimes, confidenceLevel);

                    if (verbose)
                        Console.WriteLine("Iteration " + i + ": " + result.intervalPercentage);

                    if (result.intervalPercentage <= breakIfInternalPercentageIs)
                        break;
                    LaunchGC();
                }
                catch (Exception)
                {
                    return ((0, 0), 1, 0, 0);
                }
            }
            return result;
        }
    }
}
