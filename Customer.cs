using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCard
{
    internal class Customer
    {
        public string ID; // Declaring this field to access the customer's ID number
        public string Name; // Declaring this field to access the customer's name
        public string City; // Declaring this field to access the customer's city

        public Customer(string id, string name, string city) // // Passing the parameters inside the class constructor
        {
            ID = id; // initializing a value to the ID field
            Name = name; // initializing a value to the Name field
            City = city; // initializing a value to the City field
        }

        public override string ToString() // Defining an override ToString method to provide a string representation of the fields
        {
            return $"{ID} {Name} {City}";
        }
    }
}
