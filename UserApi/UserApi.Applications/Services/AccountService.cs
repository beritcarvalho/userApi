using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Applications.InputModels;
using UserApi.Applications.Interfaces;
using UserApi.Applications.ViewModels;
using UserApi.Domain.Entities;
using UserApi.Domain.Interfaces;

namespace UserApi.Applications.Services
{   
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;

        }

        public async Task<AccountViewModel> AddAccount(AccountInputModel accountInput)
        {
            try
            {
                var account = _mapper.Map<Account>(accountInput);

                account.Create_Date = DateTime.Now;
                account.Last_Update_Date = DateTime.Now;

                await _accountRepository.InsertAsync(account);
                return _mapper.Map<AccountViewModel>(account);
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
                var account = await _accountRepository.GetByIdAsync(id);

                if (account == null)
                    return null;

                return _mapper.Map<AccountViewModel>(account);
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
    }
}
