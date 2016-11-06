using Caliburn.Micro;

namespace RAD.AddressBook.ViewModels
{
    public class Person: PropertyChangedBase
    {
        internal int Id;

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
    }
}