using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Applications.Interfaces;

namespace UserApi.Applications.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public async Task<CreateSmtpEmail> SendEmailNewPasswordAsync(Name nameTo, string password, string email)
        {
            var apiInstance = new TransactionalEmailsApi();
            var from = await PrepararRemetente();
            var to = await PrepararDestinatarios(nameTo, email);

            var conteudoEmail = await PrepararEmailRecoveryNewPassword(nameTo, password, from, to);

            return await Send(apiInstance, conteudoEmail);
        }

        private async Task<SendSmtpEmail> PrepararEmailRecoveryNewPassword(Name nameTo, string password, SendSmtpEmailSender from, List<SendSmtpEmailTo> to)
        { 
            long? templateId;

            var subject = "{{params.subject}}";
            #region ConteudoHtml
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
            <h3>Recuperação de Senha</h3>
            <p><b>{{params.firstName}}</b>, você solicitou redefinir a senha da sua conta Api-BeritDev.</p>
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

            #region Parametros
            var parameters = new JObject();
            //Nome/valor parametro para o texto html content
            parameters.Add("firstName", $"{nameTo.First_Name}");

            //texto/valor parametro do senha
            parameters.Add("password", $"{password}");
            
            //texto/valor parametro do assunto
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

        private async Task<List<SendSmtpEmailTo>> PrepararDestinatarios(Name name, string email)
        {
            //destinatario
            string ToEmail = email;
            string ToName = $"{name.First_Name} {name.Last_Name}";
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);

            //lista de envio
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            return To;
        }

        private async Task<SendSmtpEmailSender> PrepararRemetente()
        {
            //rementente
            string FromName = Options.FromName;
            string FromEmail = Options.FromEmail;
            SendSmtpEmailSender Email = new SendSmtpEmailSender(FromName, FromEmail);
            return Email;
        }

        public async Task<CreateSmtpEmail> Send(TransactionalEmailsApi apiInstance, SendSmtpEmail ConteudoDeEnvio)
        {
            try
            {
                return apiInstance.SendTransacEmail(ConteudoDeEnvio);
            }
            catch(Exception e)
            {
                throw new Exception("Não foi possível enviar o email");
            }
        }
    }
}


