using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawler_ItJobs_Portugal.Application.Base;
using Crawler_ItJobs_Portugal.Models.Search;
using Crawler_ItJobs_Portugal.Services.Email;
using Crawler_ItJobs_Portugal.Services.Search;

namespace Crawler_ItJobs_Portugal.Core.JobSearch
{
    public class JobSearchCore : IJobSearchCore
    {
        private readonly ISearchService SearchService;
        private readonly IEmailService EmailService;
        public JobSearchCore (ISearchService searchService, IEmailService emailService)
        {
            this.SearchService = searchService;
            this.EmailService = emailService;
        }

        private BaseResponse<bool> Valid (SearchJobModel model)
        {
            if (model.PageStart > model.PageEnd)
                return new BaseResponse<bool> (false, "Pagina de inicio maior que pagina final");

            if (string.IsNullOrEmpty (model.Tag))
                return new BaseResponse<bool> (false, "Tag vazia");

            return new BaseResponse<bool> (true);
        }

        public BaseResponse<SearchJobResult> SearchJobs (SearchJobModel searchJobModel)
        {
            var modelValid = Valid (searchJobModel);

            if (!modelValid.Data)
                return new BaseResponse<SearchJobResult> (false, modelValid.ErrorMessage);

            var jobList = new List<JobsModel> ();
            var successMailsList = new List<bool> ();
            
            for (int page = searchJobModel.PageStart; page <= searchJobModel.PageEnd; page++)
            {
                var result = this.SearchService.BuscarVagasPagina (searchJobModel.Tag, page);
                if (result.Ok)
                {
                    jobList.AddRange (result.Data);
                }
            }

            var emailsList = new List<string> ();

            foreach (var item in jobList)
            {
                if (!item.ContemEmail) continue;

                foreach (var email in item.EmailsRelacionados)
                {
                    if (!emailsList.Contains (email))
                    {
                        emailsList.Add (email);
                        var resultMail = this.EmailService.EnviarEmail (email, item);
                        successMailsList.Add (resultMail);
                    }
                }
            }

            var coreResult = new SearchJobResult ()
            {
                QtdVagas = jobList.Count (),
                QtdVagasComEmail = jobList.Where (x => x.ContemEmail).Count (),
                QtdVagasSemEmail = jobList.Where (x => !x.ContemEmail).Count (),
                QtdEmailsEnviados = successMailsList.Where (x => x).Count (),
                QtdEmailsErros = successMailsList.Where (x => !x).Count ()
            };

            return new BaseResponse<SearchJobResult> (coreResult);
        }
    }
}