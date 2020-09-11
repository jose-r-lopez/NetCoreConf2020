using System.Collections.Generic;

namespace TestMeasurements.AuxiliaryFunctions
{
    /// <summary>
    /// Methods that manipulate text files
    /// </summary>
    static class FileFunctions
    {
        /// <summary>
        /// Read a password file (one password per line) as an array
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <returns>Array with the read passwords</returns>
        public static string[] ReadPasswordsAsArray(string filePath)
        {
            string line;
            var list = new List<string>();
            // Read the file and add its contents line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
                list.Add(line);

            file.Close();
            return list.ToArray();
        }

        /// <summary>
        /// Read a password file (one password per line) as a IEnumerable,
        /// using generators
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <returns>Generator with the read passwords</returns>
        public static IEnumerable<string> ReadPasswordsAsIEnumerable
            (string filePath)
        {
            string line;
            var list = new List<string>();
            // Read the file and serve lines using a generator  
            System.IO.StreamReader file =
                new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
                yield return line;
            file.Close();
        }
    }
}
