using AutoMapper;
using ClinicManager.Application.Helpers;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Patients.ListAll
{
    public class ListAllPatientsQueryHandler : IRequestHandler<ListAllPatientsQuery, PageList<PatientViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListAllPatientsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageList<PatientViewModel>> Handle(ListAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.Patients.GetAllAsync();
            var queryable = _mapper.ProjectTo<PatientViewModel>(results);

            var pagination = PageList<PatientViewModel>
                .CreatePagination(queryable, request.PageNumber, request.PageSize);
            return pagination;
        }
    }
}
