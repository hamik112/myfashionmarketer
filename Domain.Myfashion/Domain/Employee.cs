using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Myfashion.Domain
{
    public class Employee
    {
        public Guid Employee_id { get; set; }
        public Guid Company_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Phone_no { get; set; }
        public string Address { get; set; }
        public string Country_name { get; set; }
        public string City_name { get; set; }
   
        //public static List<Employee> lstEmployee = new List<Employee>();
    }
}