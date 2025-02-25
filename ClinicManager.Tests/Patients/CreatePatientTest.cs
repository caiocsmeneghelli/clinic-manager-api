﻿using ClinicManager.Domain.Repositories;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Application.Commands.Patients.Create;
using NSubstitute;
using FluentValidation;
using ClinicManager.Domain.Services.Users;
using FluentValidation.Results;
using ClinicManager.Domain.Entities;

namespace ClinicManager.Tests.Patients
{
    public class CreatePatientTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreatePatientCommand> _validator;
        private readonly IUserService _userService;

        CreatePatientCommand command;

        public CreatePatientTest()
        {
            var patientRepositoryMock = Substitute.For<IPatientRepository>();
            var userRepositoryMock = Substitute.For<IUserRepository>();

            _unitOfWork = Substitute.For<IUnitOfWork>();
            _unitOfWork.Patients.Returns(patientRepositoryMock);
            _unitOfWork.Users.Returns(userRepositoryMock);

            _validator = Substitute.For<IValidator<CreatePatientCommand>>();
            _userService = Substitute.For<IUserService>();

            command = new CreatePatientCommand();
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

            command.Height = 1.75m;
            command.Weight = 60.4m;
        }

        [Fact]
        public async void HandlerShouldReturnSuccess()
        {
            // Arrange
            _userService.CreateUserAndValidateLogin(Arg.Any<string>(), Domain.Enums.EProfile.Patient)
                .Returns(new User("patientLogin", "password", Domain.Enums.EProfile.Patient));

            _validator.Validate(Arg.Any<CreatePatientCommand>())
                .Returns(new ValidationResult());

            // Act
            var handler = new CreatePatientCommandHandler(_unitOfWork, _validator, _userService);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void HandlerShouldReturnError()
        {
            // Arrange
            var validationErrors = new List<ValidationFailure>()
            {
                new ValidationFailure("Cpf", "Cpf inválido"),
                new ValidationFailure("Email", "E-mail inválido")
            };

            _userService.CreateUserAndValidateLogin(Arg.Any<string>(), Domain.Enums.EProfile.Patient)
                .Returns(new User("login", "password", Domain.Enums.EProfile.Patient));

            _validator.Validate(Arg.Any<CreatePatientCommand>())
                .Returns(new ValidationResult(validationErrors));

            // Act
            var handler = new CreatePatientCommandHandler(_unitOfWork, _validator, _userService);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(2, result.Messages.ToList().Count);
            Assert.Equal(400, result.StatusCode);
            Assert.Null(result.Data);
        }
    }
}
