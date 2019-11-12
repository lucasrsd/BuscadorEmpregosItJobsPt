using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crawler_ItJobs_Portugal.Controllers.V1
{
    [Route ("api/v1/[controller]")]
    [ApiController]
    public class SearchJobsController : ControllerBase
    {
        private readonly ISearchService SearchService;
        private readonly IEmailService EmailService;
        public SearchJobsController (ISearchService searchService, IEmailService emailService)
        {
            this.SearchService = searchService;
            this.EmailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Calcular ()
        {
            var lista = new List<object> ();
            var listaEmails = new List<string> ();
            for (int x = 3; x < 20; x++)
            {
                var result = this.SearchService.ListarVagasUrls (x);

                lista.Add (result);
                foreach (var item in result.Data)
                {
                    this.EmailService.EnviarEmail (item, listaEmails);

                    // ToDo - Arrumar isto
                    // foreach (var dadosEmail in result.Data)
                    // {
                    //     if (dadosEmail.EmailsRelacionados != null && dadosEmail.EmailsRelacionados.Any ())
                    //     {
                    //         listaEmails.Add (dadosEmail.EmailsRelacionados.FirstOrDefault ());
                    //     }
                    // }
                }
            }

            return Ok (lista);
        }
    }
}