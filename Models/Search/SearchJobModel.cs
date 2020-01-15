using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Crawler_ItJobs_Portugal.Models.Search
{
    public class SearchJobModel
    {
        [Required]
        public string Tag { get; set; }

        [Required]
        [Range (1, int.MaxValue)]
        public int PageStart { get; set; }

        [Required]
        [Range (1, int.MaxValue)]
        public int PageEnd { get; set; }
    }
}