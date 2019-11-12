using System.Collections.Generic;
using System.Linq;

namespace Crawler_ItJobs_Portugal.Models.Search
{
    public class JobsModel
    {
        public JobsModel ()
        {
            this.EmailsRelacionados = new List<string> ();
        }

        public string Url { get; set; }
        public string Titulo { get; set; }
        public int Pagina { get; set; }
        public string DataPublicacao { get; set; }
        public string Empresa { get; set; }
        public string Localidade { get; set; }
        public string Referencia { get; set; }
        public IList<string> EmailsRelacionados { get; set; }
        public bool ContemEmail => EmailsRelacionados != null && EmailsRelacionados.Any ();
    }
}