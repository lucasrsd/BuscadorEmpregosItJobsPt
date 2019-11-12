using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Application.Base;
using Crawler_ItJobs_Portugal.Helpers;
using Crawler_ItJobs_Portugal.Models.Search;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Services
{
    public class EmailService : IEmailService
    {
        public void EnviarEmail (JobsModel job)
        {
            try
            {
                if (!job.ContemEmail)
                {
                    Console.WriteLine ($"{DateTime.Now} Sem emails encontrados para: {job.Titulo} - Emails: {string.Join(',', job.EmailsRelacionados)}");
                    return;
                }
                System.Threading.Thread.Sleep (3000);

                Console.WriteLine ($"{DateTime.Now} Enviando email para: {job.Titulo} - Emails: {string.Join(',', job.EmailsRelacionados)}");
                SmtpClient SmtpServer = new SmtpClient ("smtp.live.com");
                var mail = new MailMessage ();
                mail.From = new MailAddress ("EMAIL_ORIGEM");
                mail.To.Add (job.EmailsRelacionados.FirstOrDefault ());
                mail.Subject = $"ASSUNTO";
                mail.IsBodyHtml = true;
                mail.Attachments.Add (new Attachment (@"CV_PDF", "application/pdf"));
                string htmlBody;
                htmlBody = MontarHtml (job);
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential ("USUARIO", "SENHA");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send (mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine ($"{DateTime.Now} - Falha no envio do email: {ex.ToString()}");
            }
        }

        private string MontarHtml (JobsModel job)
        {
            return $@"MENSAGEM";
        }
    }
}