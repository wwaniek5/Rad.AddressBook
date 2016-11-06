using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook
{
    public class EnvironmentSettings
    {
        public string ConnectionString { get; internal set; }
        public bool UserUsesTheirOwnString { get; internal set; }
        public string Environment { get; internal set; }
    }
}
