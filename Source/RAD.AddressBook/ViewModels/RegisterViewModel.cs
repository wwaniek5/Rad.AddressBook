using Caliburn.Micro;
using RAD.AddressBook.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RAD.AddressBook.Security;

namespace RAD.AddressBook.ViewModels
{
    public class RegisterViewModel : Screen
    {
        private readonly EventAggregator _eventAggregator;
        private readonly EnvironmentSettings _settings;
        private SetupViewModelsFactory _setupViewModelsFactory;



        public RegisterViewModel(
            SetupViewModelsFactory setupViewModelsFactory,
            EnvironmentSettings settings,
            EventAggregator eventAggregator,
            UserFactory userFactory
            )
        {
            _settings = settings;
            _eventAggregator = eventAggregator;
            _setupViewModelsFactory = setupViewModelsFactory;
            _userFactory = userFactory;

        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }

            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        private string _password;
        private UserFactory _userFactory;

        public string Password
        {
            get { return _password; }

            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }





        public void Ok()
        {
            try
            {
                _userFactory.Create(UserName, Password);
            }
            catch (DuplikateUserException e)
            {
                MessageBox.Show("Login w użyciu");
                return;
            }
      

            _settings.UserName = UserName;
            _settings.Password = Password;
            _eventAggregator.PublishOnCurrentThread(new GoToNextViewEvent(_setupViewModelsFactory.CreateAddressBookViewModel(), UserName));
        }


    }
}

