using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Interfaces
{
    public interface IEmailSender
    {
        Task<CreateSmtpEmail> SendEmailNewPasswordAsync(Name nameTo, string password, string email);
    }
}
