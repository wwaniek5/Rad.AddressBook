using Caliburn.Micro;
using RAD.AddressBook.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook.ViewModels
{
    public class CustomConnectionStringViewModel : Screen
    {
        private readonly EventAggregator _eventAggregator;
        private readonly EnvironmentSettings _settings;

        private string _connectionString;
        private SetupViewModelsFactory _setupViewModelsFactory;

        public string ConnectionString
        {
            get { return _connectionString; }

            set
            {
                _connectionString = value;
                NotifyOfPropertyChange(() => ConnectionString);
            }
        }

        public CustomConnectionStringViewModel(SetupViewModelsFactory setupViewModelsFactory, EnvironmentSettings settings,
            EventAggregator eventAggregator)
        {
            _settings = settings;
            _eventAggregator = eventAggregator;
            _setupViewModelsFactory = setupViewModelsFactory;

        }


        public void Ok()
        {
            _settings.ConnectionString = ConnectionString;
            _eventAggregator.PublishOnCurrentThread(new GoToNextViewEvent(_setupViewModelsFactory.CreateChangeBayStatusViewModel(), "Custom connection string"));
        }


    }
}

