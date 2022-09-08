using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.InputModels;
using UserApi.Applications.ViewModels;
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
            CreateMap<Account, AccountInputModel>().ReverseMap();

            // Inserindo os dados em account em viewModel
            CreateMap<User, UserViewModel>();

            //Insrindo os dados de account em InputModel
            CreateMap<User, UserInputModel>().ReverseMap();

        }
    }
}
