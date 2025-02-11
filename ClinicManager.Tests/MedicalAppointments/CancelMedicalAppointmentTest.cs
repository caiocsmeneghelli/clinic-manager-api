using System.Data;
using System.Net;
using ClinicManager.Application.Commands.MedicalAppointments.Cancel;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Moq;

namespace ClinicManager.Tests.MedicalAppointments
{
    public class CancelMedicalAppointmentTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CancelMedicalAppointmentCommandHandler _handler;

        public CancelMedicalAppointmentTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler = new CancelMedicalAppointmentCommandHandler(_unitOfWork.Object);
        }

        [Fact]
        public async Task Handler_ShouldRetrunError_WhenAppointmentNotFound()
        {
            // Arrange
            var command = new CancelMedicalAppointmentCommand(1);
            _unitOfWork.Setup(u => u.MedicalAppointments.GetMedicalAppointmentByIdAsync(command.IdAppointment))
                .ReturnsAsync((MedicalAppointment)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        // Fix
        [Fact]
        public async Task Handler_ShouldReturnSuccess_WhenCancelAppointment(){
            // Arrange
            var command = new CancelMedicalAppointmentCommand(1);
            _unitOfWork.Setup(u => u.MedicalAppointments
                .GetMedicalAppointmentByIdAsync(command.IdAppointment))
                .ReturnsAsync(new MedicalAppointment());
            
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

        }
    }
}