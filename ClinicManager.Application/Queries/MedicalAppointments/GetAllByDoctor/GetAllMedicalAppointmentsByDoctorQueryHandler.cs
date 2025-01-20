using AutoMapper;
using ClinicManager.Application.Results;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalAppointments.GetAllByDoctor
{
    public class GetAllMedicalAppointmentsByDoctorQueryHandler : IRequestHandler<GetAllMedicalAppointmentsByDoctorQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMedicalAppointmentsByDoctorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllMedicalAppointmentsByDoctorQuery request, CancellationToken cancellationToken)
        {
            var medicalCares = await _unitOfWork.MedicalAppointments.GetAllByDoctor(request.DoctorId);
            var vmMedicalCare = _mapper.Map<MedicalAppointmentViewModel>(medicalCares);
            return Result.Success(vmMedicalCare);
        }
    }
}
