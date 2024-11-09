using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Patients.GetPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = await _unitOfWork.Patients
                .GetByIdAsNoTrackingAsync(request.IdPatient);
            if (patient is null) { return Result.NotFound("Paciente não encontrado."); }

            return Result.Success(patient);
        }
    }
}
