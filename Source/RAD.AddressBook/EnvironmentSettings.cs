using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook
{
    public class EnvironmentSettings
    {
        public string UserName { get; internal set; }
        public bool UserUsesTheirOwnString { get; internal set; }
        public string Password { get; internal set; }
    }
}
