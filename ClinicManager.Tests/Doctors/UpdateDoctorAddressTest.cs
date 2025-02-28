using ClinicManager.Application.Commands.Doctors.UpdateAddress;
using ClinicManager.Application.Commands.Doctors.UpdatePersonalDetail;
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

namespace ClinicManager.Tests.Doctors
{
    public class UpdateDoctorAddressTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<UpdateDoctorAddressCommand>> _validatorMock;
        private readonly UpdateDoctorAddressCommandHandler _handler;

        public UpdateDoctorAddressTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UpdateDoctorAddressCommand>>();
            _handler = new UpdateDoctorAddressCommandHandler(_validatorMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            // Arrange
            var command = new UpdateDoctorAddressCommand() { IdDoctor = 1, Uf = "ES" };
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor());
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var doctor = (Doctor)result.Data;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("ES", doctor.Address.UF);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenDoctorNotFound()
        {
            // Arrange
            var command = new UpdateDoctorAddressCommand() { IdDoctor = 1, Uf = "ES" };
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync((Doctor)null);
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenValidationFail()
        {
            // Arrange
            var command = new UpdateDoctorAddressCommand() { IdDoctor = 1, Uf = "ES" };
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor());
            _validatorMock.Setup(v => v.Validate(command))
                .Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("PropertyName", "ErroMessage") }));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
