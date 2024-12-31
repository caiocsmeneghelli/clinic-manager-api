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

namespace ClinicManager.Application.Queries.MedicalCare.GetAll
{
    public class GetAllMedicalCareQueryHandler : IRequestHandler<GetAllMedicalCareQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMedicalCareQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllMedicalCareQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.MedicalCares.GetAll();
            var resultsViewModel = _mapper.Map<List<MedicalCareViewModel>>(results);
            return Result.Success(resultsViewModel);
        }
    }
}
