using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Application.Base;
using Crawler_ItJobs_Portugal.Helpers;
using Crawler_ItJobs_Portugal.Models.Search;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Services
{
    public class SearchService : ISearchService
    {
        public SearchService (string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

        private string BaseUrl { get; set; }
        private ILogger<SearchService> Logger { get; set; }

        public BaseResponse<IList<JobsModel>> ListarVagasUrls (int page)
        {
            var result = new List<JobsModel> ();
            var uri = new Uri (this.BaseUrl + $"/emprego/.net?&sort=date&page={page}");

            var client = new RestClient (uri);

            client.CookieContainer = new System.Net.CookieContainer ();

            var request = new RestRequest (Method.GET);

            IRestResponse response = client.Execute (request);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument ();
            htmlDoc.LoadHtml (response.Content);

            var nodes = htmlDoc.DocumentNode.SelectNodes (@"//div[@class='list-title']");

            if (nodes == null)
                return new BaseResponse<IList<JobsModel>> (false, $"Nenhum registro encontrado na pagina {page}");

            var jobs = htmlDoc.DocumentNode.SelectNodes (
                    @" //div[@class='list-title']/a")
                .Select (li => li);

            foreach (var job in jobs)
            {
                var url = this.BaseUrl + job.Attributes["href"].Value;
                var titulo = job.Attributes["title"].Value;

                result.Add (new JobsModel ()
                {
                    Url = url,
                        Titulo = titulo,
                        Pagina = page
                });
                
            }

            if (result != null)
                result.ForEach (x => IncluirEmailsVaga (x));

            return new BaseResponse<IList<JobsModel>> (result);
        }

        private JobsModel IncluirEmailsVaga (JobsModel job)
        {
            var rnd = new Random ().Next (5, 20);
            System.Threading.Thread.Sleep (rnd * 100);

            var uri = new Uri (job.Url);

            var client = new RestClient (uri);

            client.CookieContainer = new System.Net.CookieContainer ();

            var request = new RestRequest (Method.GET);

            IRestResponse response = client.Execute (request);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument ();
            htmlDoc.LoadHtml (response.Content);

            var jobsMails = htmlDoc.DocumentNode.SelectNodes ("//span[contains(@class, '__cf_email__')]");

            if (jobsMails == null)
            {
                Console.WriteLine ($"{DateTime.Now} Pagina: {job.Pagina}  Titulo: {job.Titulo}  Url: {job.Url}  SEM EMAILS ENCONTRADOS!{Environment.NewLine}");
                return job;
            }

            foreach (var mail in jobsMails)
            {
                var valueMailEncoded = mail.Attributes["data-cfemail"];
                if (valueMailEncoded != null)
                {
                    var email = Helpers.MailDecoder.cfDecodeEmail (valueMailEncoded.Value);
                    var conteudo = $"{DateTime.Now} Pagina: {job.Pagina}  Titulo: {job.Titulo}  Url: {job.Url}  Email: {email}{Environment.NewLine}";
                    Console.WriteLine (conteudo);
                    System.IO.File.AppendAllText ($@"C:\JobsCrawler\Teste.txt", conteudo);
                    job.EmailsRelacionados.Add(email);
                }
            }

            return job;
        }
    }
}