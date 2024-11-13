using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.User
{
    public class PasswordHasher
    {
        public static string HashPassword(string userPassword)
        {
            // Splitting salt and hash
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);


            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                /* Notice
                Since it is a portfolio project and I have no intention of
                using this backend for a real e-commerce, I will leave the 
                iterations at 10 to save resources and save time in the
                hashing process. 
                */
                iterationCount: 10,
                numBytesRequested: 512 / 8
            ));

            // Saving the salt and hash in the same string
            string saltAndHash = Convert.ToBase64String(salt) + ":" + hashed;

            return saltAndHash;
        }

        public static bool VerifyPassword(string userPassword, string saltAndHashFromDb)
        {
            // Splitting salt and hash
            string[] parts = saltAndHashFromDb.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            string hashFromDb = parts[1];

            string hashedInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                /* Notice
                Since it is a portfolio project and I have no intention of
                using this backend for a real e-commerce, I will leave the 
                iterations at 10 to save resources and save time in the
                hashing process. 
                */
                iterationCount: 10,
                numBytesRequested: 512 / 8
            ));

            return hashedInput == hashFromDb;
        }
    }
}
