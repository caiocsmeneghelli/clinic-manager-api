﻿using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.UpdatePersonalDetail
{
    public class UpdatePatientPersonalDetailCommand : IRequest<Result>
    {
        public int IdPatient { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BloodType { get; set; }
        public string Cpf { get; set; }
    }
}
