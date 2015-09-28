using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDtaService.classes
{
    static class Globals
    {
        public static Dictionary<string, string> Industry = new Dictionary<string,string>();
        public static Dictionary<string, string> SeniorLevel = new Dictionary<string, string>();
        public static Dictionary<string, string> Groups = new Dictionary<string, string>();
        public static Dictionary<string, string> CompanySize = new Dictionary<string, string>();
        public static Queue<string> DataUrlsQue = new Queue<string>();
        public static List<string> ValidProxyList = new List<string>();
       
    }
}
