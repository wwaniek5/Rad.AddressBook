using Caliburn.Micro;
using RAD.AddressBook.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook.ViewModels
{
    public sealed class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<GoToNextViewEvent>
    {


        public ShellViewModel(SetupViewModelsFactory setupViewModelsFactory, EventAggregator eventAggregator)
        {

            eventAggregator.Subscribe(this);
            ActivateItem(setupViewModelsFactory.CreateChooseEnvironmentViewModel());
        }

        private string _windowTitle = "Events Simulation Tool";
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
            WindowTitle = "Events Simulation Tool (" + nextViewEvent.WindowSubtitle + ")";
            ActivateItem(nextViewEvent.ViewModel);
        }
    }
}
