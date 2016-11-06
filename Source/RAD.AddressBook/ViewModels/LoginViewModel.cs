using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RAD.AddressBook.Events;

namespace RAD.AddressBook.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly EventAggregator _eventAggregator;
        private readonly EnvironmentSettings _settings;
        private string _selectedEnvironment = "Dev";
        private SetupViewModelsFactory _setupViewModelsFactory;

        public LoginViewModel(
            SetupViewModelsFactory setupViewModelsFactory,
            EnvironmentSettings settings,
            EventAggregator eventAggregator

            )
        {
            _settings = settings;
            _eventAggregator = eventAggregator;
            _setupViewModelsFactory = setupViewModelsFactory;
        }

        public BindableCollection<string> Environments { get; set; } = new BindableCollection<string>
        {
            "Dev",
            "Test"
        };

        public string SelectedEnvironment
        {
            get { return _selectedEnvironment; }

            set
            {
                _selectedEnvironment = value;
                NotifyOfPropertyChange(() => SelectedEnvironment);
            }
        }


        public void Ok()
        {
            _settings.Environment = SelectedEnvironment;
            _eventAggregator.PublishOnCurrentThread(new GoToNextViewEvent(_setupViewModelsFactory.CreateChangeBayStatusViewModel(), SelectedEnvironment));
        }

        public void OtherConnectionString()
        {
            _settings.UserUsesTheirOwnString = true;
            _settings.Environment = SelectedEnvironment;
            _eventAggregator.PublishOnCurrentThread(new GoToNextViewEvent(_setupViewModelsFactory.CreateCustomStringViewModel(), ""));
        }
    }
}
