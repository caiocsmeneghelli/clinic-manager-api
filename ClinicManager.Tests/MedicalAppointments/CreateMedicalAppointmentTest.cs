using Moq;
using FluentValidation;
using FluentValidation.Results;
using ClinicManager.Application.Commands.MedicalAppointments.Create;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;


namespace ClinicManager.Tests.MedicalAppointments
{
    public class CreateMedicalAppointmentCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<CreateMedicalAppointmentCommand>> _validatorMock;
        private readonly CreateMedicalAppointmentCommandHandler _handler;

        public CreateMedicalAppointmentCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<CreateMedicalAppointmentCommand>>();
            _handler = new CreateMedicalAppointmentCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenValidationFails()
        {
            var command = new CreateMedicalAppointmentCommand();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Property", "Error message") });
            _validatorMock.Setup(v => v.Validate(command)).Returns(validationResult);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error message", result.Messages);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenPatientOrDoctorNotFound()
        {
            var command = new CreateMedicalAppointmentCommand { IdPatient = 1, IdDoctor = 2 };
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(1)).ReturnsAsync((Patient)null);
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(2)).ReturnsAsync((Doctor)null);
            _unitOfWorkMock.Setup(u => u.MedicalAppointments.GetMedicalAppointmentByDoctorAndDate(command.IdDoctor, command.Start))
                .ReturnsAsync((MedicalAppointment)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Contains("Paciente não encontrado.", result.Messages);
            Assert.Contains("Médico não encontrado.", result.Messages);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenMedicalAppointmentIsCreated()
        {
            Patient patient = new Patient { Id = 1 };
            Doctor doctor = new Doctor { Id = 1 };

            var command = new CreateMedicalAppointmentCommand { IdPatient = 1, IdDoctor = 2, Service = "Consulta", Description = "Descrição", Cost = 100, Duration = 30, Start = DateTime.Now, HealthPlan = "Plano A" };
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(1)).ReturnsAsync(patient);
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(2)).ReturnsAsync(doctor);
            _unitOfWorkMock.Setup(u => u.Services.CreateAsync(It.IsAny<Service>())).ReturnsAsync(1);
            _unitOfWorkMock.Setup(u => u.MedicalAppointments.CreateAsync(It.IsAny<MedicalAppointment>())).ReturnsAsync(1);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
            _unitOfWorkMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            var command = new CreateMedicalAppointmentCommand { IdDoctor = 1, IdPatient = 1, Start = new DateTime() };
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient { Id = 1 });
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor { Id = 1 });
            _unitOfWorkMock.Setup(u => u.MedicalAppointments.GetMedicalAppointmentByDoctorAndDate(command.IdDoctor, command.Start))
                .ReturnsAsync((MedicalAppointment)null);
            _unitOfWorkMock.Setup(u => u.Services.CreateAsync(It.IsAny<Service>())).ThrowsAsync(new Exception("Erro"));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Contains("Falha ao salvar atendimento médico.", result.Messages);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenMedicalAppointmentExists()
        {
            var command = new CreateMedicalAppointmentCommand { IdDoctor = 1, IdPatient = 2 };
            _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());
            _unitOfWorkMock.Setup(u => u.Patients.GetByIdAsync(command.IdPatient)).ReturnsAsync(new Patient { Id = 1 });
            _unitOfWorkMock.Setup(u => u.Doctors.GetByIdAsync(command.IdDoctor)).ReturnsAsync(new Doctor { Id = 1 });
            _unitOfWorkMock.Setup(u => u.MedicalAppointments.GetMedicalAppointmentByDoctorAndDate(command.IdDoctor, command.Start))
                .ReturnsAsync(new MedicalAppointment { Id = 1 });

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}