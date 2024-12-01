using AutoMapper;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Mapper
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorViewModel>()
               .ForMember(dest => dest.PersonDetail, opt => opt.MapFrom(src => src.PersonDetail))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(dest => dest.CRM, opt => opt.MapFrom(src => src.CRM))
               .ForMember(dest => dest.MedicalSpecialty, opt => opt.MapFrom(src => src.MedicalSpecialty));
        }
    }
}
