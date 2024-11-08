using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Doctors.GetDoctorById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _unitOfWork.Doctors
                .GetByIdAsNoTrackingAsync(request.IdDoctor);
            if(doctor is null) { return Result.NotFound("Médico não encontrado."); }

            return Result.Success(doctor);
        }
    }
}
