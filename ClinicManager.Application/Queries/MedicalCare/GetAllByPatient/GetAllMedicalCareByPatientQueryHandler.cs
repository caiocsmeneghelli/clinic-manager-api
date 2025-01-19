﻿using AutoMapper;
using ClinicManager.Application.Results;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalCare.GetAllByPatient
{
    public class GetAllMedicalCareByPatientQueryHandler : IRequestHandler<GetAllMedicalCareByPatientQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMedicalCareByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllMedicalCareByPatientQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.MedicalCares.GetAllByPatient(request.PatientId);
            var vmMedicalCare = _mapper.Map<MedicalCareViewModel>(results);
            return Result.Success(vmMedicalCare);
        }
    }
}
