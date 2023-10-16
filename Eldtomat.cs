using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCard
{
    internal class Eldtomat : Card
    {   // Defining a constructer for the sub-class Kristallhäst and calling the constructor of the base class by passing the required parameters
        public Eldtomat(string serialNumber, string type) : base(serialNumber, type) { }
    }
}
