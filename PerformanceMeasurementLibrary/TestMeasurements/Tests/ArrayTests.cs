using System;
using System.Collections.Generic;
using System.Text;
using PerformanceMeasurementLibrary;
using TestMeasurements.AuxiliaryFunctions;

namespace TestMeasurements.Tests
{
    /// <summary>
    /// Implementation of the different tests of the presentation based on arrays
    /// (Single Execution (3 executions), Startup, Steady, Memory)
    /// </summary>
    public static class ArrayTests
    {
        /// <summary>
        /// Single executions (3) of the array tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void ArrayVersionSingleExecutions(string foundHash)
        {
            var result1 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsArray(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashArray(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("Array: " + result1);

            result1 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsArray(@"..\..\..\data\rockyou.txt");

                string pwd = HashFunctions.RevertSha256HashArray(foundHash, pwdArray);

                //Console.WriteLine(pwd);
            });
            Console.WriteLine("Array: " + result1);

            result1 = Performance.SingleExecution(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsArray(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashArray(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("Array: " + result1);

        }

        /// <summary>
        /// Startup execution of the array tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void ArrayVersionStartup(string foundHash)
        {
            var result1 = Performance.Startup(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsArray(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashArray(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("Array (Startup): " + result1);
        }

        /// <summary>
        /// Steady-State execution of the array tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void ArrayVersionSteady(string foundHash)
        {
            var result1 = Performance.Steady(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsArray(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashArray(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("Array (Steady): " + result1);
        }

        /// <summary>
        /// Memory execution of the array tests
        /// </summary>
        /// <param name="foundHash">Hash to find</param>
        public static void ArrayVersionMemory(string foundHash)
        {
            var result1 = Performance.Memory(() =>
            {
                var pwdArray =
                    FileFunctions.ReadPasswordsAsArray(@"..\..\..\data\rockyou.txt");
                string pwd = HashFunctions.RevertSha256HashArray(foundHash, pwdArray);
                //Console.WriteLine(pwd);
            });
            Console.WriteLine("Array (Memory): " + result1);
        }
    }
}
