using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Newtonsoft.Json.Linq;
namespace Myfashionmarketer.Controllers
{
    public class AssetsController : Controller
    {
        //
        // GET: /Assets/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            User _user = (User)Session["User"];
            return View();
        }

        public ActionResult Account()
        {
            AccountRepository accrepo = new AccountRepository();
            User _user = (User)Session["User"];
            //_user.EmailId = "rakeshjha@globussoft.com";
            List<Account> objaccount = accrepo.getAccountDetails(_user.EmailId);
            ViewBag.accountlist = objaccount;
            return View();
        }

        public ActionResult NewAccount()
        {
            return View("PartialNewAccountAdd");
        }
        /* Developer : Rakesh Jha\
           Desc:Adding a new Account 
           Dated :5/30/2015  */

        public ActionResult addaccount()
        {
            Account account = new Account();

            User user = new User();
            string str = "";
            User _user = (User)Session["User"];
            System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
            string line = "";
            line = sr.ReadToEnd();
            JObject jo = JObject.Parse(line);
            string Company = Server.UrlDecode((string)jo["Company"]);
            string web_url = Server.UrlDecode((string)jo["web_url"]);
            string default_no = Server.UrlDecode((string)jo["default_no"]);
            string billing_email = Server.UrlDecode((string)jo["billing_email"]);
            string Address = Server.UrlDecode((string)jo["Address"]);
            string City = Server.UrlDecode((string)jo["City"]);
            string Postal = Server.UrlDecode((string)jo["Postal"]);

            account.EmailId = _user.EmailId;
            account.Business_name = Company;
            account.Default_no = default_no;
            account.Company_url = web_url;
            account.Address = Address;
            account.City = City;
            account.Zip = Postal;
            account.User_id = _user.Id;
            account.Company_id = Guid.NewGuid();



            //Account _account = (Account)Session["Account"];
            Session["Account"] = account;
            try
            {
                AccountRepository.Add(account);
                //user.Company_id = account.Company_id;
                //user.Id = Guid.NewGuid();
                //user.UserName = _user.UserName;
                //user.EmailId = _user.EmailId;
                //user.Phone = _user.Phone;
                //  UserRepository.Add(user);
                return Content("Success");
            }
            catch
            {
                return Content("Failed");
            }

        }

        /* Developer : Rakesh Jha\
           Desc:Edit the Account 
           Dated : 6/30/2015
      */
        public ActionResult EditAccount()
        {
            User _user = (User)Session["User"];
            if (_user != null)
            {
                string value = Request.QueryString["value"];

                if (value == "Edit")
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                    string line = "";
                    line = sr.ReadToEnd();
                    JObject jo = JObject.Parse(line);
                    string c_id = Server.UrlDecode((string)jo["company_id"]);
                    Guid company_id = Guid.Parse(c_id);
                    AccountRepository objaccrepo = new AccountRepository();
                    Account account = objaccrepo.getuserAccountDetails(company_id);
                    Session["Account"] = account;
                    ViewBag.accountlist = account;
                    return View("EditAccount");
                }
                else if (value == "addEdit")
                {
                    Account _account = (Account)Session["Account"];
                    ViewBag.accountlist = _account;
                    return View("EditAccount");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /* Developer : Rakesh Jha\
         Desc:Update the  user's Existing Account Account
         Dated : 7/30/2015
       */


        public ActionResult UpdateAccount()
        {
            try
            {
                AccountRepository objaccrepo = new AccountRepository();
                System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                string line = "";
                line = sr.ReadToEnd();
                JObject jo = JObject.Parse(line);
                string Company = Server.UrlDecode((string)jo["Company"]);
                string web_url = Server.UrlDecode((string)jo["web_url"]);
                string default_no = Server.UrlDecode((string)jo["default_no"]);
                string billing_email = Server.UrlDecode((string)jo["billing_email"]);
                string Address = Server.UrlDecode((string)jo["Address"]);
                string City = Server.UrlDecode((string)jo["City"]);
                string Postal = Server.UrlDecode((string)jo["Postal"]);
                string account_id = Server.UrlDecode((string)jo["account_id"]);
                Account acc = new Account();
                acc.Address = Address;
                acc.Business_name = Company;
                acc.City = City;
                acc.Zip = Postal;
                acc.Billing_email = billing_email;

                string msg = objaccrepo.updateAccount(account_id, acc);
                return Content(msg);

            }
            catch
            {
                return null;
            }
        }



        /* Developer : Rakesh Jha\
         Desc: All the Employees of the Account
         Dated : 6/30/2015
       */
        public ActionResult Employees()
        {
            try
            {
                Account _account = (Account)Session["Account"];

                UserRepository objuserrepo = new UserRepository();
                List<User> total_emp = objuserrepo.Employees(_account.Company_id);
                ViewBag.lstEmployee = total_emp;
                return View("EmployeePartialView");
            }
            catch
            {
                return null;

            }


        }


        /* Developer : Rakesh Jha\
           Desc: Add a Employee to Account
           Dated : 7/30/2015
         */
        public ActionResult AddEmployee()
        {
            try
            {
                Account _account = (Account)Session["Account"];
                Employee objemp = new Employee();
                User objuser = new User();
                System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                string line = "";
                line = sr.ReadToEnd();
                JObject jo = JObject.Parse(line);
                string First_name = Server.UrlDecode((string)jo["First_name"]);
                string Last_name = Server.UrlDecode((string)jo["Last_name"]);
                string Email = Server.UrlDecode((string)jo["Email"]);
                string Phone = Server.UrlDecode((string)jo["Phone"]);
                objuser.UserName = First_name;
                objuser.EmailId = Email;
                objuser.Id = Guid.NewGuid();
                objuser.Company_id = _account.Company_id;
                objuser.Company = _account.Business_name;
                objuser.Phone = Phone;
                UserRepository.Add(objuser);
                return Content("success");


            }
            catch
            {
                return null;
            }


        }



        /* Developer : Rakesh Jha\
           Desc: User's  Contact's Profile
           Dated : 7/30/2015
         */

        public ActionResult ContactProfile()
        {

            try
            {
                User _user = (User)Session["User"];
                string uid = Request.QueryString["id"];
                Guid id;
                if (uid == "myprofile")
                {
                    id = _user.Id;
                }
                else {
                     id = Guid.Parse(uid);
                }




                UserRepository objuserrepo = new UserRepository();
                User emp = objuserrepo.contact(id);
                ViewBag.empprofile = emp;
                string userview = "normal";
                if (_user.Id == emp.Id)
                {
                    userview = "session_user";

                }
                ViewBag.userview = userview;
                return View();

            }
            catch
            {
                return null;

            }

        }
    }


}