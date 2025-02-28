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
    public class UpdateDoctorPersonalDetailTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<UpdateDoctorPersonalDetailCommand>> _validatorMock;
        private readonly UpdateDoctorPersonalDetailCommandHandler _handler;

        public UpdateDoctorPersonalDetailTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UpdateDoctorPersonalDetailCommand>>();
            _handler = new UpdateDoctorPersonalDetailCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            // Arrange
            var command = new UpdateDoctorPersonalDetailCommand() { IdDoctor = 1, Name = "Name Test", LastName = "LastName Test"};
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor());
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var doctor = (Doctor)result.Data;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Name Test", doctor.PersonDetail.FirstName);
            Assert.Equal("LastName Test", doctor.PersonDetail.LastName);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenDoctorNotFound()
        {
            // Arrange
            var command = new UpdateDoctorPersonalDetailCommand() { IdDoctor = 1, Name = "Name Test", LastName = "LastName Test" };
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
            var command = new UpdateDoctorPersonalDetailCommand() { IdDoctor = 1, Name = "Name Test", LastName = "LastName Test" };
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor());
            _validatorMock.Setup(v => v.Validate(command))
                .Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("PropertyName", "ErroMessage")}));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
