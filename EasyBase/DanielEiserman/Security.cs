using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DanielEiserman.Security
{
	public static class Security
	{
		private static readonly byte[] cSaltBytes = new byte[] { 47, 251, 8, 14, 99 };
        
		/// <summary>
		/// Assymetrisk kryptering av in-variabeln text.
		/// </summary>
		/// <param name="text">Texten som ska krypteras.</param>
		/// <returns>Den krypterade texten</returns>
		public static string EncryptAss(string text)
		{
			// Convert text into a byte array.
			byte[] textBytes = Encoding.UTF8.GetBytes(text);

			// Allocate array, which will hold plain text and salt.
			byte[] textWithSaltBytes =
					new byte[textBytes.Length + cSaltBytes.Length];

			// Copy plain text bytes into resulting array.
			for (int i = 0; i < textBytes.Length; i++)
				textWithSaltBytes[i] = textBytes[i];

			// Append salt bytes to the resulting array.
			for (int i = 0; i < cSaltBytes.Length; i++)
				textWithSaltBytes[textBytes.Length + i] = cSaltBytes[i];

			SHA1Managed hash = new SHA1Managed();

			// Compute hash value of our plain text with appended salt.
			byte[] hashBytes = hash.ComputeHash(textWithSaltBytes);

			// Create array which will hold hash and original salt bytes.
			byte[] hashWithSaltBytes = new byte[hashBytes.Length +
												cSaltBytes.Length];

			// Copy hash bytes into resulting array.
			for (int i = 0; i < hashBytes.Length; i++)
				hashWithSaltBytes[i] = hashBytes[i];

			// Append salt bytes to the result.
			for (int i = 0; i < cSaltBytes.Length; i++)
				hashWithSaltBytes[hashBytes.Length + i] = cSaltBytes[i];

			// Convert result into a base64-encoded string.
			string hashValue = Convert.ToBase64String(hashWithSaltBytes);

			// Return the result.
			return hashValue;
		}

        public static string EncryptAssNoSalt(string text)
        {
            // Convert text into a byte array.
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            // Allocate array, which will hold plain text.
            byte[] textWithSaltBytes = new byte[textBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < textBytes.Length; i++)
                textWithSaltBytes[i] = textBytes[i];

            SHA1Managed hash = new SHA1Managed();

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(textWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        private static string GetRandomVocal(Random rnd)
		{
			string[] vocals = new string[] { "a", "o", "u", "e", "i", "y" };
			int index = rnd.Next(0, vocals.GetUpperBound(0));
			return vocals[index];
		}

		private static string GetRandomConsonant(Random rnd)
		{
			string[] consonants = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };
			int index = rnd.Next(0, consonants.GetUpperBound(0));
			return consonants[index];
		}

		/// <summary>
		/// Genererar ett lösenord på 5 slumpvis valda tecken mellan a och z.
		/// </summary>
		/// <returns>Det genererade lösenordet.</returns>
		public static string GeneratePassword()
		{
			Random rnd = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);

			StringBuilder pw = new StringBuilder(8);
			pw.Append(GetRandomConsonant(rnd));
			pw.Append(GetRandomVocal(rnd));
			pw.Append(GetRandomConsonant(rnd));
			pw.Append(GetRandomConsonant(rnd));
			pw.Append(GetRandomVocal(rnd));

			return pw.ToString();
		}

	}
}
