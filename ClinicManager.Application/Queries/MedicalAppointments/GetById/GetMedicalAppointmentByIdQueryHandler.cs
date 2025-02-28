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

namespace ClinicManager.Application.Queries.MedicalAppointments.GetById
{
    public class GetMedicalAppointmentByIdQueryHandler : IRequestHandler<GetMedicalAppointmentByIdQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicalAppointmentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetMedicalAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var medicalCare = await _unitOfWork.MedicalAppointments.GetMedicalAppointmentByIdAsync(request.IdMedicalCare);
            if (medicalCare == null) { return Result.NotFound("Atendimento não encontraado."); }

            var medicalCareViewModel = _mapper.Map<MedicalAppointmentViewModel>(medicalCare);
            return Result.Success(medicalCareViewModel);
        }
    }
}
