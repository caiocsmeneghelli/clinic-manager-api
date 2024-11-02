using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class Service : Entity
    {
        public Service(string name, string description, decimal cost, int duration)
        {
            Name = name;
            Description = description;
            Cost = cost;
            Duration = duration;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Cost { get; private set; }
        public int Duration { get; private set; }
    }
}
