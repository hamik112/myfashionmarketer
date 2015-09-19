using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Api.Myfashionmarketer.Models;
using Domain.Myfashion.Domain;
using Myfashionmarketer.Helper;
using log4net;
using System.Web.Script.Serialization;

namespace Myfashionmarketer.Controllers
{
    public class IndexController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(IndexController));
        //
        // GET: /Index/
        public ActionResult Index()
        {
            User _user = (User)Session["User"];
            if (_user != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Features()
        {
            User _user = (User)Session["User"];
            if (_user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Pricing()
        {
            User _user = (User)Session["User"];
            if (_user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            User _user = (User)Session["User"];
            if (_user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult SignUp()
        {
            User _user = (User)Session["User"];
            return View(_user);
           
        }

        public ActionResult Contactus()
        {
            User _user = (User)Session["User"];
            if (_user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }

        public ActionResult Register()
        {
            Domain.Myfashion.Domain.User user = new Domain.Myfashion.Domain.User();
            Api.User.User objApiUser = new Api.User.User();

            Domain.Myfashion.Domain.User _user = (Domain.Myfashion.Domain.User)Session["User"];
            string str = "";

            System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
            string line = "";
            line = sr.ReadToEnd();
            JObject jo = JObject.Parse(line);
            user.PaymentStatus = "unpaid";
            user.AccountType = Server.UrlDecode((string)jo["package"]);
            user.CreateDate = DateTime.Now;
            user.ExpiryDate = DateTime.Now.AddMonths(1);
            user.Id = Guid.NewGuid();
            string FirstName = Server.UrlDecode((string)jo["firstname"]);
            string LastName = Server.UrlDecode((string)jo["lastname"]);
            string profile = Server.UrlDecode((string)jo["package"]);
            string Company = Server.UrlDecode((string)jo["Company"]);
            user.UserName = Server.UrlDecode((string)jo["firstname"]) + " " + Server.UrlDecode((string)jo["lastname"]);
            user.EmailId = Server.UrlDecode((string)jo["email"]);
            user.Phone = Server.UrlDecode((string)jo["Phone"]);
            user.Password =Server.UrlDecode((string)jo["password"]);
            user.Company = Server.UrlDecode((string)jo["Company"]);
            //account.User_id = user.Id;
            //account.Company_id = Guid.NewGuid();
            //account.Business_name = Company;
            string res_Registration = string.Empty;
            res_Registration = objApiUser.Register(user.EmailId, user.Password, user.AccountType, user.UserName, user.Company, "0");
            if (res_Registration=="Email Already Exists")
            {
                str = "Email Already Exists";   
            }
            else if (res_Registration == "Something Went Wrong")
            {
                str = "Something Went Wrong";
            }
            else {
                Api.User.User obj = new Api.User.User();
                user = (User)(new JavaScriptSerializer().Deserialize(obj.Login(user.EmailId, user.Password), typeof(User)));
                Session["User"] = user;
                str = "Success";
            }
            return Content(str);
        }


        public ActionResult SignIn()
        {
            Session.Clear();
            Session.RemoveAll();
            User _user = (User)Session["User"];
             string str="";
             string returnmsg = "";
             try
             {
                 logger.Error("Avinash Login");
                 User user = new Domain.Myfashion.Domain.User();
                 string uname = Request.QueryString["username"].ToString();
                 string pass = Request.QueryString["password"].ToString();
                 Api.User.User obj = new Api.User.User();
                 string logindata = obj.Login(uname, pass);
                 str = logindata.Replace("\"", string.Empty).Trim();
                 if (str != "Not Exist" && !str.Equals("Email Not Exist"))
                 {
                     user = (User)(new JavaScriptSerializer().Deserialize(logindata, typeof(User)));
                     Api.Team.Team ApiobjTeam = new Api.Team.Team();
                     List<Domain.Myfashion.Domain.Groups> lstgroup = (List<Domain.Myfashion.Domain.Groups>)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjTeam.GetTeamByUserId(user.Id.ToString()), typeof(List<Domain.Myfashion.Domain.Groups>)));
                     foreach (Groups item in lstgroup)
                     {
                        System.Web.HttpContext.Current.Session["group"] = item.Id;
                     }
                     if (user == null)
                     {
                         returnmsg = "Invalid Email or Password";
                     }
                     else
                     {
                         Session["User"] = user;
                         returnmsg = "user";
                     }
                     return Content(returnmsg);
                     
                 }
                 else if (str.Equals("Email Not Exist"))
                 {
                     returnmsg = "Sorry, Socioboard doesn't recognize that username.";
                     return Content(returnmsg);

                 }
                 return Content(returnmsg);
             }
             catch (Exception ex)
             {
                 logger.Error("UserLogin >>" + ex.Message);
                 logger.Error("UserLogin >>" + ex.StackTrace);
                 return Content(str);

             }

        }
    }
}