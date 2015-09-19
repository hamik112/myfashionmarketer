using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Myfashion.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid Company_id { get; set; }
        public string UserName { get; set; }

        public string AccountType { get; set; }
        public string ProfileUrl { get; set; }
        public string EmailId { get; set; }

        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserStatus { get; set; }
        public string Password { get; set; }
        public string TimeZone { get; set; }
        public string PaymentStatus { get; set; }
        public string ActivationStatus { get; set; }

        public string UserType { get; set; }

        public string ChangePasswordKey { get; set; }
        public int IsKeyUsed { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string PuId { get; set; }

        public static List<User> lstUser = new List<User>();
    }
}