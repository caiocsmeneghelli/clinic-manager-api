using ClinicManager.Application.Commands.Doctors.Cancel;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using FluentAssertions;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Tests.Doctors
{
    public class CancelDoctorTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CancelDoctorTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess_WhenCancelDoctor()
        {
            // Arrange
            var command = new CancelDoctorCommand(1);
            var listMedicalAppointment = new List<MedicalAppointment>() { new MedicalAppointment() };
            var listService = new List<Service>() { new Service() };

            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor());
            _unitOfWorkMock.Setup(u => u.MedicalAppointments.GetAllByDoctor(command.IdDoctor)).ReturnsAsync(listMedicalAppointment);
            _unitOfWorkMock.Setup(u => u.Services.GetAllByIdDoctor(command.IdDoctor)).ReturnsAsync(listService);

            var handler = new CancelDoctorCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.All(listService, item => Assert.False(item.Active));
            Assert.All(listMedicalAppointment, item => Assert.False(item.Active));

            // FluentAssertions;
            listService.Should().OnlyContain(item => item.Active == false);
            listMedicalAppointment.Should().OnlyContain(item => item.Active == false);
        }

        [Fact]
        public async Task Handler_ShouldReturnError_WhenDoctorNotFound()
        {
            // Arrange
            var command = new CancelDoctorCommand(1);
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync((Doctor)null);

            var handler = new CancelDoctorCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
