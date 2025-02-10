using ClinicManager.Application.Commands.MedicalAppointments.Reschedule;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Net;

namespace ClinicManager.Tests.MedicalAppointments
{
    public class RescheduleMedicalAppointmentTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IValidator<RescheduleMedicalAppointmentCommand>> _validator;
        private readonly RescheduleMedicalAppointmentCommandHandler _handler;

        public RescheduleMedicalAppointmentTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _validator = new Mock<IValidator<RescheduleMedicalAppointmentCommand>>();
            _handler = new RescheduleMedicalAppointmentCommandHandler(_unitOfWork.Object, _validator.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenValidationFails()
        {
            // Arrange
            var command = new RescheduleMedicalAppointmentCommand();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Property", "Error message.") });
            _validator.Setup(v => v.Validate(command)).Returns(validationResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenMedicalAppointmentNotFound()
        {
            // Arrange
            var command = new RescheduleMedicalAppointmentCommand() { IdAppointment = 1 };
            _validator.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWork.Setup(m => m.MedicalAppointments
            .GetMedicalAppointmentByIdAsync(command.IdAppointment)).ReturnsAsync((MedicalAppointment)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenDoctorHasMedicalAppointmentSameTime()
        {
            // Arrange
            var command = new RescheduleMedicalAppointmentCommand() { IdAppointment = 1, Start = new DateTime(2025, 11, 15, 17, 32, 0), End = new DateTime(2025, 11, 15, 18, 32, 0) };
            var medicalAppointment = new MedicalAppointment(1, 1, 1, "string", new DateTime(), new DateTime());
            _validator.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWork.Setup(u => u.MedicalAppointments.GetMedicalAppointmentByIdAsync(command.IdAppointment))
                .ReturnsAsync(medicalAppointment);
            _unitOfWork.Setup(u => u.MedicalAppointments.GetMedicalAppointmentByDoctorAndDate(medicalAppointment.IdDoctor, command.Start))
                .ReturnsAsync(new MedicalAppointment());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
