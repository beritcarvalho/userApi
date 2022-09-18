using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Applications.Interfaces;
using UserApi.Domain.Exceptions;

namespace UserApi.Applications.Services
{
    public class EmailService : IEmailService
    {
        public EmailService(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; }

        public async Task<CreateSmtpEmail> SendEmailPasswordAsync(Name nameTo, string password, string emailAddress, string typeContent)
        {
            var apiInstance = new TransactionalEmailsApi();
            var from = await PrepareSender();
            var to = await PrepareRecipients(nameTo, emailAddress);

            var emailContent = await PrepareContentNewPasswordEmail(nameTo, password, from, to, typeContent);

            return await Send(apiInstance, emailContent);
        }

        public async Task<CreateSmtpEmail> SendEmailUsernameAsync(Name nameTo, string username, string emailAddress, string typeContent)
        {
            var apiInstance = new TransactionalEmailsApi();
            var from = await PrepareSender();
            var to = await PrepareRecipients(nameTo, emailAddress);

            var emailContent = await PrepareContentUserEmail(nameTo, username, from, to, typeContent);

            return await Send(apiInstance, emailContent);
        }

        public async Task<CreateSmtpEmail> SendEmailNewUserAsync(Name nameTo, string emailAddress, string username, string password)
        {
            var apiInstance = new TransactionalEmailsApi();
            var from = await PrepareSender();
            var to = await PrepareRecipients(nameTo, emailAddress);

            var emailContent = await PrepareContentNewUserEmail(nameTo, from, to, username, password);

            return await Send(apiInstance, emailContent);
        }



        private async Task<List<SendSmtpEmailTo>> PrepareRecipients(Name name, string emailAddress)
        {
            //destinatario
            string ToEmail = emailAddress;
            string ToName = $"{name.First_Name} {name.Last_Name}";
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);

            //lista de envio
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            return To;
        }

        private async Task<SendSmtpEmailSender> PrepareSender()
        {
            string FromName = Options.FromName;
            string FromEmail = Options.FromEmail;
            SendSmtpEmailSender Sender = new SendSmtpEmailSender(FromName, FromEmail);
            return Sender;
        }

        public async Task<CreateSmtpEmail> Send(TransactionalEmailsApi apiInstance, SendSmtpEmail emailContent)
        {
            try
            {
                return apiInstance.SendTransacEmail(emailContent);
            }
            catch
            {
                throw new EmailException("Não foi possível enviar o email de recuperação de senha");
            }
        }

        private async Task<SendSmtpEmail> PrepareContentNewPasswordEmail(Name nameTo, string password, SendSmtpEmailSender from, List<SendSmtpEmailTo> to, string typeContent)
        {
            long? templateId;

            var subject = "{{params.subject}}";
            #region Html Email Body
            var htmlContent = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">    
    <link href=""style.css"" rel=""stylesheet"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=Open+Sans&family=Roboto&display=swap"" rel=""stylesheet"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=Lato&display=swap"" rel=""stylesheet"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <script src=""https://kit.fontawesome.com/a076d05399.js""></script>
</head>
<style>
    * {
        margin: 0;
        padding: 0;
        text-decoration: none;
        box-sizing: border-box;
    }
    body {
        font-family: 'Open Sans', 'Roboto', 'Segoe UI', 'sans-serif';
    }
    #breve_resumo {
        font-family: Helvetica, Arial, 'Lato';
        color: #241e20;
        text-align: left;
        padding-top: 10px;
    }
    #breve_resumo h3 {
        font-weight: bold;
        letter-spacing: 1px;
        margin-bottom: 15px;
    }
    #breve_resumo p {
        width: 800px;
        margin-left: 1%;
        margin-right: auto;
        margin-bottom: 40px;
        letter-spacing: 0.5px;
        line-height: 28px;
    }
    #breve_resumo .pass {
        background-color: #ad2164;
        color: #d6d6d6;
        text-transform: uppercase;
        border: none;
        border-radius: 25px;
        width: 200px;
        height: 50px;
        text-align-last: center;
        padding-top: 0.70em;
    }

    #breve_resumo .pass:hover {
        color: #b8909d;
        transition: 0.5s;
    }
    #breve_resumo .pass:active {
        color: #241e20;
        transition: none;
        border: solid #241e20;
    }
