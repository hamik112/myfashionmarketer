using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Myfashion.Domain;
using System.Web.Script.Serialization;

namespace Myfashionmarketer.Controllers
{
    public class BillingController : Controller
    {
        //
        // GET: /Billing/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpgradePlan(string AccountType)
        {
            string amount="";
            string returnurl = "";
            User objUser = (User)Session["User"];
            Helper.Payment objpayment=new Helper.Payment ();
            Session["AccountType"] = AccountType;
            if (AccountType == "Personal")
            {
                amount = "5";
            }
            else if (AccountType == "Personal")
            {
                amount = "10";
            }
            else if (AccountType == "Personal")
            {
                amount = "25";
            }
            string UserName = objUser.UserName;
            string EmailId = objUser.EmailId;
            string UpgradePlanSuccessURL = ConfigurationManager.AppSettings["UpgradeAccountSuccessURL"];
            string UpgradePlanFailedURL = ConfigurationManager.AppSettings["UpgradeAccountFailedURL"];
            string UpgradePlanpaypalemail = ConfigurationManager.AppSettings["UpgradeAccountpaypalemail"];
            string userId = objUser.Id.ToString();
            returnurl = objpayment.PayWithPayPal(amount, AccountType, UserName, "", EmailId, "USD", UpgradePlanpaypalemail, UpgradePlanSuccessURL,
                                            UpgradePlanFailedURL, UpgradePlanSuccessURL, "", "", userId);
           return Content(returnurl);
        }



        public ActionResult UpgradeAccountSuccessful()
        {
            Domain.Myfashion.Domain.User _User = new Domain.Myfashion.Domain.User();
            User objUser = (User)Session["User"];
            string accountType = (string)Session["AccountType"];
            Api.User.User objuser = new Api.User.User();
            string data = objuser.UpdatePaymentSatus(objUser.Id.ToString(), accountType);
            string user = objuser.getUsersById(objUser.Id.ToString());
            _User = (User)(new JavaScriptSerializer().Deserialize(user, typeof(User)));
            Session["User"] = _User;
            Session["Paid_User"] = "Paid";
            return RedirectToAction("Index", "Home");
        }





	}
}