﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Myfashionmarketer.App_Start;

namespace Myfashionmarketer.Controllers
{
    //[Authorize]
    //[CustomAuthorize]
    public class InstagramManagerController : Controller
    {
        ILog logger = LogManager.GetLogger(typeof(InstagramManagerController));
        //
        // GET: /InstagramManager/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Instagram()
        {
            string AddTwitterAccount = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            string code = (String)Request.QueryString["code"];

            Api.Instagram.Instagram apiobjInstagram = new Api.Instagram.Instagram();
            try
            {
                AddTwitterAccount = apiobjInstagram.AddInstagramAccount(ConfigurationManager.AppSettings["InstagramClientKey"], ConfigurationManager.AppSettings["InstagramClientSec"], ConfigurationManager.AppSettings["InstagramCallBackURL"], objUser.Id.ToString(), Session["group"].ToString(), code);
                Session["SocialManagerInfo"] = AddTwitterAccount;
            }
            catch (Exception ex)
            {
                logger.Error("Instagram =>" + ex.StackTrace);
                logger.Error("Instagram =>" + ex.Message);
                Console.WriteLine(ex.Message);
            }
            logger.Error("Instagram =>" + AddTwitterAccount);
            if (Session["SocialManagerInfo"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("SocialMedia", "Home");
        }
        
        public ActionResult AuthenticateInstagram()
        {
            try
            {
                try
                {
                   
                    
                            Api.Instagram.Instagram ApiobjInstagram = new Api.Instagram.Instagram();
                            string redirecturl = ApiobjInstagram.GetInstagramRedirectUrl(ConfigurationManager.AppSettings["InstagramClientKey"], ConfigurationManager.AppSettings["InstagramClientSec"], ConfigurationManager.AppSettings["InstagramCallBackURL"]);
                            logger.Error("AuthenticateInstagram => redirect uri =>" + redirecturl);
                            if (redirecturl.Contains("FacebookManager") || redirecturl.Contains("Facebook"))
                            {
                                redirecturl = redirecturl.Replace("FacebookManager","InstagramManager").Replace("Facebook","Instagram");
                                Response.Redirect(redirecturl);
                            }
                            else
                            {
                                Response.Redirect(redirecturl);
                            }
                            
                       
                    
                }
                catch (Exception ex)
                {
                    logger.Error("AuthenticateInstagram =>" +ex.StackTrace);
                    logger.Error("AuthenticateInstagram =>" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("AuthenticateInstagram =>" + ex.StackTrace);
                logger.Error("AuthenticateInstagram =>" + ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }



    }
}
