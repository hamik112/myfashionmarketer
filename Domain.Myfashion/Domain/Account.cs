using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Myfashion.Domain
{
    public class Account
    {
        public Guid Company_id { get; set; }
        public Guid User_id { get; set; }
        public string EmailId { get; set; }
        public string Business_name { get; set; }

        public string Tags { get; set; }
        public string Indusrties { get; set; }
        public string Block_status { get; set; }
        public string Default_no { get; set; }
        public string Address { get; set; }
        public int Country_name { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Company_url { get; set; }


        public string Spam_url { get; set; }
        public Guid Api_key { get; set; }
        public string Blocked_url { get; set; }
        public string Google_analytics_acc_id { get; set; }
        public string Page_domain { get; set; }
        public string Adwords_custId { get; set; }
        public string Link_replace_domain { get; set; }
        public string Google_captcha_key { get; set; }
        public string Google_captcha_secret { get; set; }

        public string Billing_email { get; set; }
        public static List<Account> lstAccount = new List<Account>();

    }
}