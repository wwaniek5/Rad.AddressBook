﻿using Caliburn.Micro;
using RAD.AddressBook.Events;

namespace RAD.AddressBook.ViewModels
{
    public sealed class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<GoToNextViewEvent>
    {


        public ShellViewModel(SetupViewModelsFactory setupViewModelsFactory, EventAggregator eventAggregator)
        {

            eventAggregator.Subscribe(this);
            ActivateItem(setupViewModelsFactory.CreateChooseEnvironmentViewModel());
        }

        private string _windowTitle = "Książka Adresowa";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                NotifyOfPropertyChange(() => WindowTitle);
            }
        }

        public void Handle(GoToNextViewEvent nextViewEvent)
        {
            WindowTitle = "Książka Adresowa (" + nextViewEvent.WindowSubtitle + ")";
            ActivateItem(nextViewEvent.ViewModel);
        }
    }
}
