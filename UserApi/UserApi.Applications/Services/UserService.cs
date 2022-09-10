using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<UserAddViewModel> AddUser(UserInputModel UserInput)
        {
            try
            {
                var user = _mapper.Map<User>(UserInput);

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
                var User = await _UserRepository.GetUserByIdAsync(id);

                if (User == null)
                    return null;

                return _mapper.Map<UserViewModel>(User);
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }

        public async Task<UserViewModel> UpdateUser(UserInputModel UserInput)
        {
            try
            {
                var User = await _UserRepository.GetByIdAsync(UserInput.Id);

                if (User == null)
                    return null;

                _mapper.Map(UserInput, User);

                User.Last_Update_Date = DateTime.Now;

                await _UserRepository.UpdateAsync(User);
                return _mapper.Map<UserViewModel>(User);
            }
            catch (DbUpdateException e)
            {
                throw new Exception("ERR-01X04 Não foi possível atualizar o cadastro");
            }
            catch
            {
                throw new Exception("ERR-01X05 Falha interna no servidor");
            }
        }

        public async Task<UserViewModel> RemoveUserById(int id)
        {
            try
            {
                var User = await _UserRepository.GetByIdAsync(id);

                if (User == null)
                    return null;

                await _UserRepository.RemoveAsync(User);
                return _mapper.Map<UserViewModel>(User);
            }
            catch(DbUpdateException e)
            {
                throw new Exception("ERR-01X06 Não foi possível realizar o cadastro");
            }
            catch
            {
                throw new Exception("ERR-01X07 Falha interna no servidor");
            }
        }
    }
}
