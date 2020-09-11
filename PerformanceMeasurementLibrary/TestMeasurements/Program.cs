using TestMeasurements.AuxiliaryFunctions;
using TestMeasurements.Tests;

namespace TestMeasurements
{
    class Program
    {
        static void Main(string[] args)
        {
            //In a real-life example, this should
            //be an unknown hash you want to shake it off ;)
            string foundHash = HashFunctions.Sha256Hash("taylor swift");

            // Single execution
            ArrayTests.ArrayVersionSingleExecutions(foundHash);
            IEnumerableTests.IEnumerableVersionSingleExecutions(foundHash);
            PLinqTests.IEnumerableVersionSingleExecutionsPLinq(foundHash);

            // Startup
            //ArrayTests.ArrayVersionStartup(foundHash);
            //IEnumerableTests.IEnumerableVersionStartup(foundHash);
            //PLinqTests.IEnumerableVersionStartupPLinq(foundHash);

            // Steady-state
            //ArrayTests.ArrayVersionSteady(foundHash);
            //IEnumerableTests.IEnumerableVersionSteady(foundHash);
            //PLinqTests.IEnumerableVersionSteadyPLinq(foundHash);

            // Memory
            //ArrayTests.ArrayVersionMemory(foundHash);
            //IEnumerableTests.IEnumerableVersionMemory(foundHash);
            //PLinqTests.IEnumerableVersionMemoryPLinq(foundHash);
        }
    }
}
