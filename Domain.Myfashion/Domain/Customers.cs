using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Myfashion.Domain
{
    public class Customers
    {
        public Customers() { }
        public virtual int Customerid { get; set; }
        public virtual int? Resellerid { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual int Zipcode { get; set; }
        public virtual int Allowedusercount { get; set; }
        public virtual int Activeusercount { get; set; }
        public virtual int Allowedcampaignscount { get; set; }
        public virtual int Activeserpcampaignscount { get; set; }
        public virtual int Activevideocampaigncount { get; set; }
        public virtual int Allowedkeywordcount { get; set; }
        public virtual int Activekeywordcount { get; set; }
        public virtual int Activevideokeywordcount { get; set; }
        public virtual string Timezoneid { get; set; }
        public virtual string Timezonevalue { get; set; }
        public virtual string Alertemailid { get; set; }
        public virtual string Alertnotificationcount { get; set; }
        public virtual string Companyname { get; set; }
        public virtual string Companyurllink { get; set; }
        public virtual string Companylogolink { get; set; }
        public virtual string Companydescription { get; set; }
    }
}
