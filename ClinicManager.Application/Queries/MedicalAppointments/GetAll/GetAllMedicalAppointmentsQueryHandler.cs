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
    public class GetAllMedicalAppointmentsQueryHandler : IRequestHandler<GetAllMedicalAppointmentsQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMedicalAppointmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllMedicalAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.MedicalAppointments.GetAll();
            var resultsViewModel = _mapper.Map<List<MedicalAppointmentViewModel>>(results);
            return Result.Success(resultsViewModel);
        }
    }
}
