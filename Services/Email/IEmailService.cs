using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Application.Base;
using Crawler_ItJobs_Portugal.Models.Search;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Services.Email
{
    public interface IEmailService
    {
        bool EnviarEmail (string targetMail, JobsModel job);
    }
}