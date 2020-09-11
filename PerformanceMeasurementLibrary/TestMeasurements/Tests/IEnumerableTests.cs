using System;
using System.Collections.Generic;
using System.Text;
using PerformanceMeasurementLibrary;
using TestMeasurements.AuxiliaryFunctions;

namespace TestMeasurements.Tests
{
    /// <summary>
    /// Implementation of the different tests of the presentation based on IEnumerable
    /// and Linq (Single Execution (3 executions), Startup, Steady, Memory)
    /// </summary>
    public static class IEnumerableTests
    {
        /// <summary>
        /// Single executions (3) of the IEnumerable+Linq tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionSingleExecutions(string foundHash)
        {
            var result2 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerable(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable: " + result2);

            result2 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerable(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable: " + result2);

            result2 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerable(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable: " + result2);
        }

        /// <summary>
        /// Startup execution of the IEnumerable+Linq tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionStartup(string foundHash)
        {
            var result = Performance.Startup(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerable(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable (Startup): " + result);
        }

        /// <summary>
        /// Steady-State execution of the IEnumerable+Linq tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionSteady(string foundHash)
        {
            var result = Performance.Steady(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerable(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable (Steady): " + result);
        }

        /// <summary>
        /// Memory execution of the IEnumerable+Linq tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionMemory(string foundHash)
        {
            var result = Performance.Memory(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerable(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable (Memory): " + result);
        }
    }
}
