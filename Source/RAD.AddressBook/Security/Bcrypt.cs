using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook.Security
{
    public class Bcrypt : ICryptographicAlgorithm
    {
        public bool IsPasswordCorrect(string password, Users user)
        {
            return user.Hash.Equals(GenerateHash(password, user.Salt));
        }

        public string GenerateSalt()
        {
            //todo there is no point of storing hash if it's prepended before hash
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string GenerateHash(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}

