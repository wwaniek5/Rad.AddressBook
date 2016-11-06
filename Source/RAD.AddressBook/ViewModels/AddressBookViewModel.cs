using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RAD.AddressBook.Security;

namespace RAD.AddressBook.ViewModels
{
    public class AddressBookViewModel : Screen
    {
        private readonly EventAggregator _eventAggregator;
        private readonly EnvironmentSettings _settings;
        private readonly AES _encryptor;
        private WindowManager _windowManager;


        public AddressBookViewModel(
            EnvironmentSettings settings,
            EventAggregator eventAggregator,
            AES encryptor,
            WindowManager windowManager
            )
            {
                _settings = settings;
                _eventAggregator = eventAggregator;
                _encryptor = encryptor;
               _encryptor.SetPasword(settings.Password);
                _windowManager = windowManager;
            }

        public BindableCollection<Person> People { get; set; } = new BindableCollection<Person>();


        protected override  void OnInitialize()
        {
            base.OnInitialize();
            UpdatePeople();
        }

        private void UpdatePeople()
        {
            using (var context = new DatabaseEntities())
            {
                var addresses = context
                    .AddressBook
                    .Where(a => a.Owner == _settings.UserName)
                    .ToList();

                People.Clear();
                foreach (var address in addresses)
                {
                    People.Add(new Person
                        {
                             Id= address.Id,
                            Name =_encryptor.Decrypt(address.Name),
                            Surname = _encryptor.Decrypt(address.Surname),
                            Address = _encryptor.Decrypt(address.Address),
                            Phone = _encryptor.Decrypt(address.Phone),
                            Email = _encryptor.Decrypt(address.Email)
                          }
                    );
                }

            }
        }

        private Person _person = new Person();
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                NotifyOfPropertyChange(() => Person);
            }
        }

    //private string _person="";
    //public string Person
    //{
    //    get { return _person; }
    //    set
    //    {
    //        _person = value;
    //        NotifyOfPropertyChange(() => Person);
    //    }
    //}

    //private string _surname = "";
    //public string Surname
    //{
    //    get { return _surname; }
    //    set
    //    {
    //        _surname = value;
    //        NotifyOfPropertyChange(() => Surname);
    //    }
    //}

    //private string _address = "";
    //public string Address
    //{
    //    get { return _address; }
    //    set
    //    {
    //        _address = value;
    //        NotifyOfPropertyChange(() => Address);
    //    }
    //}

    //private string _phone = "";
    //public string Phone
    //{
    //    get { return _phone; }
    //    set
    //    {
    //        _phone = value;
    //        NotifyOfPropertyChange(() => Phone);
    //    }
    //}

    //private string _email = "";
    //public string Email
    //{
    //    get { return _email; }
    //    set
    //    {
    //        _email = value;
    //        NotifyOfPropertyChange(() => Email);
    //    }
    //}

    public void Add()
        {
            using (var context = new DatabaseEntities())
            {
                var addressBook = new AddressBook();
                addressBook.Owner = _settings.UserName;
                addressBook.Name = _encryptor.Encrypt(Person.Name);
                addressBook.Surname = _encryptor.Encrypt(Person.Surname);
                addressBook.Address = _encryptor.Encrypt(Person.Address);
                addressBook.Phone = _encryptor.Encrypt(Person.Phone);
                addressBook.Email = _encryptor.Encrypt(Person.Email);


                context.AddressBook.Add(addressBook);
                context.SaveChanges();
            }

            UpdatePeople();
        }

        public void Delete(Person person)
        {
            using (var context = new DatabaseEntities())
            {
                var toBeDeleted=context
                    .AddressBook
                    .Single(a => a.Id == person.Id);

                context
                    .AddressBook
                    .Remove(toBeDeleted);

                context.SaveChanges();
            }

            UpdatePeople();
        }

        public void Edit(Person person)
        {
            var model = new EditViewModel(_windowManager,person);
            _windowManager.ShowWindow(model);
            if (model.NewPerson != null)
            {
                Delete(person);
               // Add(model.NewPerson);
            }

            //using (var context = new DatabaseEntities())
            //{
            //    var toBeDeleted = context
            //        .AddressBook
            //        .Single(a => a.Id == person.Id);

            //    context
            //        .AddressBook
            //        .Remove(toBeDeleted);

            //    context.SaveChanges();
            //}

            //UpdatePeople();
        }


    }

  
}
