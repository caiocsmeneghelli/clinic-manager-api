using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalCare.GetAll
{
    public class GetAllMedicalCareQueryHandler : IRequestHandler<GetAllMedicalCareQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMedicalCareQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetAllMedicalCareQuery request, CancellationToken cancellationToken)
        {
            // To-do
            return Result.Success();
        }
    }
}
