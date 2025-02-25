using ClinicManager.Application.Commands.Patients.UpdatePersonalDetail;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Tests.Patients
{
    public class UpdatePatientPersonalDetailTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IValidator<UpdatePatientPersonalDetailCommand>> _validatorMock;
        private readonly UpdatePatientPersonalDetailCommandHandler _handler;

        public UpdatePatientPersonalDetailTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UpdatePatientPersonalDetailCommand>>();
            _handler = new UpdatePatientPersonalDetailCommandHandler(_unitOfWork.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            // Arrange
            var command = new UpdatePatientPersonalDetailCommand() { IdPatient = 1, Name = "Thoams", LastName = "Anderson" };

            _unitOfWork.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient());
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var patient = (Patient)result.Data;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Anderson", patient.PersonDetail.LastName);

        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenPatientNotFound()
        {
            // Arrange
            var command = new UpdatePatientPersonalDetailCommand() { IdPatient = 1 };

            _unitOfWork.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync((Patient)null);
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenValidationFails()
        {
            // Arrange
            var command = new UpdatePatientPersonalDetailCommand() { IdPatient = 1 };

            _unitOfWork.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient());
            _validatorMock.Setup(v => v.Validate(command))
                .Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("PropertyName","ErrorMessage") }));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
