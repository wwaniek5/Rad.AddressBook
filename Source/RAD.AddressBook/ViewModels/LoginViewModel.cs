﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using RAD.AddressBook.Events;
using RAD.AddressBook.Security;

namespace RAD.AddressBook.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly EventAggregator _eventAggregator;
        private readonly EnvironmentSettings _settings;
        private readonly SetupViewModelsFactory _setupViewModelsFactory;

        public LoginViewModel(
            SetupViewModelsFactory setupViewModelsFactory,
            EnvironmentSettings settings,
            EventAggregator eventAggregator,
            ICryptographicAlgorithm cryptographicAlgorithm
            )
        {
            _settings = settings;
            _eventAggregator = eventAggregator;
            _setupViewModelsFactory = setupViewModelsFactory;
            _cryptographicAlgorithm = cryptographicAlgorithm;
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
        private ICryptographicAlgorithm _cryptographicAlgorithm;

        public string Password
        {
            get { return _password; }

            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }


        public void Login()
        {
           
            using (var context = new DatabaseEntities())
            {
               var user =context.Users.FirstOrDefault(u=>u.UserName==UserName);
                if (user == null || _cryptographicAlgorithm.IsPasswordCorrect(Password, user)==false)
                {
                    MessageBox.Show("Błąd");
                    return;
                }
            }


            _settings.Password = Password;
            _settings.UserName = UserName;



            _eventAggregator.PublishOnCurrentThread(new GoToNextViewEvent(_setupViewModelsFactory.CreateAddressBookViewModel(), UserName));
        }

        public void Register()
        {
            _eventAggregator.PublishOnCurrentThread(new GoToNextViewEvent(_setupViewModelsFactory.CreateRegisterViewModel(), ""));
        }
    }
}
