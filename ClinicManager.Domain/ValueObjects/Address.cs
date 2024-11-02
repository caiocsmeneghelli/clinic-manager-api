using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.ValueObjects
{
    public record Address
    {
        public Address(string street, string city, string uF, string country)
        {
            Street = street;
            City = city;
            UF = uF;
            Country = country;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string UF { get; private set; }
        public string Country { get; private set; }

        public override string ToString()
        {
            return string.Format($"{Street} {City} {UF} {Country}");
        }
    }
}
