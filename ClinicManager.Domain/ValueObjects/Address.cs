using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [MaxLength(128)]
        public string Street { get; private set; }

        [Required]
        [MaxLength(128)]
        public string City { get; private set; }

        [Required]
        [StringLength(2)]
        public string UF { get; private set; }

        [Required]
        [MaxLength(64)]
        public string Country { get; private set; }

        public override string ToString()
        {
            return string.Format($"{Street} {City} {UF} {Country}");
        }
    }
}
