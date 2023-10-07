using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCard
{
    internal class Card
    {
        public string SerialNumber;
        public string Type;

        public Card(string SerialNumber, string Type)
        {
            this.SerialNumber = SerialNumber;
            this.Type = Type;
        }

        public override string ToString()
        {
            return $"{SerialNumber} {Type}-Card!";
        }
    }
}
