using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Applications.Interfaces;
using UserApi.Domain.Entities;
using UserApi.Domain.Interfaces;

namespace UserApi.Applications.Services
{    
    
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _AccountRepository;
        private readonly IMapper _Mapper;
        
        public AccountService(IAccountRepository accountRepository,
            IMapper mapper,
            IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _AccountRepository = accountRepository;
            _Mapper = mapper;
            EmailProperties = optionsAccessor.Value;
        }
        
        public AuthMessageSenderOptions EmailProperties { get; }

        public async Task<AccountViewModel> AddAccount(AccountInputModel accountInput)
        {
            try
            {
                var account = _Mapper.Map<Account>(accountInput);

                account.Create_Date = DateTime.UtcNow;
                account.Last_Update_Date = DateTime.UtcNow;

                await _AccountRepository.InsertAsync(account);
                return _Mapper.Map<AccountViewModel>(account);
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

        public async Task<AccountViewModel> GetById(int id)
        {
            try
            {
                var account = await _AccountRepository.GetByIdAsync(id);

                if (account == null)
                    return null;

                return _Mapper.Map<AccountViewModel>(account);
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }

        public async Task<AccountViewModel> UpdateAccount(AccountInputModel accountInput)
        {
            try
            {
                var account = await _AccountRepository.GetByIdAsync(accountInput.Id);

                if (account == null)
                    return null;

                _Mapper.Map(accountInput, account);

                account.Last_Update_Date = DateTime.UtcNow;

                await _AccountRepository.UpdateAsync(account);
                return _Mapper.Map<AccountViewModel>(account);
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

        public async Task<AccountViewModel> RemoveById(int id)
        {
            try
            {
                var account = await _AccountRepository.GetByIdAsync(id);

                if (account == null)
                    return null;

                await _AccountRepository.RemoveAsync(account);
                return _Mapper.Map<AccountViewModel>(account);
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
