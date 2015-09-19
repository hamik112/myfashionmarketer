using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;



namespace Myfashionmarketer.Controllers
{
    //[Authorize]
    //[CustomAuthorize]
    public class TwitterManagerController : Controller
    {
        //
        // GET: /TwitterManager/

        ILog logger = LogManager.GetLogger(typeof(TwitterManagerController));

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Twitter()
        {
            var requestToken = (String)Request.QueryString["oauth_token"];
            var requestSecret = (String)Session["requestSecret"];
            var requestVerifier = (String)Request.QueryString["oauth_verifier"];
            Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
           
                try
                {
                    string AddTwitterAccount = string.Empty;
                    Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                    apiobjTwitter.Timeout = 120 * 1000;
                    //AddTwitterAccount = apiobjTwitter.AddTwitterAccount(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"], objUser.Id.ToString(), Session["group"].ToString(), requestToken, requestSecret, requestVerifier);
                    Domain.Myfashion.Domain.TwitterAccount objTwitterAccount = (Domain.Myfashion.Domain.TwitterAccount)new JavaScriptSerializer().Deserialize(apiobjTwitter.AddTwitterAccount(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"], objUser.Id.ToString(), Session["group"].ToString(), requestToken, requestSecret, requestVerifier), typeof(Domain.Myfashion.Domain.TwitterAccount));
                    AddTwitterAccount = objTwitterAccount.TwitterUserId;
                    Session["SocialManagerInfo"] = AddTwitterAccount;

                    //To enable the Tweet Pop up
                    TempData["IsTwitterAccountAdded"] = 1;
                    TempData["TwitterAccount"] = objTwitterAccount;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }

                if (Session["SocialManagerInfo"] == null)
                {
                    return RedirectToAction("SocialMedia", "Home");
                }

                return RedirectToAction("SocialMedia", "Home");
        }
        public ActionResult AuthenticateTwitter(string op)
        {
            logger.Error("Abhay twittermanager");
            try
            {
                try
                {
                    if (op != null)
                    {
                        if (op == "twitterlogin")
                        {
                            Session["twitterlogin"] = op;
                            Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
                            string TwitterUrl = apiobjTwitter.GetTwitterRedirectUrl(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                            string str = TwitterUrl.Split('~')[0].ToString();
                            Session["requestSecret"] = TwitterUrl.Split('~')[1].ToString();
                            //Response.Redirect(TwitterUrl.Split('~')[0].ToString(), true);
                            return Content(TwitterUrl.Split('~')[0].ToString());
                        }
                    }
                    else
                    {
                        Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                        JObject group = null;

                        try
                        {
                            logger.Error("GetGroupDetailsByGroupId before");
                            group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString()));
                            logger.Error("GetGroupDetailsByGroupId after");
                        }
                        catch (Exception ex)
                        {
                            logger.Error("GetGroupDetailsByGroupId Exception");
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }

                      
                        if (Convert.ToString(group["GroupName"]) == ConfigurationManager.AppSettings["DefaultGroupName"].ToString())
                        {
                           
                                Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
                                string TwitterUrl = apiobjTwitter.GetTwitterRedirectUrl(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                                string str = TwitterUrl.Split('~')[0].ToString();
                                Session["requestSecret"] = TwitterUrl.Split('~')[1].ToString();
                                Response.Redirect(TwitterUrl.Split('~')[0].ToString());
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            //return View();
            return RedirectToAction("SocialMedia", "Home");
        }


    }
}
