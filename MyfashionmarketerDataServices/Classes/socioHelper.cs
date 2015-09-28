using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDtaService.classes
{
    class socioHelper
    {
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public static List<string> GetHrefFromString(string pageSrcHtml)
        {
            Chilkat.HtmlUtil obj = new Chilkat.HtmlUtil();

            Chilkat.StringArray dataImage = obj.GetHyperlinkedUrls(pageSrcHtml);

            List<string> list = new List<string>();

            for (int i = 0; i < dataImage.Length; i++)
            {
                string hreflink = dataImage.GetString(i);
                list.Add(hreflink);

            }
            return list;
        }
    }
}
