using ClinicManager.Application.Commands.Doctors.Create;
using ClinicManager.Application.Commands.Patients.Create;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using ClinicManager.Domain.Services.Users;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Tests.Doctors
{
    public class CreateDoctorTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateDoctorCommand> _validator;
        private readonly IUserService _userService;

        CreateDoctorCommand command;

        public CreateDoctorTest()
        {
            var doctorRepositoryMock = Substitute.For<IDoctorRepository>();
            var userRepositoryMock = Substitute.For<IUserRepository>();

            _unitOfWork = Substitute.For<IUnitOfWork>();
            _unitOfWork.Doctors.Returns(doctorRepositoryMock);
            _unitOfWork.Users.Returns(userRepositoryMock);

            _validator = Substitute.For<IValidator<CreateDoctorCommand>>();
            _userService = Substitute.For<IUserService>();

            command = new CreateDoctorCommand();
            command.Name = "FirstName";
            command.LastName = "LastName";
            command.Birthdate = new DateTime(1995, 12, 15);
            command.PhoneNumber = "PhoneNumber";
            command.Email = "email";
            command.BloodType = "A+";
            command.Cpf = "cpf";

            command.Street = "Street";
            command.City = "City";
            command.Uf = "UF";
            command.Country = "Country";

            command.CRM = "CCDDSSD";
            command.MedicalEspeciality = "MedicalEsp";
        }

        [Fact]
        public async Task Handler_Should_Return_Success()
        {
            // Arrange
            _userService.CreateUserAndValidateLogin(Arg.Any<string>(), Domain.Enums.EProfile.Doctor)
                .Returns(new User("doctorLogin", "password", Domain.Enums.EProfile.Doctor));

            _validator.Validate(Arg.Any<CreateDoctorCommand>())
                .Returns(new ValidationResult());

            // Act
            var handler = new CreateDoctorCommandHandler(_unitOfWork, _validator, _userService);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }
    }
}
