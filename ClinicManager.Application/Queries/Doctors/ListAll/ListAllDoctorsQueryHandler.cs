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
            result = result
                .Skip((request.PageParams.PageNumber - 1) * request.PageParams.PageSize)
                .Take(request.PageParams.PageSize);
            var viewModels = _mapper.Map<List<DoctorViewModel>>(result);

            return new PageList<DoctorViewModel>(viewModels, request.PageParams.PageNumber, request.PageParams.PageSize, viewModels.Count);

        }
    }
}
