using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.Update
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(reg => reg.Height).GreaterThan(0).WithMessage("A altura não pode ser 0.");
            RuleFor(reg => reg.Weight).GreaterThan(0).WithMessage("O peso não pode ser 0.");
        }
    }
}
