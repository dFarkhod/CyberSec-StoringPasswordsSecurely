using System.Security.Cryptography;

namespace HashingWithSalt
{
    internal class HashingHelper
    {
        public static string GetHash(string input)
        {
            // Generate a random salt
            var salt = new byte[32];
            RandomNumberGenerator.Create().GetBytes(salt);

            // Create the Rfc2898DeriveBytes object with the input and salt
            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 20000, HashAlgorithmName.SHA256);

            // Get the hash value
            byte[] hash = pbkdf2.GetBytes(32);  // 32-bytes for SHA256, 64-bytes for SHA512

            // Combine the salt and hash
            byte[] hashBytes = new byte[64];
            Array.Copy(salt, 0, hashBytes, 0, 32);
            Array.Copy(hash, 0, hashBytes, 32, 32);

            // Convert the combined bytes to a base64 string
            string hashedPassword = Convert.ToBase64String(hashBytes);
            return hashedPassword;
        }

        public static bool IsHashValid(string input, string hashedValue)
        {
            try
            {
                // Convert the hashed password from base64 string to bytes
                byte[] hashBytes = Convert.FromBase64String(hashedValue);

                // Extract the salt from the hashBytes
                byte[] salt = new byte[32];
                Array.Copy(hashBytes, 0, salt, 0, 32);

                // Create the Rfc2898DeriveBytes object with the password and extracted salt
                var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 20000, HashAlgorithmName.SHA256);

                // Compute the hash of the given password
                byte[] hash = pbkdf2.GetBytes(32); // 32-bytes for SHA256, 64-bytes for SHA512

                // Compare the computed hash with the stored hash
                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 32] != hash[i])
                    {
                        return false;
                    }
                }
            }
            catch 
            {
                return false;
            }

            return true;
        }
    }
}
