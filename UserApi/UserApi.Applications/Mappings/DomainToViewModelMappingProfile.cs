using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            // Inserindo os dados em account em viewModel
            CreateMap<Account, AccountViewModel>();

            //Insrindo os dados de account em InputModel
            CreateMap<AccountInputModel, Account>()
                .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.Name.First_Name))
                .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.Name.Last_Name))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Number))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Ddd + src.Phone.Number))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.EmailAddress))
                .ReverseMap();

            // Inserindo os dados em account em viewModel
            CreateMap<User, UserViewModel>();

            //Insrindo os dados de account em InputModel
            CreateMap<User, UserInputModel>().ReverseMap();

        }
    }
}
