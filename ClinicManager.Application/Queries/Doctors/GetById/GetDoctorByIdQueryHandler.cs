using AutoMapper;
using ClinicManager.Application.Results;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Doctors.GetById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _unitOfWork.Doctors
                .GetByIdAsNoTrackingAsync(request.IdDoctor);
            if(doctor is null) { return Result.NotFound("Médico não encontrado."); }

            var doctorViewModel = _mapper.Map<DoctorViewModel>(doctor);
            return Result.Success(doctorViewModel);
        }
    }
}
