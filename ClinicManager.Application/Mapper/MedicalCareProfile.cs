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
    public class MedicalCareProfile : Profile
    {
        public MedicalCareProfile()
        {
            CreateMap<MedicalCare, MedicalCareViewModel>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.PersonDetail.FirstName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.PersonDetail.FirstName))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Title))
                .ForMember(dest => dest.ServiceDescription, opt => opt.MapFrom(src => src.Service.Description))
                .ForMember(dest => dest.ServiceCost, opt => opt.MapFrom(src => src.Service.Cost));
        }
    }
}
