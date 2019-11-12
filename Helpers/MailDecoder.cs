using System;

namespace Crawler_ItJobs_Portugal.Helpers
{
    public static class MailDecoder
    {
        public static string cfDecodeEmail (string encodedString)
        {
            string email = "";
            int r = Convert.ToInt32 (encodedString.Substring (0, 2), 16), n, i;
            for (n = 2; encodedString.Length - n > 0; n += 2)
            {
                i = Convert.ToInt32 (encodedString.Substring (n, 2), 16) ^ r;
                char character = (char) i;
                email += Convert.ToString (character);
            }

            return email;
        }

    }
}