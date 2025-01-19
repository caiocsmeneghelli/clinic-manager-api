using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class Service : Entity
    {
        public Service(string title, string description, decimal cost, int duration)
        {
            Title = title;
            Description = description;
            Cost = cost;
            Duration = duration;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Cost { get; private set; }
        public int Duration { get; private set; }
        public int IdMedicalCare { get; private set; }
        public MedicalAppointment MedicalCare { get; private set; }
    }
}