</style>

<body>
    <main>        
        <div id=""breve_resumo"">
            <h3>{{params.title}}</h3>
            <p><b>{{params.firstName}}</b>, você solicitou {{params.options}} a senha da sua conta Api-BeritDev.</p>
            <p>Estamos enviando a sua nova senha.</p>
            <p><br/><br/>Sua nova senha é:</p>
            <p class=""pass"">{{params.password}}</p>
            <p>Atenciosamente,
            <br />Equipe do Berit Carvalho</p>
        </div>
    </main>
</body>
</html>";
            #endregion;

            var headers = new JObject();
            headers.Add("Some-Custom-Name", "unique-id-1234");
            templateId = null;

            #region Parameters
            var parameters = new JObject();


            parameters.Add("firstName", $"{nameTo.First_Name}");
            parameters.Add("options", $"{typeContent}");
            parameters.Add("title", $"Recuperação de Senha");
            parameters.Add("password", $"{password}");
            parameters.Add("subject", "API - BERIT Recuperação de senha");

            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", parameters);
            #endregion            

            var sendSmtpEmail = new SendSmtpEmail
            {
                Sender = from,
                To = to,
                Bcc = null,
                Cc = null,
                HtmlContent = htmlContent,
                TextContent = null,
                Subject = subject,
                ReplyTo = null,
                Attachment = null,
                Headers = headers,
                TemplateId = templateId,
                Params = parameters,
                MessageVersions = null,
                Tags = null
            };

            return sendSmtpEmail;
        }

        private async Task<SendSmtpEmail> PrepareContentUserEmail(Name nameTo, string login, SendSmtpEmailSender from, List<SendSmtpEmailTo> to, string typeContent)
        {
            long? templateId;

            var subject = "{{params.subject}}";
            #region Html Email Body
            var htmlContent = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">    
    <link href=""style.css"" rel=""stylesheet"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=Open+Sans&family=Roboto&display=swap"" rel=""stylesheet"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=Lato&display=swap"" rel=""stylesheet"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <script src=""https://kit.fontawesome.com/a076d05399.js""></script>
</head>
<style>
    * {
        margin: 0;
        padding: 0;
        text-decoration: none;
        box-sizing: border-box;
    }
    body {
        font-family: 'Open Sans', 'Roboto', 'Segoe UI', 'sans-serif';
    }
    #breve_resumo {
        font-family: Helvetica, Arial, 'Lato';
        color: #241e20;
        text-align: left;
        padding-top: 10px;
    }
    #breve_resumo h3 {
        font-weight: bold;
        letter-spacing: 1px;
        margin-bottom: 15px;
    }
    #breve_resumo p {
        width: 800px;
        margin-left: 1%;
        margin-right: auto;
        margin-bottom: 40px;
        letter-spacing: 0.5px;
        line-height: 28px;
    }
    #breve_resumo .pass {
        background-color: #ad2164;
        color: #d6d6d6;
        text-transform: uppercase;
        border: none;
        border-radius: 25px;
        width: 200px;
        height: 50px;
        text-align-last: center;
        padding-top: 0.70em;
    }

    #breve_resumo .pass:hover {
        color: #b8909d;
        transition: 0.5s;
    }
    #breve_resumo .pass:active {
        color: #241e20;
        transition: none;
        border: solid #241e20;
    }
</style>

<body>
    <main>        
        <div id=""breve_resumo"">
            <h3>{{params.title}}</h3>
            <p><b>{{params.firstName}}</b>, você solicitou {{params.options}} o login da sua conta Api-BeritDev.</p>
            <p>Estamos enviando seu login.</p>
            <p><br/><br/>Seu login agora é:</p>
            <p class=""pass"">{{params.login}}</p>
            <p>Atenciosamente,
            <br />Equipe do Berit Carvalho</p>
        </div>
    </main>
