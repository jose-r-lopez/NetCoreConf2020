using System;
using System.Collections.Generic;
using System.Text;
using PerformanceMeasurementLibrary;
using TestMeasurements.AuxiliaryFunctions;

namespace TestMeasurements.Tests
{
    /// <summary>
    /// Implementation of the different tests of the presentation based on IEnumerable
    /// and PLinq (Single Execution (3 executions), Startup, Steady, Memory)
    /// </summary>
    public static class PLinqTests
    {
        /// <summary>
        /// Single executions (3) of the array tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionSingleExecutionsPLinq(string foundHash)
        {
            var result3 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerablePLinq(foundHash, pwdArray);
                // Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable PLinq: " + result3);

            result3 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerablePLinq(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable PLinq: " + result3);

            result3 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerablePLinq(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable PLinq: " + result3);
        }

        /// <summary>
        /// Startup execution of the IEnumerable+PLinq tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionStartupPLinq(string foundHash)
        {
            var result = Performance.Startup(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerablePLinq(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable PLinq (Startup): " + result);
        }

        /// <summary>
        /// Steady-State execution of the IEnumerable+PLinq tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionSteadyPLinq(string foundHash)
        {
            var result3 = Performance.Steady(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerablePLinq(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable PLinq (Steady): " + result3);
        }

        /// <summary>
        /// Memory execution of the IEnumerable+PLinq tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void IEnumerableVersionMemoryPLinq(string foundHash)
        {
            var result = Performance.Memory(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsIEnumerable(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashIEnumerablePLinq(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("IEnumerable PLinq (Memory): " + result);
        }
    }
}
