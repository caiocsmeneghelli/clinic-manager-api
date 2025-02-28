using ClinicManager.Application.Commands.Doctors.Update;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Net;

namespace ClinicManager.Tests.Doctors
{
    public class UpdateDoctorTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<UpdateDoctorCommand>> _validatorMock;
        private readonly UpdateDoctorCommandHandler _handler;

        public UpdateDoctorTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UpdateDoctorCommand>>();
            _handler = new UpdateDoctorCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            // Arrange
            var command = new UpdateDoctorCommand() { IdDoctor = 1, CRM = "CRMTEST" };
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor());
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var doctor = (Doctor)result.Data;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("CRMTEST", doctor.CRM);
            Assert.True(doctor.Active);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenValidationFail()
        {
            // Arrange
            var command = new UpdateDoctorCommand() { IdDoctor = 1 };
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor());
            _validatorMock.Setup(v => v.Validate(command))
                .Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("PropertyError", "ErrorMessage") }));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenDoctorNotFound()
        {
            // Arrange
            var command = new UpdateDoctorCommand() { IdDoctor = 1 };
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync((Doctor)null);
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);

        }
    }
}
