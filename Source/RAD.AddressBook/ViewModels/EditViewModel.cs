using Caliburn.Micro;
using RAD.AddressBook.Events;
using RAD.AddressBook.HelpClasses;

namespace RAD.AddressBook.ViewModels
{
    internal class EditViewModel:Screen
    {
        private Person _person;

        public EditViewModel(Person person,AddressBookManager addressBookManager, EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _addressBookManager = addressBookManager;
            _person = person;
            Name = person.Name;
            Surname = person.Surname;
            Address = person.Address;
            Phone = person.Phone;
            Email = person.Email;
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
        private AddressBookManager _addressBookManager;
        private EventAggregator _eventAggregator;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        public void Confirm()
        {
            var newPerson = new Person
            {
                Id = _person.Id, Name = Name, Surname = Surname, Address = Address, Phone = Phone, Email = Email
            };

            _addressBookManager.DeleteFromDatabase(_person);
            _addressBookManager.AddToDatabase(newPerson);
            _eventAggregator.PublishOnCurrentThread(new UpdatePeople());

            TryClose();
        }
    }
}