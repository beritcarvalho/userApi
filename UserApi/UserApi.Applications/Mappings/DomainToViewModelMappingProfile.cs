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
            // Inserindo os dados em person em viewModel
            CreateMap<Person, PersonViewModel>();

            //Insrindo os dados de person em InputModel
            CreateMap<Person, PersonInputModel>().ReverseMap();

        }
    }
}
