using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Patients.GetAll
{
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientQuery, Result>
    {
        private readonly IUnitOfWork _unitOFWork;

        public GetAllPatientsQueryHandler(IUnitOfWork unitOFWork)
        {
            _unitOFWork = unitOFWork;
        }

        public async Task<Result> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOFWork.Patients.GetAllAsync();
            return Result.Success(list);
        }
    }
}
