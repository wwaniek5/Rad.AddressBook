using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RAD.AddressBook.Events;
using RAD.AddressBook.HelpClasses;
using RAD.AddressBook.Security;

namespace RAD.AddressBook.ViewModels
{
    public class AddressBookViewModel : Screen, IHandle<UpdatePeople>
    {
        private readonly EventAggregator _eventAggregator;
        private WindowManager _windowManager;
        private AddressBookManager _addressBookManager;


        public AddressBookViewModel(
            EventAggregator eventAggregator,
            WindowManager windowManager,
            AddressBookManager addressBookManager

            )
            {

                _eventAggregator = eventAggregator;
                 eventAggregator.Subscribe(this);
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
                People.Clear();
                foreach (var person in _addressBookManager.GetAllPeople())
                {
                    People.Add(person);
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
            var model = new EditViewModel(person, _addressBookManager, _eventAggregator);
            _windowManager.ShowWindow(model);
        }

        public void Handle(UpdatePeople message)
        {
            UpdatePeople();
        }
    }
}
