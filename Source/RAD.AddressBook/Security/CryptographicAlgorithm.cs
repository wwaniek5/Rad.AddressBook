using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook.Security
{
    public interface ICryptographicAlgorithm
    {
        bool IsPasswordCorrect(string password, Users user);
        string GenerateSalt();
        string GenerateHash(string password, string salt);
    }

    public class SHA512 : ICryptographicAlgorithm
    {
        public bool IsPasswordCorrect(string password, Users user)
        {
            var h = GenerateHash(password, user.Salt);
            return user.Hash.Equals(GenerateHash(password, user.Salt));
        }

        public string GenerateSalt()
        {
            var random = RandomNumberGenerator.Create();
            byte[] salt = new byte[50];
            random.GetBytes(salt);
            return Encoding.UTF8.GetString(salt);
        }

        public string GenerateHash(string password, string salt)
        {
            return Encoding.UTF8.GetString(new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
        }
    }


}

