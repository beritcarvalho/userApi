using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Applications.Interfaces;
using UserApi.Domain.Entities;
using UserApi.Domain.Exceptions;
using UserApi.Domain.Interfaces;

namespace UserApi.Applications.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IAccountRepository _AccountRepository;
        private readonly IRoleRepository _RoleRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            IAccountRepository accountRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _UserRepository = userRepository;
            _AccountRepository = accountRepository;
            _RoleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<UserAddViewModel> AddUser(UserInputModel userInput)
        {
            try
            {
                var user = _mapper.Map<User>(userInput);

                var account = await _AccountRepository.GetByIdAsync(user.Acco_Id);

                if (account == null)
                    throw new UserException("ERR-03X01 Cadastrado não encontrado");

                user.Account = account;

                var role = await _RoleRepository.GetByIdAsync(user.Role_Id);
                
                if (role == null)
                    throw new UserException("ERR-03X02 Perfil de usuário não encontrado");

                user.Role = role;

                if (user.Active)
                    user.Active_Date = DateTime.Now;

                user.Create_Date = DateTime.Now;
                user.Last_Update_Date = DateTime.Now;

                await _UserRepository.InsertAsync(user);
                return _mapper.Map<UserAddViewModel>(user);
            }
            catch (DbUpdateException e)
            {
                throw new Exception("ERR-01X01 Não foi possível realizar o cadastro");
            }
            catch (UserException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X02 Falha interna no servidor");
            }
        }

        public async Task<UserViewModel> GetUserByIdWithInclude(int id)
        {
            try
            {
                var user = await _UserRepository.GetUserByIdWithIncludeAsync(id);

                if (user == null)
                    return null;

                return _mapper.Map<UserViewModel>(user);
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }

        public async Task<UserActiveViewModel> ActiveUser(int id)
        {
            var user = await _UserRepository.GetByIdAsync(id);

            if (user == null)
                throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

            user.Active = true;
            user.Active_Date = DateTime.Now;
            user.Last_Update_Date = DateTime.Now;

            return _mapper.Map<UserActiveViewModel>(user); ;
        }

        public async Task<UserInactiveViewModel> InactiveUser(int id)
        {
            var user = await _UserRepository.GetByIdAsync(id);

            if (user == null)
                throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

            user.Active = false;
            user.Inactive_Date = DateTime.Now;
            user.Last_Update_Date = DateTime.Now;

            return _mapper.Map<UserInactiveViewModel>(user); 
        }

        public async Task<ChangePasswordViewModel> ChangePassword(ForgetInputModel input)
        {
            var phone = string.Concat(input.Phone.Ddd + input.Phone.Number);
            var user = await _UserRepository.GetUserForChangePassword(input.Login, input.Cpf.Number, phone);
            var changePassword =  _mapper.Map<ChangePasswordViewModel>(user);
            changePassword.Success = true;
            return changePassword;
        }

        public async Task<ForgetUserViewModel> GetUserName(ForgetInputModel input)
        {
            var phone = string.Concat(input.Phone.Ddd + input.Phone.Number);
            var user = await _UserRepository.GetUserForLoginForget(input.Cpf.Number, phone);
            return _mapper.Map<ForgetUserViewModel>(user);
        }
    }
}
