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

namespace ClinicManager.Application.Queries.Patients.GetById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = await _unitOfWork.Patients
                .GetByIdAsNoTrackingAsync(request.IdPatient);
            if (patient is null) { return Result.NotFound("Paciente não encontrado."); }

            var patientViewModel = _mapper.Map<PatientViewModel>(patient);

            return Result.Success(patientViewModel);
        }
    }
}
