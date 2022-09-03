using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                await _accountRepository.InsertAsync(account);
                return _mapper.Map<AccountViewModel>(account);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AccountViewModel> DisableAccount(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);

            account.Last_Update_Date = DateTime.Now;

            await _accountRepository.UpdateAsync(account);

            var accountView = new AccountViewModel
            {
                Id = account.Id,
                First_Name = account.First_Name,
                Last_Name = account.Last_Name,
                Create_Date = account.Create_Date
            };
            return accountView;
        }

        public async Task<AccountViewModel> ActivateAccount(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);


            account.Last_Update_Date = DateTime.Now;

            await _accountRepository.UpdateAsync(account);

            var accountView = new AccountViewModel
            {
                Id = account.Id,
                First_Name = account.First_Name,
                Last_Name = account.Last_Name,
                Create_Date = account.Create_Date,
                Last_Update_Date = account.Last_Update_Date,
            };
            return accountView;
        }

        public async Task<AccountViewModel> GetById(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            
            if (account == null)
                return null;

            var accountView = new AccountViewModel
            {
                Id = account.Id,
                First_Name = account.First_Name
            };

            return accountView;
        }
    }
}