</body>
</html>";
            #endregion;

            var headers = new JObject();
            headers.Add("Some-Custom-Name", "unique-id-1234");
            templateId = null;

            #region Parameters
            var parameters = new JObject();


            parameters.Add("firstName", $"{nameTo.First_Name}");
            parameters.Add("options", $"{typeContent}");
            parameters.Add("title", $"Recuperação de Login");
            parameters.Add("login", $"{login}");
            parameters.Add("subject", "API - BERIT Recuperação de Login");

            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", parameters);
            #endregion            

            var sendSmtpEmail = new SendSmtpEmail
            {
                Sender = from,
                To = to,
                Bcc = null,
                Cc = null,
                HtmlContent = htmlContent,
                TextContent = null,
                Subject = subject,
                ReplyTo = null,
                Attachment = null,
                Headers = headers,
                TemplateId = templateId,
                Params = parameters,
                MessageVersions = null,
                Tags = null
            };

            return sendSmtpEmail;
        }

        private async Task<SendSmtpEmail> PrepareContentNewUserEmail(Name nameTo, SendSmtpEmailSender from, List<SendSmtpEmailTo> to, string login, string password)
        {
            long? templateId;

            var subject = "{{params.subject}}";
            #region Html Email Body
            var htmlContent = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">    
    <link href=""style.css"" rel=""stylesheet"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=Open+Sans&family=Roboto&display=swap"" rel=""stylesheet"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=Lato&display=swap"" rel=""stylesheet"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <script src=""https://kit.fontawesome.com/a076d05399.js""></script>
</head>
<style>
    * {
        margin: 0;
        padding: 0;
        text-decoration: none;
        box-sizing: border-box;
    }
    body {
        font-family: 'Open Sans', 'Roboto', 'Segoe UI', 'sans-serif';
    }
    #breve_resumo {
        font-family: Helvetica, Arial, 'Lato';
        color: #241e20;
        text-align: left;
        padding-top: 10px;
    }
    #breve_resumo h3 {
        font-weight: bold;
        letter-spacing: 1px;
        margin-bottom: 15px;
    }
    #breve_resumo p {
        width: 800px;
        margin-left: 1%;
        margin-right: auto;
        margin-bottom: 40px;
        letter-spacing: 0.5px;
        line-height: 28px;
    }
    #breve_resumo .pass {
        background-color: #ad2164;
        color: #d6d6d6;
        text-transform: uppercase;
        border: none;
        border-radius: 25px;
        width: 200px;
        height: 50px;
        text-align-last: center;
        padding-top: 0.70em;
    }

    #breve_resumo .pass:hover {
        color: #b8909d;
        transition: 0.5s;
    }
    #breve_resumo .pass:active {
        color: #241e20;
        transition: none;
        border: solid #241e20;
    }
</style>

<body>
    <main>        
        <div id=""breve_resumo"">
            <h3>{{params.title}}</h3>
            <p><b>{{params.firstName}}</b>, você agora é um novo usuário;</p>
            <p>Estamos enviando seu login.</p>
            <p><br/><br/>Seu login é:</p>
            <p class=""pass"">{{params.login}}</p>
            <p><br/><br/>Sua senha é:</p>
            <p class=""pass"">{{params.password}}</p>
            <p>Atenciosamente,
            <br />Equipe do Berit Carvalho</p>
        </div>
    </main>
</body>
</html>";
            #endregion;

            var headers = new JObject();
            headers.Add("Some-Custom-Name", "unique-id-1234");
            templateId = null;

            #region Parameters
            var parameters = new JObject();


            parameters.Add("firstName", $"{nameTo.First_Name}");
            parameters.Add("password", $"{password}");
            parameters.Add("title", $"Bem Vindo");
            parameters.Add("login", $"{login}");
            parameters.Add("subject", "API - BERIT 'Bem Vindo'");

            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", parameters);
            #endregion            

            var sendSmtpEmail = new SendSmtpEmail
            {
                Sender = from,
                To = to,
                Bcc = null,
                Cc = null,
                HtmlContent = htmlContent,
                TextContent = null,
                Subject = subject,
                ReplyTo = null,
                Attachment = null,
                Headers = headers,
                TemplateId = templateId,
                Params = parameters,
                MessageVersions = null,
                Tags = null
            };

            return sendSmtpEmail;
        }
    }
}


