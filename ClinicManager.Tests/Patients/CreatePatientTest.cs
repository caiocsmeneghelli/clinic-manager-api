using ClinicManager.Domain.Repositories;
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

        public CreatePatientTest()
        {
            var patientRepositoryMock = Substitute.For<IPatientRepository>();
            var userRepositoryMock = Substitute.For<IUserRepository>();

            _unitOfWork = Substitute.For<IUnitOfWork>();
            _unitOfWork.Patients.Returns(patientRepositoryMock);
            _unitOfWork.Users.Returns(userRepositoryMock);

            _validator = Substitute.For<IValidator<CreatePatientCommand>>();
            _userService = Substitute.For<IUserService>();
        }

        [Fact]
        public async void HandlerShouldReturnSuccess()
        {
            // Arrange
            _unitOfWork
                .Patients
                .GetByIdAsNoTrackingAsync(Arg.Any<int>())
                .Returns(Task.FromResult<Patient?>(null));

            _userService.CreateUserAndValidateLogin(Arg.Any<string>(), Domain.Enums.EProfile.Patient)
                .Returns(new User("login", "password", Domain.Enums.EProfile.Patient));

            _validator.Validate(Arg.Any<CreatePatientCommand>())
                .Returns(new ValidationResult());

            // Act
            var command = new CreatePatientCommand();
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

            var handler = new CreatePatientCommandHandler(_unitOfWork, _validator, _userService);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
    }
}
