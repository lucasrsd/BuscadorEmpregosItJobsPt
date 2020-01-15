using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Application.Base;
using Crawler_ItJobs_Portugal.Models.Search;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Core.JobSearch
{
    public interface IJobSearchCore
    {
        BaseResponse<SearchJobResult> SearchJobs (SearchJobModel searchJobModel);
    }
}