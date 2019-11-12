using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Application.Base;
using Crawler_ItJobs_Portugal.Models.Search;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Services
{
    public interface ISearchService
    {
        BaseResponse<IList<JobsModel>> ListarVagasUrls (int page);
    }
}