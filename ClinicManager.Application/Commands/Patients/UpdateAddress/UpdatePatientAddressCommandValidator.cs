using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.UpdateAddress
{
    public class UpdatePatientAddressCommandValidator : AbstractValidator<UpdatePatientAddressCommand>
    {
        public UpdatePatientAddressCommandValidator()
        {
            RuleFor(c => c.Street).NotEmpty().WithMessage("O campo Rua não pode ser vazio.")
                .MaximumLength(128).WithMessage("O campo Rua deve ter no máximo 128 caracteres.");

            RuleFor(c => c.City).NotEmpty().WithMessage("O campo Cidade não pode ser vazio.")
                .MaximumLength(128).WithMessage("O campo Cidade deve ter no máximo 128 caracteres.");

            RuleFor(c => c.Uf).NotEmpty().WithMessage("O campo UF não pode ser vazio.")
                .MaximumLength(128).WithMessage("O campo UF deve ter no máximo 128 caracteres.");

            RuleFor(c => c.Country).NotEmpty().WithMessage("O campo Pais não pode ser vazio.")
                .MaximumLength(128).WithMessage("O campo Pais deve ter no máximo 128 caracteres.");
        }
    }
}
