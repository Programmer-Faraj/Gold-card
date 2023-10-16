using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCard
{
    internal class Dunderkatt : Card
    {   // Defining a constructer for the sub-class Dunderkatt and calling the constructor of the base class by passing the required parameters
        public Dunderkatt(string serialNumber, string type) : base(serialNumber, type) { }
    }
}
