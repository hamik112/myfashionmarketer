﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using log4net;
using Myfashionmarketer.App_Start;
using Domain.Myfashion.Domain;
using Api.Myfashionmarketer.Services;
using Myfashionmarketer.Helper;

namespace Myfashionmarketer.Controllers
{
    public class PublishingController : Controller
    {
        //
        // GET: /Publishing/

        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                @ViewBag.Message = Request.QueryString["Message"];
                
                    return View();
               
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //return View();
        }
        public ActionResult Socioqueue()
        {
           
                return View();
           
        }
        public ActionResult Draft()
        {
            
                return View();
           
        }

        public ActionResult loadsocialqueue()
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            //ApiobjScheduledMessage.GetSociaoQueueMessageByUserIdAndGroupId(objUser.Id.ToString(), Session["group"].ToString());
            List<ScheduledMessage> lstScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetSociaoQueueMessageByUserIdAndGroupId(objUser.Id.ToString(), Session["group"].ToString()), typeof(List<ScheduledMessage>)));
            return PartialView("_SocialQueuePartial", lstScheduledMessage);
        }

        public ActionResult loaddrafts()
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            List<Drafts> lstScheduledMessage = (List<Drafts>)(new JavaScriptSerializer().Deserialize(ApiobjDrafts.GetDraftMessageByUserIdAndGroupId(objUser.Id.ToString(), Session["group"].ToString()), typeof(List<Drafts>)));
            return PartialView("_DraftPartial", lstScheduledMessage);
        }
        public ActionResult ModifyDraftMessage(string draftid, string draftmsg)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            string retmsg= ApiobjDrafts.UpdateDraftsMessage(draftid, objUser.Id.ToString(), Session["group"].ToString(), draftmsg);
            return Content(retmsg);
        }
        public ActionResult DeleteDraftMessage(string draftid)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            string retmsg = ApiobjDrafts.DeleteDrafts(draftid);
            return Content(retmsg);
        }

        public ActionResult DeleteSocioQueueMessage(string msgid)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            string retmsg = ApiobjScheduledMessage.DeleteSchecduledMessage(msgid);
            return Content(retmsg);
        }


        public ActionResult EditSocioQueueMessage(string msgid, string msg)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            string retmsg = ApiobjScheduledMessage.EditSchecduledMessage(msgid, msg);
            return Content(retmsg);
        }

        public ActionResult ComposeScheduledMessage()
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Dictionary<TeamMemberProfile, object> dict_TeamMember = new Dictionary<TeamMemberProfile, object>();
                      if (Session["group"] != null)
            {
                dict_TeamMember = SBUtils.GetUserProfilesccordingToGroup();
            }
                      return PartialView("_ComposeMessageSchedulerPartial", dict_TeamMember);
        }


        public ActionResult ScheduledMessage(string scheduledmessage, string scheduleddate, string scheduledtime, string profiles, string clienttime)
        {
            var fi = Request.Files["file"];
            string file = string.Empty;
            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");

                    // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                    file = path + "\\" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(file);
                    path = path + "\\" + fi.FileName;
                }
            }

            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            string retmsg = ApiobjScheduledMessage.AddAllScheduledMessage(profiles, scheduledmessage, clienttime, scheduleddate, scheduledtime, objUser.Id.ToString(), file);
            return Content("_ComposeMessagePartial");
        }


        public ActionResult SaveDraft(string scheduledmessage)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            string retmsg = ApiobjDrafts.AddDraft(objUser.Id.ToString(),Session["group"].ToString(),DateTime.Now,scheduledmessage);
            return Content(retmsg);
        }

        
    }
}
