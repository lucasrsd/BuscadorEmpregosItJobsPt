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
        public SearchJobsController (ISearchService searchService)
        {
            this.SearchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> Calcular ()
        {
            var processamento = DateTime.Now.ToString ("ddMMyyyyHHmmss");
            for (var x = 1; x < 50; x++)
            {
                var result = this.SearchService.ListarVagasUrls (x);
            }
            return Ok ();
        }
    }
}