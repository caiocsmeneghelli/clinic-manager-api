using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Doctors.GetAll
{
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllDoctorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Doctors.GetAllAsync();
            return Result.Success(result);
        }
    }
}
