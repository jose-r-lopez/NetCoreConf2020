using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TestMeasurements.AuxiliaryFunctions
{
    /// <summary>
    /// Utility class to generate and revert SHA256 hashes
    /// </summary>
    static class HashFunctions
    {
        /// <summary>
        /// SHA256 hash generator (global)
        /// </summary>
        private static SHA256 _mySHA256 = SHA256.Create();

        /// <summary>
        /// Obtains the SHA256 hash equivalent of a provided text, also in text format 
        /// </summary>
        /// <param name="pwd">Text to obtain the hash from</param>
        /// <param name="salt">Salt bits (optional) that will be added to the
        /// text to hash to mitigate brute-force attacks</param>
        /// <returns></returns>
        public static string Sha256Hash(string pwd, string salt = "")
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data =
                _mySHA256.ComputeHash(Encoding.UTF8.GetBytes(pwd + salt));
            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            foreach (var t in data)
                sBuilder.Append(t.ToString("x2"));

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Obtain the original text that correspond to a passed hash, if this
        /// text is present in the array provided as the second parameter (array)
        /// </summary>
        /// <param name="foundHash">Hash to revert</param>
        /// <param name="pwdArray">strings to use to try to guess the original
        /// hash text</param>
        /// <returns></returns>
        internal static string RevertSha256HashArray(string foundHash,
            string[] pwdArray)
        {
            foreach (var pwd in pwdArray)
            {
                if (foundHash.Equals(Sha256Hash(pwd)))
                    return pwd;
            }
            return null;
        }

        /// <summary>
        /// Obtain the original text that correspond to a passed hash, if this
        /// text is present in the array provided as the second parameter (IEnumerable).
        /// This method uses Linq internally.
        /// </summary>
        /// <param name="foundHash">Hash to revert</param>
        /// <param name="pwds">strings to use to try to guess the original
        /// hash text</param>
        /// <returns></returns>
        internal static string RevertSha256HashIEnumerable
            (string foundHash, IEnumerable<string> pwds)
        {
            return pwds.FirstOrDefault(
                pwd => foundHash.Equals(Sha256Hash(pwd)));
        }

        /// <summary>
        /// Obtain the original text that correspond to a passed hash, if this
        /// text is present in the array provided as the second parameter (IEnumerable).
        /// This method uses PLinq internally.
        /// </summary>
        /// <param name="foundHash">Hash to revert</param>
        /// <param name="pwds">strings to use to try to guess the original
        /// hash text</param>
        /// <returns></returns>
        internal static string RevertSha256HashIEnumerablePLinq
            (string foundHash, IEnumerable<string> pwds)
        {
            return pwds.AsParallel().FirstOrDefault(
                pwd => foundHash.Equals(Sha256Hash(pwd)));
        }
    }
}
