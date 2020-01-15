using System.Collections.Generic;
using System.Linq;

namespace Crawler_ItJobs_Portugal.Models.Email
{
    public class EmailOptions
    {
        public string SmtpClient { get; set; }
        public string MailAddress { get; set; }
        public Attachment Attachment { get; set; }
        public Credentials NetworkCredential { get; set; }
        public string Subject { get; set; }
        public int Port { get; set; }
    }

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Attachment
    {
        public string File { get; set; }
        public string MediaType { get; set; }
    }
}