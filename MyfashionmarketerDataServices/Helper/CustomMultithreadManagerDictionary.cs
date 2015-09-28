using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public static class CustomMultithreadManagerDictionary
    {
        public static Dictionary<string, CustomMultithreadsManager> dictionary_CustomMultithreadsManager = new Dictionary<string, CustomMultithreadsManager>() { { "AllParsers", new CustomMultithreadsManager() }, { "DirectUrlParser", new CustomMultithreadsManager() }, { "Parser", new CustomMultithreadsManager() } };
        //public static Dictionary<string, CustomMultithreadsManager> dictionary_CustomMultithreadsManager = new Dictionary<string, CustomMultithreadsManager>();

        //public static int countinstances_CustomMultithreadsManager = 0;
    }
}
