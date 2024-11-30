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
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            // Mapeamento direto para Address e AddressViewModel
            CreateMap<Address, AddressViewModel>();
            CreateMap<AddressViewModel, Address>();
        }
    }
}
