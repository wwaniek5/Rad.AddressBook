using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RAD.AddressBook.HelpClasses;
using RAD.AddressBook.Security;

namespace RAD.AddressBook.ViewModels
{
    public class AddressBookViewModel : Screen
    {
        private readonly EventAggregator _eventAggregator;
        private readonly EnvironmentSettings _settings;
        private readonly AES _encryptor;
        private WindowManager _windowManager;
        private AddressBookManager _addressBookManager;


        public AddressBookViewModel(
            EnvironmentSettings settings,
            EventAggregator eventAggregator,
            AES encryptor,
            WindowManager windowManager,
            AddressBookManager addressBookManager

            )
            {
                _settings = settings;
                _eventAggregator = eventAggregator;
                _encryptor = encryptor;
               _encryptor.SetPasword(settings.Password);
                _windowManager = windowManager;
                _addressBookManager = addressBookManager;
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
                IEnumerable<Person> people= _addressBookManager.GetAllPeople();

               
                People.Clear();
                foreach (var person in people)
                {
                    People.Add(person                    );
                }

            }
        }


        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private string _surname = "";
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                NotifyOfPropertyChange(() => Surname);
            }
        }

        private string _address = "";
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        private string _phone = "";
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                NotifyOfPropertyChange(() => Phone);
            }
        }

        private string _email = "";
       

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        public void Add()
        {
            _addressBookManager.AddToDatabase( new Person
                {
                    Name = Name,
                    Surname = Surname,
                    Address = Address,
                    Phone = Phone,
                    Email = Email
                });

            UpdatePeople();
        }

       

        public void Delete(Person person)
        {
            _addressBookManager.DeleteFromDatabase(person);

            UpdatePeople();
        }



        public void Edit(Person person)
        {
            var model = new EditViewModel(_windowManager,person);
            _windowManager.ShowWindow(model);
            if (model.NewPerson != null)
            {
             //   DeleteFromDatabase(person);
             //  AddToDatabase(model.NewPerson);
                UpdatePeople();
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
