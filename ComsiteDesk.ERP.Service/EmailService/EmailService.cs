using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;

namespace ComsiteDesk.ERP.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string to, string subject, string html, string from = "helpdesk@comsite.com.ve")
        {
            try
            {
                from = string.IsNullOrEmpty(_configuration["smtp:SmtpUser"]) ? from : _configuration["smtp:SmtpUser"];

                // create message
                var email = new MimeMessage();
                var address = MailboxAddress.Parse(from);
                address.Name = "Comsite HelpDesk Website";
                email.From.Add(address);
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Headers.Add("X-Mailer-Machine", Environment.MachineName);
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(_configuration["smtp:SmtpHost"], int.Parse(_configuration["smtp:SmtpPort"]), SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(_configuration["smtp:SmtpUser"], _configuration["smtp:SmtpPass"]);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }            
        }
    }
}
