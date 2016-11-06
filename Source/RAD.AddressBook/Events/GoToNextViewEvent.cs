using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook.Events
{
    public class GoToNextViewEvent
    {
        public IScreen ViewModel { get; set; }
        public string WindowSubtitle { get; set; }

        public GoToNextViewEvent(IScreen viewModel, string windowTitle)
        {
            WindowSubtitle = windowTitle;
            ViewModel = viewModel;
        }
    }
}
