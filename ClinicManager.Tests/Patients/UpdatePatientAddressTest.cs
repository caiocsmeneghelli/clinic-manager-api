using ClinicManager.Application.Commands.Patients.UpdateAddress;
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
    public class UpdatePatientAddressTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<UpdatePatientAddressCommand>> _validatorMock;
        private readonly UpdatePatientAddressCommandHandler _handler;

        public UpdatePatientAddressTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UpdatePatientAddressCommand>>();
            _handler = new UpdatePatientAddressCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            // Arrange
            var command = new UpdatePatientAddressCommand() { IdPatient = 1, Uf = "ES"};
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient());
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var patient = (Patient)result.Data;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("ES", patient.Address.UF);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenPatientNotFound()
        {
            // Arrange
            var command = new UpdatePatientAddressCommand { IdPatient = 1 };
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync((Patient)null);
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
