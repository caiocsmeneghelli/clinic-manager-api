using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class MedicalCare : Entity
    {
        public MedicalCare(int idPatient, int idDoctor, int idService, 
            string healthInsurance, DateTime start, DateTime end)
        {
            IdPatient = idPatient;
            IdDoctor = idDoctor;
            IdService = idService;
            HealthInsurance = healthInsurance;
            Start = start;
            End = end;
        }

        public int IdPatient { get; private set; }
        public int IdDoctor { get; private set; }
        public int IdService { get; private set; }
        public string HealthInsurance { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
    }
}
