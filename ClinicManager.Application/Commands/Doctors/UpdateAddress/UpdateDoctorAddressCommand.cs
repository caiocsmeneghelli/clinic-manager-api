using ClinicManager.Application.Results;
using MediatR;

namespace ClinicManager.Application.Commands.Doctors.UpdateAddress
{
    public class UpdateDoctorAddressCommand : IRequest<Result>
    {
        public  int IdDoctor { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Uf { get; set; }
        public string Country { get; set; }
    }
}
