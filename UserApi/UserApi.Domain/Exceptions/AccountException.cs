using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Domain.Entities;

namespace UserApi.Domain.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException(string message) : base(message)
        {
        }

        public AccountException()
        {
            throw new AccountException("AccountException: Cadastro não encontrado");
        }
    }
}
