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

namespace ClinicManager.Application.Queries.Patients.GetAll
{
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientQuery, Result>
    {
        private readonly IUnitOfWork _unitOFWork;
        private readonly IMapper _mapper;

        public GetAllPatientsQueryHandler(IUnitOfWork unitOFWork, IMapper mapper)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            var patients = await _unitOFWork.Patients.GetAllAsync();
            var patientsViewModel = _mapper.Map<List<PatientViewModel>>(patients);
            return Result.Success(patientsViewModel);
        }
    }
}
