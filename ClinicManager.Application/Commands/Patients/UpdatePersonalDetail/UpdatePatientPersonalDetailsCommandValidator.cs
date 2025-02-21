using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.UpdatePersonalDetail
{
    public class UpdatePatientPersonalDetailsCommandValidator : AbstractValidator<UpdatePatientPersonalDetailCommand>
    {
        public UpdatePatientPersonalDetailsCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Nome não pode ser vazio.")
                .MaximumLength(128).WithMessage("Nome não pode ter mais de 128 caracteres.");

            RuleFor(c => c.LastName).NotEmpty().WithMessage("Sobrenome não pode ser vazio.")
                .MaximumLength(128).WithMessage("Sobrenome não pode ter mais de 128 caracteres.");

            RuleFor(c => c.Birthdate).NotEmpty().WithMessage("Data de nascimento não pode ser vazio.");

            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Telefone de contato não pode ser vazio.")
                .Length(11).WithMessage("Telefone de contato precisa ter 11 digitos.");

            RuleFor(c => c.Email).NotEmpty().WithMessage("E-mail não pode ser vazio")
                .EmailAddress().WithMessage("E-mail não válido.");

            RuleFor(c => c.Cpf).NotEmpty().WithMessage("CPF não pode ser vazio.")
                .Length(11).WithMessage("O campo CPF precisa ter 11 digitos.");

            RuleFor(c => c.BloodType).NotEmpty().WithMessage("O campo Tipo Sanguíneo não pode ser vazio.");
        }
    }
}
