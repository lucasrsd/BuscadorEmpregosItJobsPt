using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Crawler_ItJobs_Portugal.Models.Search
{
    public class SearchJobResult
    {
        public int QtdVagas { get; set; }
        public int QtdVagasComEmail { get; set; }
        public int QtdVagasSemEmail { get; set; }
        public int QtdEmailsEnviados { get; set; }
        public int QtdEmailsErros { get; set; }
    }
}