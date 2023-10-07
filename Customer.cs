using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCard
{
    internal class Customer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Municipality { get; set; }

        public Customer(string id, string name, string municipality)
        {
            ID = id;
            Name = name;
            Municipality = municipality;
        }

        public override string ToString()
        {
            return $"{ID} {Name} {Municipality}";
        }
    }
}
