using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Domain.Entities;

namespace UserApi.Domain.Exceptions
{
    public class EmailException : Exception
    {
        public EmailException(string message) : base(message)
        {
        }
    }
}
