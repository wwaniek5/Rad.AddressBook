using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RAD.AddressBook.Security;
using RAD.AddressBook.ViewModels;

namespace RAD.AddressBook.HelpClasses
{
   public  class AddressBookManager
    {
        private EnvironmentSettings _settings;
        private EventAggregator _eventAggregator;
        private AES _encryptor;

        public AddressBookManager(
            EnvironmentSettings settings,
            EventAggregator eventAggregator,
            AES encryptor

            )
        {
            _settings = settings;
            _eventAggregator = eventAggregator;
            _encryptor = encryptor;
            _encryptor.SetPasword(settings.Password);
        }


        public void AddToDatabase(Person person)
        {
            using (var context = new DatabaseEntities())
            {
                var addressBook = new AddressBook
                {
                    Owner = _settings.UserName,
                    Name = _encryptor.Encrypt(person.Name),
                    Surname = _encryptor.Encrypt(person.Surname),
                    Address = _encryptor.Encrypt(person.Address),
                    Phone = _encryptor.Encrypt(person.Phone),
                    Email = _encryptor.Encrypt(person.Email)
                };


                context.AddressBook.Add(addressBook);
                context.SaveChanges();
            }
        }

        public  void DeleteFromDatabase(Person person)
        {
            using (var context = new DatabaseEntities())
            {
                var toBeDeleted = context
                    .AddressBook
                    .Single(a => a.Id == person.Id);

                context
                    .AddressBook
                    .Remove(toBeDeleted);

                context.SaveChanges();
            }
        }

        internal IEnumerable<Person> GetAllPeople()
        {
            using (var context = new DatabaseEntities())
            {
                var addresses = context
                    .AddressBook
                    .Where(a => a.Owner == _settings.UserName)
                    .ToList();

                return addresses.Select(address => new Person
                {
                    Id = address.Id,
                    Name = _encryptor.Decrypt(address.Name),
                    Surname = _encryptor.Decrypt(address.Surname),
                    Address = _encryptor.Decrypt(address.Address),
                    Phone = _encryptor.Decrypt(address.Phone),
                    Email = _encryptor.Decrypt(address.Email)
                }).ToList();

            }
        }
    }
}
