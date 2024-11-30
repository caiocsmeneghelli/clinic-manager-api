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
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientViewModel>()
                .ForMember(dest => dest.PersonDetail, opt => opt.MapFrom(src => src.PersonDetail))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserLogin));

        }
    }
}
