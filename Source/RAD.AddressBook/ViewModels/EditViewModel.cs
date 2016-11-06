﻿using Caliburn.Micro;

namespace RAD.AddressBook.ViewModels
{
    internal class EditViewModel:Screen
    {
        private WindowManager _windowManager;
        private Person _person;
        public Person NewPerson { get; set; } = null;

        public EditViewModel(WindowManager _windowManager,Person person)
        {
            this._windowManager = _windowManager;
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
            NewPerson = new Person
            {
                Id = _person.Id,
                Name = Name,
                Surname = Surname,
                Address = Address,
                Phone = Phone,
                Email = Email
            };

            TryClose();
        }
    }
}