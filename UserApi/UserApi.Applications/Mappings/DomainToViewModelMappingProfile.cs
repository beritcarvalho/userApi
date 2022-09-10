using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ValueObjects;
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

            //Mapamento AccountInputModel para Account
            CreateMap<AccountInputModel, Account>()
                .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.Name.First_Name))
                .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.Name.Last_Name))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Number))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Ddd + src.Phone.Number))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.EmailAddress))
                .ReverseMap();

            
            //Mapamento User para UserAddView
            CreateMap<User, UserAddViewModel>()
                .ForPath(userView => userView.Name.First_Name, options => options.MapFrom(user => user.Account.First_Name))
                .ForPath(userView => userView.Name.Last_Name, options => options.MapFrom(user => user.Account.Last_Name))
                .ForPath(userView => userView.Role, options => options.MapFrom(user => user.Role.Name));

            //Mapamento User para UserView
            CreateMap<User, UserViewModel>()           
                .ForPath(userView => userView.Name.First_Name, options => options.MapFrom(user => user.Account.First_Name))
                .ForPath(userView => userView.Name.Last_Name, options => options.MapFrom(user => user.Account.Last_Name))
                .ForPath(userView => userView.Role, options => options.MapFrom(user => user.Role.Name));

            //Insrindo os dados de account em InputModel
            CreateMap<UserInputModel, User>()
                .ForMember(user => user.Acco_Id, options => options.MapFrom(userInput => userInput.Account_Id))
                .ReverseMap();

            CreateMap<Role, RoleViewModel>();

            CreateMap<List<Role>, RoleViewModel>();


        }
    }
}
