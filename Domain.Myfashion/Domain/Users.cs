using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Myfashion.Domain
{

    public class Users
    {
        public virtual int Userid { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual int? Resellerid { get; set; }
        public virtual string Loginid { get; set; }
        public virtual string Password { get; set; }
        public virtual int? Usertype { get; set; }
        public virtual int Active { get; set; }
        public virtual DateTime? Addeddate { get; set; }
        public virtual string Token { get; set; }
    }
}
