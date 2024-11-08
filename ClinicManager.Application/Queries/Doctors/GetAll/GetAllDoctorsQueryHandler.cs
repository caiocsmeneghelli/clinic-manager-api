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
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, List<Doctor>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllDoctorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Doctor>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Doctors.GetAllAsync();
            return result;
        }
    }
}
