using ClinicManager.Application.Commands.MedicalAppointments.Reschedule;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using FluentValidation.Results;
using Moq;

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
    }
}
