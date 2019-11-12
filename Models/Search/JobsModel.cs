using System.Collections.Generic;

namespace Crawler_ItJobs_Portugal.Models.Search
{
    public class JobsModel
    {
        public JobsModel (string url, string titulo, int pagina)
        {
            this.Url = url;
            this.Titulo = titulo;
            this.Pagina = pagina;
            this.EmailsRelacionados = new List<string>();
        }

        public string Url { get; set; }
        public string Titulo { get; set; }
        public int Pagina { get; set; }
        public IList<string> EmailsRelacionados { get; set; }
    }
}