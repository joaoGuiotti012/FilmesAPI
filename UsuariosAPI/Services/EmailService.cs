using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {

        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MimeMessage CreateBodyMail(Mensagem msg)
        {
            var msgMail = new MimeMessage();
            msgMail.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailSettings:From")));
            msgMail.To.AddRange(msg.Destinatario);
            msgMail.Subject = msg.Assunto;
            msgMail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = msg.Conteudo
            };
            return msgMail;
        }

        private void SendMail(MimeMessage mesagemEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(
                        _configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    var from = _configuration.GetValue<string>("EmailSettings:From");
                    var password = _configuration.GetValue<string>("EmailSettings:Password");
                    client.Authenticate(from, password);
                    client.Send(mesagemEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string codeAtivation)
        {
            Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, codeAtivation);
            var mesagemEmail = CreateBodyMail(mensagem);
            SendMail(mesagemEmail);
        }

    }
}
