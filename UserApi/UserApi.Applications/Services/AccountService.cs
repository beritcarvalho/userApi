﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Applications.Interfaces;
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

        public async Task<AccountViewModel> UpdateAccount(AccountInputModel accountInput)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(accountInput.Id);

                if (account == null)
                    return null;

                _mapper.Map(accountInput, account);

                account.Last_Update_Date = DateTime.Now;

                await _accountRepository.UpdateAsync(account);
                return _mapper.Map<AccountViewModel>(account);
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
                var account = await _accountRepository.GetByIdAsync(id);

                if (account == null)
                    return null;

                await _accountRepository.RemoveAsync(account);
                return _mapper.Map<AccountViewModel>(account);
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
