using AutoMapper;
using ClinicManager.Application.Helpers;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Doctors.ListAll
{
    public class ListAllDoctorsQueryHandler : IRequestHandler<ListAllDoctorsQuery, PageList<DoctorViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListAllDoctorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageList<DoctorViewModel>> Handle(ListAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Doctors.GetAllAsync();
            var pageViewModels = _mapper.ProjectTo<DoctorViewModel>(result);

            var pagination = PageList<DoctorViewModel>
                .CreatePagination(pageViewModels, request.PageParams.PageNumber, request.PageParams.PageSize);


            return pagination;

        }
    }
}
