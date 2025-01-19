using ClinicManager.Application.Results;
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

        public GetAllMedicalCareByDoctorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetAllMedicalCareByDoctorQuery request, CancellationToken cancellationToken)
        {
            var medicalCares = await _unitOfWork.MedicalCares.Get

        }
    }
}
