using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RAD.AddressBook.HelpClasses
{
    public interface IExceptionCommunicator
    {

        void Show(Exception e);
    }
    public class ExceptionCommunicator : IExceptionCommunicator
    {
        public void Show(Exception e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
