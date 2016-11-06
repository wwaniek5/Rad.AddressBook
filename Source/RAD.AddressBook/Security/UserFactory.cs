using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook.Security
{
    public class UserFactory
    {
        private readonly ICryptographicAlgorithm _cryptographicAlgorithm;

        public UserFactory(ICryptographicAlgorithm algorithm)
        {
            _cryptographicAlgorithm = algorithm;
        }

        public void Create(string userName, string password)
        {
            using (var context = new DatabaseEntities())
            {
                var user = new Users();
                var salt = _cryptographicAlgorithm.GenerateSalt();
                user.UserName = userName;
                user.Salt = salt;
                user.Hash = _cryptographicAlgorithm.GenerateHash(password, salt);

                if (context.Users.FirstOrDefault(u => u.UserName == userName) != null)
                {
                    throw new DuplikateUserException();
                }

                context.Users.Add(user);
               context.SaveChanges();
            }
        }
    }
}