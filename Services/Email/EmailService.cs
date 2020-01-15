using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Application.Base;
using Crawler_ItJobs_Portugal.Helpers;
using Crawler_ItJobs_Portugal.Models.Email;
using Crawler_ItJobs_Portugal.Models.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailOptions> EmailOptions;

        public EmailService (IOptions<EmailOptions> emailOptions)
        {
            this.EmailOptions = emailOptions;
        }

        public bool EnviarEmail (string targetMail, JobsModel job)
        {
            try
            {
                if (!job.ContemEmail)
                {
                    Console.WriteLine ($"{DateTime.Now} Sem emails encontrados para: {job.Titulo} - Emails: {string.Join(',', job.EmailsRelacionados)}");
                    return false;
                }

                System.Threading.Thread.Sleep (3000); // Pausa de 3s entre envios, dependendo da quantidade x tempo o outlook pode suspender

                Console.WriteLine ($"{DateTime.Now} Enviando email para: {job.Titulo} - Emails: {string.Join(',', job.EmailsRelacionados)}");
                SmtpClient SmtpServer = new SmtpClient (this.EmailOptions.Value.SmtpClient);
                var mail = new MailMessage ();
                mail.From = new MailAddress (this.EmailOptions.Value.MailAddress);
                
                // *** ATENCAO *** - Sugiro utilizar o email mockado para testar e nao enviar testes para os recrutadores
                //mail.To.Add (targetMail);
                mail.To.Add ("seu-email-de-teste@teste.com"); // Mock para testes
                
                mail.Subject = this.EmailOptions.Value.Subject;
                mail.IsBodyHtml = true;
                mail.Attachments.Add (new System.Net.Mail.Attachment (this.EmailOptions.Value.Attachment.File, this.EmailOptions.Value.Attachment.MediaType));
                string htmlBody;
                htmlBody = MontarHtml (job);
                mail.Body = htmlBody;
                SmtpServer.Port = this.EmailOptions.Value.Port;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential (this.EmailOptions.Value.NetworkCredential.Email, this.EmailOptions.Value.NetworkCredential.Password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send (mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine ($"{DateTime.Now} - Falha no envio do email: {ex.ToString()}");
                return false;
            }
        }

        private string MontarHtml (JobsModel job)
        {
            // Montar email conforme criatividade, podendo inserir o nome da vaga, do recrutador, etc etc
            return $@"MENSAGEM TESTE";
        }
    }
}