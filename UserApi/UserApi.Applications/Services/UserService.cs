using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Applications.InputModels;
using UserApi.Applications.Interfaces;
using UserApi.Applications.ViewModels;
using UserApi.Domain.Entities;
using UserApi.Domain.Interfaces;

namespace UserApi.Applications.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;

        }

        public async Task<UserViewModel> AddUser(UserInputModel UserInput)
        {
            try
            {
                var User = _mapper.Map<User>(UserInput);

                User.Create_Date = DateTime.Now;
                User.Last_Update_Date = DateTime.Now;

                await _UserRepository.InsertAsync(User);
                return _mapper.Map<UserViewModel>(User);
            }
            catch (DbUpdateException e)
            {
                throw new Exception("ERR-01X01 Não foi possível realizar o cadastro");
            }
            catch
            {
                throw new Exception("ERR-01X02 Falha interna no servidor");
            }
        }

        public async Task<UserViewModel> GetUserById(int id)
        {
            try
            {
                var User = await _UserRepository.GetByIdAsync(id);

                if (User == null)
                    return null;

                return _mapper.Map<UserViewModel>(User);
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }

        public async Task<UserViewModel> UpdateUser(UpdateUserInputModel UserInput)
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
