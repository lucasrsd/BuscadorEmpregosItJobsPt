using System;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;

namespace Crawler_ItJobs_Portugal.Helpers
{
    public static class HtmlHelpers
    {
        static CultureInfo configPtBr = new CultureInfo ("pt-BR");
        static CultureInfo configEnUS = new CultureInfo ("en-US");
        public static DateTime HorarioBR ()
        {
            return TimeZoneInfo.ConvertTime (DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById ("E. South America Standard Time"));
        }

        public static decimal GetOnlyCurrency (string entrada)
        {
            var result = entrada.Where (x => Char.IsDigit (x) || x.Equals ('.') || x.Equals (',')).ToList ();
            var numbers = new String (result.ToArray ());

            numbers = numbers.Replace (".", "");
            numbers = numbers.Replace (",", ".");

            decimal resultado = 0;

            decimal.TryParse (numbers, NumberStyles.Currency, configEnUS, out resultado);

            return resultado;
        }

        public static HtmlNodeCollection ProcessarGridNodes (this HtmlDocument doc, string xpath)
        {
            return doc.DocumentNode.SelectNodes (xpath);
        }

        public static HtmlNode ProcessarGridSingleNode (this HtmlDocument doc, string xpath)
        {
            return doc.DocumentNode.SelectSingleNode (xpath);
        }

        public static string GetValue (this HtmlDocument doc, string xpath)
        {
            var res = doc.DocumentNode.SelectSingleNode (xpath);
            return res.GetAttributeValue ("value", "");
        }

        public static string GetText (this HtmlDocument doc, string xpath)
        {
            var res = doc.DocumentNode.SelectSingleNode (xpath);
            if (res == null) return null;
            return res.InnerText;
        }

        public static string FormataString (string input)
        {
            if (input == null) return null;
            if (input == "") return "";
            return input.Replace ("\n", "").Replace ("\t", "").Trim ();
        }
    }
}