using GlobusLinkedinLib.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Myfashionmarketer.Helper
{
    public class AddlinkedinCompanyPage
    {
        public string PageId { get; set; }
        public string PageName { get; set; }
        public oAuthLinkedIn _Oauth { get; set; }
    }
}