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
            #region MapeamentoAccountService
            CreateMap<Account, AccountViewModel>();
            
            CreateMap<AccountInputModel, Account>()
                .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.Name.First_Name))
                .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.Name.Last_Name))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Number))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Ddd + src.Phone.Number))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.EmailAddress))
                .ReverseMap();
            #endregion

            #region MapeamentoUserService
            CreateMap<UserInputModel, User>()
                .ForMember(dest => dest.Acco_Id, opt => opt.MapFrom(src => src.Account_Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login.Username))
                .ForMember(dest => dest.Password_Hash, opt => opt.MapFrom(src => src.PropPassword.Password));

            CreateMap<User, UserAddViewModel >()
                .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.Account.First_Name))
                .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.Account.Last_Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.Account.First_Name))
                .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.Account.Last_Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));


            CreateMap<User, UserActiveViewModel>();

            CreateMap<User, UserInactiveViewModel>();
            #endregion

            #region MapeamentoRecoveryService
            CreateMap<User, ChangePasswordViewModel>();
            CreateMap<User, RecoveryPasswordViewModel>();
            CreateMap<User, RecoveryUsernameViewModel>();
            CreateMap<User, ChangeUserNameViewModel>();
            #endregion


            #region MapeamentoRoleService
            CreateMap<Role, RoleViewModel>();

            CreateMap<List<Role>, RoleViewModel>();
            #endregion


        }
    }
}
