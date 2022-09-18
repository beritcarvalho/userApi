using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Interfaces
{
    public interface IEmailService
    {
        Task<CreateSmtpEmail> SendEmailPasswordAsync(Name nameTo, string password, string emailAddress, string typeContent);
        Task<CreateSmtpEmail> SendEmailUsernameAsync(Name nameTo, string username, string emailAddress, string typeContent);
        Task<CreateSmtpEmail> SendEmailNewUserAsync(Name nameTo, string emailAddress, string username, string password);
    }
}
