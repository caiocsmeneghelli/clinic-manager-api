using ClinicManager.Application.Commands.Patients.Update;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Net;

namespace ClinicManager.Tests.Patients
{
    public class UpdatePatientTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<UpdatePatientCommand>> _validatorMock;
        private readonly UpdatePatientCommandHandler _handler;

        public UpdatePatientTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UpdatePatientCommand>>();
            _handler = new UpdatePatientCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            // Arrange
            var command = new UpdatePatientCommand();
            command.IdPatient = 1;
            command.Height = 1.7m;
            command.Weight = 65m;

            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);


            // Assert
            var patient = (Patient)result.Data;
            Assert.True(result.IsSuccess);
            Assert.Equal(65m, patient.Weight);
            Assert.Equal(1.7m, patient.Height);
        }

        [Fact]
        public async Task Handler_ShouldReturnNotFound_WhenPatientNotFound()
        {
            // Arrange
            var command = new UpdatePatientCommand() { IdPatient = 1 };
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync((Patient)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Contains("Paciente não encontrado.", result.Messages);
        }

        [Fact]
        public async Task Handler_ShouldReturnBadRequest_WhenValidationFailed()
        {
            // Arrange
            var command = new UpdatePatientCommand() { IdPatient = 1 };
            _validatorMock.Setup(v => v.Validate(command))
                .Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Property", "Error message") }));
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
