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

        public IScreen CreateAddressBookViewModel()
        {
            return _container.Resolve<AddressBookViewModel>();
        }

        public IScreen CreateChooseEnvironmentViewModel()
        {
            return _container.Resolve<LoginViewModel>();
        }

        public IScreen CreateRegisterViewModel()
        {
            return _container.Resolve<RegisterViewModel>();
        }
    }
}
