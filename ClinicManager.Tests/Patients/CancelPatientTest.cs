using ClinicManager.Application.Commands.Patients.Cancel;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Tests.Patients
{
    public class CancelPatientTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CancelPatientTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenPatientNotFound()
        {
            // Arrange
            var command = new CancelPatientCommand(1);
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync((Patient)null);
            var handler = new CancelPatientCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess_WhenCancelPatient()
        {
            // Arrange
            var command = new CancelPatientCommand(1);
            var listMedicalAppointment = new List<MedicalAppointment>() { new MedicalAppointment() };
            var listService = new List<Service>() { new Service() };
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient());
            _unitOfWorkMock.Setup(u => u.MedicalAppointments.GetAllByPatient(command.IdPatient)).ReturnsAsync(listMedicalAppointment);
            _unitOfWorkMock.Setup(u => u.Services.GetAllByPatient(command.IdPatient)).ReturnsAsync(listService);

            var handler = new CancelPatientCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            listService.Should().OnlyContain(item => item.Active == false);
            listMedicalAppointment.Should().OnlyContain(item => item.Active == false);
        }
    }
}
