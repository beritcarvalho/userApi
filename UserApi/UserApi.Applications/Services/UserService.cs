using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using System.Security.Principal;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ValueObjects;
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
        private readonly IEmailService _EmailService;
        public UserService(IUserRepository userRepository,
            IAccountRepository accountRepository,
            IRoleRepository roleRepository,
            IMapper mapper,
            IEmailService emailService)
        {
            _UserRepository = userRepository;
            _AccountRepository = accountRepository;
            _RoleRepository = roleRepository;
            _mapper = mapper;
            _EmailService = emailService;
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
                    user.Active_Date = DateTime.UtcNow;

                user.Create_Date = DateTime.UtcNow;
                user.Last_Update_Date = DateTime.UtcNow;
                var password = user.Password_Hash;
                user.Password_Hash = PasswordHasher.Hash(password);

                await _UserRepository.InsertAsync(user);

                var name = new Name
                {
                    First_Name = user.Account.First_Name,
                    Last_Name = user.Account.Last_Name
                };

                await _EmailService.SendEmailNewUserAsync(name, user.Account.Email, user.Login, password);
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
            catch (EmailException e)
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
                    throw new UserException("ERR-03X01 Cadastrado não encontrado");

                return _mapper.Map<UserViewModel>(user);
            }
            catch (UserException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
        public async Task<UserActiveViewModel> ActiveUser(int id)
        {
            try
            {
                var user = await _UserRepository.GetByIdAsync(id);

                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                user.Active = true;
                user.Active_Date = DateTime.UtcNow;
                user.Last_Update_Date = DateTime.UtcNow;

                await _UserRepository.UpdateAsync(user);

                return _mapper.Map<UserActiveViewModel>(user);
            }
            catch (UserException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
        public async Task<UserInactiveViewModel> InactiveUser(int id)
        {
            try
            {
                var user = await _UserRepository.GetByIdAsync(id);

                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                user.Active = false;
                user.Inactive_Date = DateTime.UtcNow;
                user.Last_Update_Date = DateTime.UtcNow;

                await _UserRepository.UpdateAsync(user);

                return _mapper.Map<UserInactiveViewModel>(user);
            }
            catch (UserException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
        public async Task<ChangeRoleViewModel> ChangeRole(int idUser,int IdRole)
        {
            try
            {
                var user = await _UserRepository.GetByIdAsync(idUser);

                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                var role = await _RoleRepository.GetByIdAsync(IdRole);
                
                if (role == null)
                    throw new UserException("ERR-03X02 Perfil de usuário não encontrado");

                user.Role = role;
                user.Last_Update_Date = DateTime.UtcNow;

                await _UserRepository.UpdateAsync(user);

                return _mapper.Map<ChangeRoleViewModel>(user);
            }
            catch (UserException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
    }
}
