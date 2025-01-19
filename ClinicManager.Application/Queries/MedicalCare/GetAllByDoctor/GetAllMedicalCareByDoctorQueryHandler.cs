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

namespace ClinicManager.Application.Queries.MedicalCare.GetAllByDoctor
{
    public class GetAllMedicalCareByDoctorQueryHandler : IRequestHandler<GetAllMedicalCareByDoctorQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMedicalCareByDoctorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllMedicalCareByDoctorQuery request, CancellationToken cancellationToken)
        {
            var medicalCares = await _unitOfWork.MedicalCares.GetAllByDoctor(request.DoctorId);
            var vmMedicalCare = _mapper.Map<MedicalCareViewModel>(medicalCares);
            return Result.Success(vmMedicalCare);
        }
    }
}
