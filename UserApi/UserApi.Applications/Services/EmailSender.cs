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

        public async Task<CreateSmtpEmail> SendEmailAsync(Name nameTo, string password, string email)
        {
            return await Execute(nameTo, password, email);
        }

        public async Task<CreateSmtpEmail> Execute(Name nameTo, string password, string email)
        {    

            var apiInstance = new TransactionalEmailsApi();
            SendSmtpEmailSender Email = PrepararRemetente();

            List<SendSmtpEmailTo> To = PrepararDestinatário(nameTo, email);
            string HtmlContent, Subject;
            JObject Headers, Params;
            long? TemplateId;
            PreparacaoConteudoEmail(nameTo, password, out HtmlContent, out Headers, out TemplateId, out Params, out Subject);

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, null, Subject, null, null, Headers, TemplateId, Params, null, null);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ErroEmail");
            }
        }

        private static void PreparacaoConteudoEmail(Name nameTo, string password, out string HtmlContent, out JObject Headers, out long? TemplateId, out JObject Params, out string Subject)
        {
            HtmlContent = @"
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
        font-family: 'Open Sans','Roboto','Segoe UI','sans-serif';
    }


    #breve_resumo {
        width: 100%;
        height: 335px;        
        font-family: Helvetica, Arial, 'Lato';
        color: #241e20;
        text-align: center;
        padding-top: 80px;
    }

        #breve_resumo h3 {
            font-size: 40px;
            font-weight: bold;
            letter-spacing: 1px;
            margin-bottom: 15px;
        }

        #breve_resumo p {
            width: 800px;
            margin-left: auto;
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
            <p><b>{{params.firstName}}</b><br/>Você solicitou redefinir a senha da sua conta Api-BeritDev. Estamos enviando a sua nova senha.
            <br/><br />Sua nova senha é:</p>
            <p class=""pass"">{{params.password}}</p>
            <p>Atenciosamente,<br />Equipe do Berit Carvalho</p>
        </div>
    </main>
</body>
</html>";
            Headers = new JObject();
            Headers.Add("Some-Custom-Name", "unique-id-1234");
            TemplateId = null;
            Params = new JObject();

            //Nome/valor parametro para o texto html content
            Params.Add("firstName", $"{nameTo.First_Name}");

            //texto/valor parametro do senha
            Params.Add("password", $"{password}");

            Subject = "{{params.subject}}";
            //texto/valor parametro do assunto
            Params.Add("subject", "API - BERIT Recuperação de senha");

            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", Params);
        }

        private static List<SendSmtpEmailTo> PrepararDestinatário(Name name, string email)
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

        private static SendSmtpEmailSender PrepararRemetente()
        {

            //rementente
            string SenderName = "Beri Dev";
            string SenderEmail = "noreply@beridev.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            return Email;
        }
    }
}

