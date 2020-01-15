using System;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Core.JobSearch;
using Crawler_ItJobs_Portugal.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace Crawler_ItJobs_Portugal.Controllers.V1
{
    [Route ("api/v1/[controller]")]
    [ApiController]
    public class SearchJobsController : ControllerBase
    {
        private readonly IJobSearchCore JobSearchCore;
        public SearchJobsController (IJobSearchCore jobSearchCore)
        {
            this.JobSearchCore = jobSearchCore;
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] SearchJobModel searchJobModel)
        {
            Console.WriteLine ($"{DateTime.Now}  ** INICIADO ** -> Tag: {searchJobModel.Tag}, Pagina inicial: {searchJobModel.PageStart}, Pagina Final: {searchJobModel.PageEnd}");
            var coreResult = this.JobSearchCore.SearchJobs (searchJobModel);

            Console.WriteLine ($"{DateTime.Now}  ** FINALIZADO ** -> Tag: {searchJobModel.Tag}, Pagina inicial: {searchJobModel.PageStart}, Pagina Final: {searchJobModel.PageEnd}");

            if (coreResult.Ok)
                return Ok (coreResult);
            else
                return BadRequest (coreResult);
        }
    }
}