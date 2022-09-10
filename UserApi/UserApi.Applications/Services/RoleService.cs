using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Applications.Interfaces;
using UserApi.Domain.Entities;
using UserApi.Domain.Interfaces;

namespace UserApi.Applications.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;

        }

        public async Task<List<RoleViewModel>> GetRoleAll()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();

                if(roles == null)
                    return null;

                var rolesViews = new List<RoleViewModel>();

                foreach(var role in roles)
                    rolesViews.Add(_mapper.Map<RoleViewModel>(roles));

                return rolesViews;
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

        public async Task<RoleViewModel> GetRoleById(int id)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(id);

                if (role == null)
                    return null;

                return _mapper.Map<RoleViewModel>(role);
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
    }
}
