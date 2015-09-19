using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Facebook;
using System.Net;
using System.Web.Security;
using System.Text;
using System.Text.RegularExpressions;
using Myfashionmarketer.App_Start;
using Myfashionmarketer.Helper;

namespace Myfashionmarketer.Controllers
{

    public class FacebookManagerController : Controller
    {
        ILog logger = LogManager.GetLogger(typeof(FacebookManagerController));
        //
        // GET: /FacebookManager/

        //[CustomAuthorize]
        public ActionResult Index()
        {

            return View();
        }
       
        public ActionResult Facebook(string code)
        {
            string status="";
            if (Session["fblogin"] != null)
            {

                if ((string)Session["fblogin"] == "fblogin")
                {
                    Session["fblogin"] = null;
                    if (String.IsNullOrEmpty(code))
                    {
                        return RedirectToAction("Index", "Index");
                    }
                    Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                    Domain.Myfashion.Domain.User checkuserexist = (Domain.Myfashion.Domain.User)Session["User"];
                    string facebookcode = code;
                    Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                    Api.User.User ApiobjUser = new Api.User.User();
                    string fbloginreturn = apiobjFacebook.FacebookLogin(code);
                    string[] arrfbloginreturn = Regex.Split(fbloginreturn,"_#_");

                    objUser = (Domain.Myfashion.Domain.User)(new JavaScriptSerializer().Deserialize(arrfbloginreturn[0], typeof(Domain.Myfashion.Domain.User)));
                    Session["AccesstokenFblogin"] = arrfbloginreturn[1];
                    Session["fblogin"] = "fblogin";

                    try
                    {
                        Response.Write("Facebook Returned email : " + objUser.EmailId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.StackTrace);
                        logger.Error(ex.Message);
                    }

                    try
                    {
                        checkuserexist = (Domain.Myfashion.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(objUser.EmailId.ToString()), typeof(Domain.Myfashion.Domain.User)));
                        string pid = checkuserexist.PuId;
                        string puid = pid.Substring(36, pid.Length - 36);
                        SortedDictionary<string, string> strdic = new SortedDictionary<string, string>();
                        strdic.Add("username", checkuserexist.EmailId);
                        strdic.Add("password", puid);
                        string data = CustomHttpWebRequest.HttpWebRequest("GET", "myfashion/loginService.action", strdic);
                        data = data.Replace("func({", "{");
                        data = data.Replace("});", "}");
                        JObject jo = JObject.Parse(data);
                        status = Server.UrlDecode((string)jo["code"]);
                    }
                    catch (Exception e)
                    {
                        checkuserexist = null;
                    }
                    if (checkuserexist != null)
                    {
                        Session["User"] = checkuserexist;
                        int daysremaining = 0;

                        daysremaining = (checkuserexist.ExpiryDate.Date - DateTime.Now.Date).Days;
                        if (daysremaining > 0)
                        {
                            #region Count Used Accounts
                            try
                            {
                                Session["Paid_User"] = "Paid";
                                Api.SocialProfile.SocialProfile apiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                                //Session["ProfileCount"] = Convert.ToInt32(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            #endregion
                        }
                        else
                        {
                            Session["Paid_User"] = "Unpaid";
                        }

                        if (status == "101")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Index");
                        }
                    }
                    else
                    {
                        objUser.ActivationStatus = "1";
                        Session["User"] = objUser;
                        return RedirectToAction("SignUp", "Index");
                    }
                }
                else if ((string)Session["fblogin"] == "page")
                {
                    Session["fblogin"] = null;
                    Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                    List<Domain.Myfashion.Domain.AddFacebookPage> lstAddFacebookPage = new List<Domain.Myfashion.Domain.AddFacebookPage>();
                    lstAddFacebookPage = (List<Domain.Myfashion.Domain.AddFacebookPage>)(new JavaScriptSerializer().Deserialize(apiobjFacebook.GetFacebookPages(code), typeof(List<Domain.Myfashion.Domain.AddFacebookPage>)));
                    Session["fbpage"] = lstAddFacebookPage;
                    return RedirectToAction("Index", "Home", new { hint = "fbpage" });
                }
                else if ((string)Session["fblogin"] == "fbgroup")
                {
                    Session["fblogin"] = null;

                    Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                    List<Domain.Myfashion.Domain.AddFacebookGroup> lstAddFacebookGroup = new List<Domain.Myfashion.Domain.AddFacebookGroup>();
                    lstAddFacebookGroup = (List<Domain.Myfashion.Domain.AddFacebookGroup>)(new JavaScriptSerializer().Deserialize(apiobjFacebook.GetFacebookGroups(code), typeof(List<Domain.Myfashion.Domain.AddFacebookGroup>)));
                    Session["fbgrp"] = lstAddFacebookGroup;
                    return RedirectToAction("Index", "Home", new { hint = "fbgrp" });
                }
            }
            else
            {
                Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                string facebookcode = code;
                Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();

                apiobjFacebook.Timeout = 120 * 1000;

                //string AddfacebookAccount = apiobjFacebook.AddFacebookAccount(facebookcode, objUser.Id.ToString(), Session["group"].ToString());
                string AddfacebookAccount = "";
                Domain.Myfashion.Domain.FacebookAccount objfacebookAccount = new Domain.Myfashion.Domain.FacebookAccount();
                try
                {
                    var res_addFacebook = apiobjFacebook.AddFacebookAccount(facebookcode, objUser.Id.ToString(), Session["group"].ToString());
                    AddfacebookAccount = res_addFacebook;
                    try
                    {
                        objfacebookAccount = (Domain.Myfashion.Domain.FacebookAccount)new JavaScriptSerializer().Deserialize(res_addFacebook, typeof(Domain.Myfashion.Domain.FacebookAccount));
                        AddfacebookAccount = objfacebookAccount.FbUserId;
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
                catch (Exception)
                {
                    AddfacebookAccount = "issue_access_token";
                }

                if (AddfacebookAccount == "issue_access_token")
                {
                    Response.Redirect(Helper.SBUtils.GetFacebookRedirectLink());
                }
                else if (AddfacebookAccount == "Account already Exist !")
                {
                }
                else
                {
                    Session["SocialManagerInfo"] = AddfacebookAccount;

                    //To enable the Facebook Message Pop up
                    TempData["IsFacebookAccountAdded"] = 1;
                    TempData["FacebookAccount"] = objfacebookAccount;
                }
            }
            return RedirectToAction("SocialMedia", "Home");

        }

        public ActionResult AuthenticateFacebook(string op)
        {
            string facebookurl = "../index/index";
            if (!string.IsNullOrEmpty(op))
            {

                if (op == "fbgroup")
                {
                    Session["fblogin"] = op;
                    facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                }
                else if (op == "page")
                {
                        Session["fblogin"] = op;
                        facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                   
                }
                else if (op == "fblogin")
                {
                    Session["fblogin"] = op;
                    
                    facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                    

                }
            }
            else if (op == "fblogin")
            {
                Session["fblogin"] = op;
                
                facebookurl = Helper.SBUtils.GetFacebookRedirectLink();

            }
            else
            {
                try
                {
                    try
                    {
                        Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                        logger.Error(Session["group"]);
                        logger.Error(Session["group"].ToString());
                        JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
                        
                                Session["fbSocial"] = "a";
                                facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                                Response.Redirect(facebookurl);
                      
                    }
                    catch (Exception ex)
                    {
                        logger.Error("ex.Message : " + ex.Message);
                        logger.Error("ex.StackTrace : " + ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            return Content(facebookurl);
        }

        public ActionResult GetFBPage()
        {
            string ret = string.Empty;
            try
            {

                List<Domain.Myfashion.Domain.AddFacebookPage> lstAddFacebookPage = new List<Domain.Myfashion.Domain.AddFacebookPage>();
                lstAddFacebookPage = (List<Domain.Myfashion.Domain.AddFacebookPage>)Session["fbpage"];
                foreach (var item in lstAddFacebookPage)
                {
                    ret += item.Name + "`" + item.ProfilePageId + "`" + item.AccessToken + "`" + item.Email + "`" + item.LikeCount + "~";
                }
                ret = ret.Substring(0, ret.Length - 1);
                Session["fbpage"] = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content(ret);
        }

        public ActionResult AddFbPage(string profileid, string accesstoken, string email)
        {
            Api.Facebook.Facebook objApiFacebook = new Api.Facebook.Facebook();
            Domain.Myfashion.Domain.User objUser = new Domain.Myfashion.Domain.User();
            objUser = (Domain.Myfashion.Domain.User)Session["User"];
            //objApiFacebook.AddFacebookPagesInfo(objUser.Id.ToString(), profileid, accesstoken, Session["group"].ToString(), email);
            //objApiFacebook.AddFacebookPagesInfoAsync(objUser.Id.ToString(), profileid, accesstoken, Session["group"].ToString(), email);

            //Api.Facebook.Facebook objApiFacebook1 = new Api.Facebook.Facebook();
            objApiFacebook.AddFacebookPagesInfo(objUser.Id.ToString(), profileid, accesstoken, Session["group"].ToString(), email);
            return Content("");
        }

        public ActionResult GetFBGroup()
        {

            string ret = string.Empty;
            try
            {

                List<Domain.Myfashion.Domain.AddFacebookGroup> lstAddFacebookGroup = new List<Domain.Myfashion.Domain.AddFacebookGroup>();
                lstAddFacebookGroup = (List<Domain.Myfashion.Domain.AddFacebookGroup>)Session["fbgrp"];
                foreach (var item in lstAddFacebookGroup)
                {
                    ret += item.Name + "`" + item.ProfileGroupId + "`" + item.AccessToken + "`" + item.Email + "~";
                }
                ret = ret.Substring(0, ret.Length - 1);
                Session["fbgrp"] = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content(ret);



        }
        [Authorize]
        [CustomAuthorize]
        public ActionResult AddFbGroup(string ProfileGroupId, string accesstoken, string email, string Name)
        {
            Api.Facebook.Facebook objApiFacebook = new Api.Facebook.Facebook();
            Domain.Myfashion.Domain.User objUser = new Domain.Myfashion.Domain.User();
            objUser = (Domain.Myfashion.Domain.User)Session["User"];
            objApiFacebook.AddFacebookGroupsInfo(objUser.Id.ToString(), ProfileGroupId, accesstoken, Session["group"].ToString(), email, Name);
            return Content("");
        }


        public ActionResult Addfacebookpagebyurl(string type, string url, string name)
        {
            var pageid = "";
            if (type == "fanpage")
            {
                try
                {
                    logger.Error("Enter in try Addfacebookpagebyurl");
                    try
                    {
                        Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                        logger.Error("Enter in try Addfacebookpagebyurl 1");

                        dynamic data = string.Empty;
                        string strdata = string.Empty;
                        try
                        {
                            Domain.Myfashion.Domain.AddFacebookPage objAddFacebookPage = (Domain.Myfashion.Domain.AddFacebookPage)(new JavaScriptSerializer().Deserialize(apiobjFacebook.GetFbPageDetails(url), typeof(Domain.Myfashion.Domain.AddFacebookPage)));
                            pageid = objAddFacebookPage.ProfilePageId;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(strdata);
                            logger.Error(data);
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }
                        {
                            logger.Error("data = fb.Get");
                            logger.Error(pageid);
                            string Accestoken = string.Empty;
                            string mail = string.Empty;
                            if (pageid != null)
                            {
                                try
                                {
                                    logger.Error("Inside apiobjFacebook.AddFacebookPagesByUrl");
                                    Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                                    apiobjFacebook.AddFacebookPagesByUrl(objUser.Id.ToString(), pageid, Session["group"].ToString(), name);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                    logger.Error("error1");
                                    logger.Error(ex.Message);
                                    logger.Error(ex.StackTrace);
                                }
                            }
                            else
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        logger.Error("dynamic data");
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }

            else
            {

            }


            return Content("");

        }



    }
}
