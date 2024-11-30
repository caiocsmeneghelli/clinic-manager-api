using AutoMapper;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Mapper
{
    public class PersonDetailProfile: Profile
    {
        public PersonDetailProfile()
        {
            // Mapeamento direto para PersonDetail e PersonDetailViewModel
            CreateMap<PersonDetail, PersonDetailViewModel>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<PersonDetailViewModel, PersonDetail>();
        }
    }
}
