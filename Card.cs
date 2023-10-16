using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCard
{
    internal class Card
    {
        public string SerialNumber; // Declaring this field to access the cards serial number
        public string Type; // Declaring this field to access the cards type

        public Card(string serialNumber, string type) // Passing the parameters inside the class constructor
        {
            SerialNumber = serialNumber; // initializing a value to the field
            Type = type; // initializing a value to the field
        }

        public override string ToString() // Defining an override ToString method to provide a string representation of the fields
        {
            return $"{SerialNumber} {Type}-Card!";
        }
    }
}
