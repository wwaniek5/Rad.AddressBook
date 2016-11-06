using Caliburn.Micro;
using Microsoft.Practices.Unity;
using RAD.AddressBook.ViewModels;

namespace RAD.AddressBook
{
    public class SetupViewModelsFactory
    {
        private readonly IUnityContainer _container;

        public SetupViewModelsFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IScreen CreateChangeBayStatusViewModel()
        {
            return _container.Resolve<AddressBookViewModel>();
        }

        public IScreen CreateChooseEnvironmentViewModel()
        {
            return _container.Resolve<LoginViewModel>();
        }

        public IScreen CreateCustomStringViewModel()
        {
            return _container.Resolve<CustomConnectionStringViewModel>();
        }
    }
}
