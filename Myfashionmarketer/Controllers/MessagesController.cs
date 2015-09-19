using Myfashionmarketer.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Myfashionmarketer.Controllers
{
    public class MessagesController : Controller
    {
        //
        // GET: /Message/.
        public static int inboxmessagecount = 0;
        public static int inboxchatmessagecount = 0;
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

      
        public ActionResult accordianprofiles()
        {
            Api.Team.Team objApiTeam = new Api.Team.Team();
            string groupid = Session["group"].ToString();
            Domain.Myfashion.Domain.Team team = (Domain.Myfashion.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Myfashion.Domain.Team));


            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            List<Domain.Myfashion.Domain.TeamMemberProfile> alstprofiles = (List<Domain.Myfashion.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(team.Id.ToString()), typeof(List<Domain.Myfashion.Domain.TeamMemberProfile>));

            return PartialView("_MessagesRightPartial", alstprofiles);//Content(view_MessagesaccordianprofilesPartial);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        //Commented by SumitGupta [09-02-2015]
        //public DataSet bindMessages()
        //{
        //    Domain.Myfashion.Domain.User _user = (Domain.Myfashion.Domain.User)Session["User"];
        //    DataSet ds = null;
        //    DataSet dataset = new DataSet();

        //    clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();

        //    //string[] profid = new string[] { };

        //    string[] profid = (string[])Session["ProfileSelected"];
        //    Domain.Myfashion.Domain.Team team = SBUtils.GetTeamFromGroupId();

        //    try
        //    {
        //        string message = string.Empty;


        //        try
        //        {
        //            //if (profid != null)
        //            //{
        //                //profid = Request.QueryString["profileid[]"].Split(',');
        //                //if (Request.QueryString["type"] != null)
        //                //{
        //                    Session["countMesageDataTable_" + profid] = null;
        //                //}
        //            //}


        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.StackTrace);
        //        }

        //        string facebook = string.Empty;

        //        foreach (var item in profid)
        //        {
        //            if (string.IsNullOrEmpty(item))
        //            {
        //                facebook = "emptyprofile";
        //            }
        //            else
        //            {
        //                facebook = "profile";
        //            }
        //        }

        //        if (string.IsNullOrEmpty(facebook))
        //        {
        //            facebook = "emptyprofile";
        //        }

        //        //if (facebook == "emptyprofile")
        //        {
        //            try
        //            {
        //                //DataSet ds = null;
        //                Session["countMesageDataTable_" + profid] = null;
        //                if (facebook == "emptyprofile")
        //                {
        //                    ds = clsfeedsandmess.bindMessagesIntoDataTable(team.Id); 
        //                }
        //                else
        //                {
        //                    ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid);
        //                }
        //                //FacebookFeedRepository fbFeedRepo = new FacebookFeedRepository();
        //                Session["MessageDataTable"] = ds;

        //                ds = (DataSet)Session["MessageDataTable"];

        //                if (Session["countMessageDataTable"] == null)
        //                {
        //                    Session["countMessageDataTable"] = 0;
        //                }
        //                int noOfDataToSkip = (int)Session["countMessageDataTable"];
        //                //DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(noOfDataToSkip + 15).CopyToDataTable();
        //                DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(15).CopyToDataTable();
        //                Session["countMessageDataTable"] = noOfDataToSkip + 15;
        //                //message = this.BindData(records);

        //                dataset.Tables.Add(records);
        //                return dataset;

        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //        }

        //        //else
        //        //{
        //        //    try
        //        //    {
        //        //        DataSet ds = null;
        //        //        Session["countMessageDataTable"] = null;

        //        //        ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid);
        //        //        Session["MessageDataTable"] = ds;

        //        //        ds = (DataSet)Session["MessageDataTable"];



        //        //        if (Session["countMesageDataTable_" + profid] == null)
        //        //        {
        //        //            Session["countMesageDataTable_" + profid] = 0;
        //        //        }

        //        //        int noOfDataToSkip = (int)Session["countMesageDataTable_" + profid];


        //        //        DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(noOfDataToSkip + 15).CopyToDataTable();
        //        //        Session["countMesageDataTable_" + profid] = noOfDataToSkip + 15;

        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        Console.WriteLine(ex.StackTrace);
        //        //    }
        //        //}

        //        //if (string.IsNullOrEmpty(message))
        //        //{

        //        //}

        //        //Response.Write(message);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }

        //    return dataset;
        //}

        //Updated by SumitGupta [09-02-2015]

        public DataSet bindMessages()
        {
            Domain.Myfashion.Domain.User _user = (Domain.Myfashion.Domain.User)Session["User"];
            DataSet ds = null;
            DataSet dataset = new DataSet();

            clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();

            //string[] profid = new string[] { };

            string[] profid = (string[])Session["ProfileSelected"];
            Domain.Myfashion.Domain.Team team = SBUtils.GetTeamFromGroupId();

            try
            {
                //For getting data range
                Session["MessageDataTable"] = ds;

                ds = (DataSet)Session["MessageDataTable"];

                if (Session["countMessageDataTable"] == null)
                {
                    Session["countMessageDataTable"] = 0;
                }

                int noOfDataToSkip = (int)Session["countMessageDataTable"];
                //DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(15).CopyToDataTable();
                Session["countMessageDataTable"] = noOfDataToSkip + 15;

                string message = string.Empty;


                try
                {
                    //if (profid != null)
                    //{
                    //profid = Request.QueryString["profileid[]"].Split(',');
                    //if (Request.QueryString["type"] != null)
                    //{
                    Session["countMesageDataTable_" + profid] = null;
                    //}
                    //}


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                string facebook = string.Empty;

                foreach (var item in profid)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        facebook = "emptyprofile";
                    }
                    else
                    {
                        facebook = "profile";
                    }
                }

                if (string.IsNullOrEmpty(facebook))
                {
                    facebook = "emptyprofile";
                }

                //if (facebook == "emptyprofile")
                {
                    try
                    {
                        //DataSet ds = null;
                        Session["countMesageDataTable_" + profid] = null;
                        if (facebook == "emptyprofile")
                        {
                            //Updated by SumitGupta [09-02-2015]
                            //ds = clsfeedsandmess.bindMessagesIntoDataTable(team.Id);
                            ds = clsfeedsandmess.bindMessagesIntoDataTable(team.Id, noOfDataToSkip, _user.Id);
                        }
                        else
                        {
                            //Updated by SumitGupta [09-02-2015]
                            //ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid);
                            ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid, noOfDataToSkip, _user.Id);
                        }

                        Session["MessageDataTable"] = ds;

                        ds = (DataSet)Session["MessageDataTable"];

                        if (Session["countMessageDataTable"] == null)
                        {
                            Session["countMessageDataTable"] = 0;
                        }

                        //Updated by Sumit Gupta [09-02-2015]
                        //dataset.Tables.Add(records);
                        //return dataset;
                        return ds;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }

              

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return dataset;
        }

        
        public ActionResult MessagesMidPartialNew()
        {
            Session["countMessageDataTable"] = 0;
            Session["ProfileSelected"] = new string[] { }; ;
            DataSet dataset = bindMessages();
            if (dataset.Tables.Count > 0 && dataset != null)
            {
                return PartialView("_MessagesMidPartialNew", dataset);
            }
            else
            {
                return Content("nodata");
            }

        }



       
        public ActionResult BindInboxOnScroll()
        {
            DataSet dataset = bindMessages();
            if (dataset.Tables.Count > 0 && dataset != null)
            {
                return PartialView("_SmartInboxPartial", dataset);
            }
            else
            {
                return Content("nodata");
            }

        }

       
        public ActionResult LoadMessagesByProfile()
        {
            Session["countMessageDataTable"] = 0;
            try
            {
                string[] profid = Request.QueryString["profileid[]"].Split(',');
                Session["ProfileSelected"] = profid;
            }
            catch (Exception ex)
            {
                Session["ProfileSelected"] = new string[] { };
            }
            DataSet dataset = bindMessages();
            if (dataset.Tables.Count > 0 && dataset != null)
            {
                return PartialView("_SmartInboxPartial", dataset);
            }
            else
            {
                return Content("nodata");
            }

        }

       
        public ActionResult Task()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View();
            }
            // return View();
        }

        
        public ActionResult loadtask()
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];

            Domain.Myfashion.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Myfashion.Domain.Tasks> taskdata = (List<Domain.Myfashion.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.getAllTasksOfUserList(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Myfashion.Domain.Tasks>));
            ViewBag.Task = "MyTask";
            return PartialView("_TaskPartial", taskdata);

        }

      
        public PartialViewResult LoadIncompleteTask()
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];

            Domain.Myfashion.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Myfashion.Domain.Tasks> taskdata = (List<Domain.Myfashion.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.GetAllIncompleteTaskofUser(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Myfashion.Domain.Tasks>));
            ViewBag.Task = "PendingTask";
            return PartialView("_TaskPartial", taskdata);
        }

       
        public PartialViewResult LoadCompleteTask()
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];

            Domain.Myfashion.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Myfashion.Domain.Tasks> taskdata = (List<Domain.Myfashion.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.GetAllCompleteTaskofUser(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Myfashion.Domain.Tasks>));
            ViewBag.Task = "CompleteTask";
            return PartialView("_TaskPartial", taskdata);
        }

       
        public PartialViewResult LoadTeamTask()
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];

            Domain.Myfashion.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Myfashion.Domain.Tasks> taskdata = (List<Domain.Myfashion.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.GetAllTeamTask(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Myfashion.Domain.Tasks>));
            ViewBag.Task = "TeamTask";
            return PartialView("_TaskPartial", taskdata);
        }


        
        public ActionResult savetask()
        {
            string groupid = Session["group"].ToString();

            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];

            string descritption = Request.Unvalidated.QueryString["description"];
            string MessageDate = Request.QueryString["msgdate"];

            string AssignDate = Request.QueryString["now"];

            string comment = Request.QueryString["comment"];

            Guid idtoassign = Guid.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["memberid"]))
                {
                    idtoassign = Guid.Parse(Request.QueryString["memberid"]);
                }
            }
            catch (Exception ex)
            {

            }

            Api.Tasks.Tasks1 objTasks = new Api.Tasks.Tasks1();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            objApiTasks.AddNewTaskWithGroup(descritption, MessageDate, objUser.Id.ToString(), objTasks, idtoassign.ToString(), comment, AssignDate, groupid);

            string Groupid = Session["group"].ToString();

            Api.BusinessSetting.BusinessSetting objApiBusinessSetting = new Api.BusinessSetting.BusinessSetting();
            Domain.Myfashion.Domain.BusinessSetting objbsns = (Domain.Myfashion.Domain.BusinessSetting)new JavaScriptSerializer().Deserialize(objApiBusinessSetting.GetDetailsofBusinessOwner(Groupid), typeof(Domain.Myfashion.Domain.BusinessSetting));
            if (objbsns.TaskNotification == true)
            {
                Api.User.User ObjApiUser = new Api.User.User();
                Domain.Myfashion.Domain.User UsertoSendMail = (Domain.Myfashion.Domain.User)(new JavaScriptSerializer().Deserialize(ObjApiUser.getUsersById(idtoassign.ToString()), typeof(Domain.Myfashion.Domain.User)));
                Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
                string mailsender = "";
                try
                {
                    var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_TaskNotificationMailPartial", UsertoSendMail);
                    string Subject = "TASK ASSIGNMENT on Socioboard";

                    mailsender = ApiobjMailSender.SendTaskNotificationMail(UsertoSendMail.EmailId, mailBody, Subject);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return Content("");
        }

        public void updatedstatus()
        {
            
        }


        public ActionResult sentmsg()
        {

            return View();
        }

        
        public ActionResult loadsentmsg()
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.getAllSentMessageDetails(AllProfileId, objUser.Id.ToString()), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

        
        public ActionResult addTaskComment()
        {
            string comment = Request.QueryString["comment"];
            string taskid = Request.QueryString["taskid"];
            string CommentDateTime = Request.QueryString["CommentDateTime"];

            Domain.Myfashion.Domain.User objDomainUser = (Domain.Myfashion.Domain.User)Session["User"];

            Api.TaskComment.TaskComment objApiTaskComment = new Api.TaskComment.TaskComment();
            string res = objApiTaskComment.AddTaskComment(comment, objDomainUser.Id.ToString(), taskid, CommentDateTime, DateTime.Now);

            return Content(res);
        }



        
        public ActionResult TwitterReply()
        {
            try
            {
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                string comment = Request.QueryString["comment"];
                string ProfileId = Request.QueryString["ProfileId"];
                string messageid = Request.QueryString["messageid"];
                string replypost = ApiobjTwitter.TwitterReplyUpdate(comment, objUser.Id.ToString(), ProfileId, messageid);
                //JArray replystatus =(JArray)(new JavaScriptSerializer().Deserialize(replypost, typeof(JArray)));
                return Content("success");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return Content("");
            }
        }

        
        public ActionResult FacebokReply()
        {
            try
            {
                Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                string comment = Request.QueryString["comment"];
                string ProfileId = Request.QueryString["ProfileId"];
                string messageid = Request.QueryString["messageid"];
                string replaypost = ApiobjFacebook.FacebookComment(comment, ProfileId, messageid, objUser.Id.ToString());
                return Content("success");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return Content("");
            }
        }

        
        public ActionResult SaveArchiveMessage()
        {
            try
            {
                Api.ArchiveMessage.ArchiveMessage ApiobjArchiveMessage = new Api.ArchiveMessage.ArchiveMessage();
                Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                string ProfileId = Request.QueryString["ProfileId"];
                string MessageId = Request.QueryString["MessageId"];
                string Network = Request.QueryString["network"];
                string UserName = Request.QueryString["username"];
                string MessageDate = Request.QueryString["MessageDate"];
                string ProfileUrl = Request.QueryString["profileurl"];
                string Message = Request.Form["message"];

                try
                {
                    if (!ApiobjArchiveMessage.CheckArchiveMessageExists(objUser.Id.ToString(), MessageId))
                    {
                        ApiobjArchiveMessage.AddArchiveMessage(objUser.Id.ToString(), ProfileId, Network, UserName, MessageId, Message, MessageDate, ProfileUrl);
                        ApiobjArchiveMessage.DeleteArchiveMessage(objUser.Id.ToString(), MessageId, Network);
                        return Content("Archived successfully");
                    }
                    else
                    {
                        return Content("Message already Archived");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return Content("Somthing went wrong!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("");
            }
        }

      
        public ActionResult Archive()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View();
            }
            // return View();
        }

        
        public ActionResult loadarchive()
        {

            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ArchiveMessage.ArchiveMessage ApiobjArchiveMessage = new Api.ArchiveMessage.ArchiveMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ArchiveMessage> lstAllArchive = (List<Domain.Myfashion.Domain.ArchiveMessage>)(new JavaScriptSerializer().Deserialize(ApiobjArchiveMessage.GetAllArchiveMessagesDetails(objUser.Id.ToString(), AllProfileId), typeof(List<Domain.Myfashion.Domain.ArchiveMessage>)));
            return PartialView("_ArchivePartial", lstAllArchive);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ChangeTaskStatus(string Taskid, string Status)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.Tasks.Tasks objTaskStatusChange = new Api.Tasks.Tasks();
            objTaskStatusChange.ChangeTaskStatus(objUser.Id.ToString(), Taskid, Status);
            return Content("Success");
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult DeleteTask(string Taskid)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.Tasks.Tasks objTaskStatusChange = new Api.Tasks.Tasks();
            objTaskStatusChange.DeleteTask(Taskid);
            return Content("Success");
        }

       
        public ActionResult getProfileDetails(string ProfileId, string Network)
        {

            Dictionary<string, object> _dicProfileDetails = new Dictionary<string, object>();
            if (Network == "twitter")
            {
                Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                string ProfileDescription = ApiobjTwitter.TwitterProfileDetails(objUser.Id.ToString(), ProfileId);
                // Domain.Socioboard.Helper.TwitterProfileDetails ProfileDetails = (Domain.Socioboard.Helper.TwitterProfileDetails)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Socioboard.Helper.TwitterProfileDetails)));
                Domain.Myfashion.Domain.TwitterAccount ProfileDetails = (Domain.Myfashion.Domain.TwitterAccount)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Myfashion.Domain.TwitterAccount)));
                _dicProfileDetails.Add("Twitter", ProfileDetails);
            }
            if (Network == "facebook")
            {
                Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                string ProfileDescription = ApiobjFacebook.FacebookProfileDetails(objUser.Id.ToString(), ProfileId);
                Domain.Myfashion.Domain.FacebookAccount ProfileDetails = (Domain.Myfashion.Domain.FacebookAccount)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Myfashion.Domain.FacebookAccount)));
                _dicProfileDetails.Add("Facebook", ProfileDetails);
            }
            if (Network == "linkedin")
            {
                Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
                Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
                string ProfileDescription = ApiobjLinkedin.LinkedinProfileDetails(objUser.Id.ToString(), ProfileId);
                Domain.Myfashion.Domain.LinkedInAccount ProfileDetails = (Domain.Myfashion.Domain.LinkedInAccount)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Myfashion.Domain.LinkedInAccount)));
                _dicProfileDetails.Add("Linkedin", ProfileDetails);
            }

            return PartialView("_SocialProfileDetail", _dicProfileDetails);
        }

       
        public ActionResult LoadSentmsgByDay(string day)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageforADay(objUser.Id.ToString(), AllProfileId, day), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

       
        public ActionResult LoadSentmsgByDays(string days)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByDays(objUser.Id.ToString(), AllProfileId, days), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

        
        public ActionResult LoadSentmsgByMonth(string month)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByMonth(objUser.Id.ToString(), AllProfileId, month), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

        
        public ActionResult LoadSentmsgForCustomrange(string sdate, string ldate)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllSentMessageDetailsForCustomrange(objUser.Id.ToString(), AllProfileId, sdate, ldate), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

        public ActionResult ShowMsgMailPopUp(string MsgId, string Network)
        {
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Dictionary<string, object> twtinfo = new Dictionary<string, object>();
            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            if (Network == "twitter")
            {
                Domain.Myfashion.Domain.TwitterMessage twtmessage = (Domain.Myfashion.Domain.TwitterMessage)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.GetTwitterMessageByMessageId(objUser.Id.ToString(), MsgId), typeof(Domain.Myfashion.Domain.TwitterMessage)));
                twtinfo.Add("twt_msg", twtmessage);
            }
            else if (Network == "facebook")
            {
                Domain.Myfashion.Domain.FacebookMessage fbmessage = (Domain.Myfashion.Domain.FacebookMessage)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.GetFacebookMessageByMessageId(objUser.Id.ToString(), MsgId), typeof(Domain.Myfashion.Domain.FacebookMessage)));
                twtinfo.Add("fb_msg", fbmessage);
            }
            return PartialView("_MailMsgSendingPartial", twtinfo);
            //return PartialView("_TwitterMailSendingPartial", twtfeed);
        }

        public ActionResult ShowInboxMsgMailPopUp(string MsgId)
        {
            Domain.Myfashion.Domain.User _User = (Domain.Myfashion.Domain.User)Session["User"];
            Api.InboxMessages.InboxMessages ApiInboxMessages = new Api.InboxMessages.InboxMessages();
            Domain.Myfashion.Domain.InboxMessages _InboxMessages = (Domain.Myfashion.Domain.InboxMessages)new JavaScriptSerializer().Deserialize(ApiInboxMessages.getInboxMessageByMessageId(_User.Id.ToString(), MsgId), typeof(Domain.Myfashion.Domain.InboxMessages));
            return PartialView("_MailMsgSendingPartial", _InboxMessages);
        }

        //Vikash [04/12/2014]
        public void ExportSentMessages(List<Domain.Myfashion.Domain.ScheduledMessage> _ScheduledMessage, Domain.Myfashion.Domain.User _user)
        {
            var details = new System.Data.DataTable("sentmessage");
            details.Columns.Add("Date", typeof(string));
            details.Columns.Add("Network", typeof(string));
            details.Columns.Add("ProfileId", typeof(string));
            details.Columns.Add("Sent By", typeof(string));
            details.Columns.Add("Message", typeof(string));
            foreach (Domain.Myfashion.Domain.ScheduledMessage item_sent in _ScheduledMessage)
            {
                details.Rows.Add(item_sent.ScheduleTime, item_sent.ProfileType, item_sent.ProfileId, _user.UserName, item_sent.ShareMessage);
            }
            var grid = new GridView();
            grid.DataSource = details;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=SentMessages_" + (DateTime.Now.Ticks).ToString() + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public ActionResult ExportSentmsgByDay(string day)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageforADay(objUser.Id.ToString(), AllProfileId, day), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }
        public ActionResult ExportSentmsgByDays(string days)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByDays(objUser.Id.ToString(), AllProfileId, days), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }
        public ActionResult ExportSentmsgByMonth(string month)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByMonth(objUser.Id.ToString(), AllProfileId, month), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }
        public ActionResult ExportSentmsgForCustomrange(string sdate, string ldate)
        {
            string AllProfileId = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Myfashion.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllSentMessageDetailsForCustomrange(objUser.Id.ToString(), AllProfileId, sdate, ldate), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }


       
        public ActionResult BindUserProfileByGroup()
        {
            Api.Team.Team objApiTeam = new Api.Team.Team();
            string groupid = Session["group"].ToString();
            Domain.Myfashion.Domain.Team team = (Domain.Myfashion.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Myfashion.Domain.Team));
            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            List<Domain.Myfashion.Domain.TeamMemberProfile> alstprofiles = (List<Domain.Myfashion.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(team.Id.ToString()), typeof(List<Domain.Myfashion.Domain.TeamMemberProfile>));
            return PartialView("_InboxProfilePartial", alstprofiles);
        }


      
        public ActionResult BindUserMessageType()
        {
            return PartialView("_InboxMessageTypePartial");
        }

       
        public ActionResult BindInboxMessage(string load, string arrmsgtype, string arrid)
        {
            string AllProfileId = string.Empty;
            string MessageType = string.Empty;
            List<Domain.Myfashion.Domain.InboxMessages> lstmsg = new List<Domain.Myfashion.Domain.InboxMessages>();
            Api.InboxMessages.InboxMessages ApiInboxMessages = new Api.InboxMessages.InboxMessages();
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            if (load == "first")
            {
                Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = new Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object>();
                try
                {
                    allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                foreach (var item in allprofileofuser)
                {
                    try
                    {
                        if (item.Key.ProfileType == "twitter")
                        {
                            AllProfileId += item.Key.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                }
                try
                {
                    AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                inboxmessagecount = 0;
                MessageType = "twt_followers,twt_mention,twt_retweet,fb_notification";
                //lstmsg = (List<Domain.Myfashion.Domain.InboxMessages>)new JavaScriptSerializer().Deserialize(ApiInboxMessages.GetInboxMessage(objUser.Id.ToString(), AllProfileId, MessageType, "0", "10"), typeof(List<Domain.Myfashion.Domain.InboxMessages>));
            }
            else if (load == "filter")
            {
                inboxmessagecount = 0;
                AllProfileId = arrid;
                MessageType = arrmsgtype;
                //lstmsg = (List<Domain.Myfashion.Domain.InboxMessages>)new JavaScriptSerializer().Deserialize(ApiInboxMessages.GetInboxMessage(objUser.Id.ToString(), AllProfileId, MessageType, "0", "10"), typeof(List<Domain.Myfashion.Domain.InboxMessages>));
            }
            else if (load == "scroll")
            {
                inboxmessagecount = inboxmessagecount + 10;
                AllProfileId = arrid;
                MessageType = arrmsgtype;

            }
            lstmsg = (List<Domain.Myfashion.Domain.InboxMessages>)new JavaScriptSerializer().Deserialize(ApiInboxMessages.GetInboxMessage(objUser.Id.ToString(), AllProfileId, MessageType, inboxmessagecount.ToString(), "10"), typeof(List<Domain.Myfashion.Domain.InboxMessages>));
            if (lstmsg.Count > 0)
            {
                return PartialView("_InboxPartial", lstmsg);
            }
            else
            {
                return Content("no_data");
            }
        }

       
        public ActionResult Inbox()
        {
            return View();
        }

       
        public ActionResult BindUserProfileByGroupChat()
        {
            Api.Team.Team objApiTeam = new Api.Team.Team();
            string groupid = Session["group"].ToString();
            Domain.Myfashion.Domain.Team team = (Domain.Myfashion.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Myfashion.Domain.Team));
            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            List<Domain.Myfashion.Domain.TeamMemberProfile> alstprofiles = (List<Domain.Myfashion.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(team.Id.ToString()), typeof(List<Domain.Myfashion.Domain.TeamMemberProfile>));
            return PartialView("_InboxChatProfilePartial", alstprofiles);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult BindInboxChatMessage(string load, string arrid)
        {
            string TwitterProfiles = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];

            if (load == "first")
            {
                Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
                foreach (var item in allprofileofuser)
                {
                    try
                    {
                        if (item.Key.ProfileType == "twitter" || item.Key.ProfileType == "facebook_page")
                        {
                            TwitterProfiles += item.Key.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                }
                try
                {
                    TwitterProfiles = TwitterProfiles.Substring(0, (TwitterProfiles.Length - 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                inboxchatmessagecount = 0;
            }
            else if (load == "filter")
            {
                inboxchatmessagecount = 0;
                TwitterProfiles = arrid;
            }
            Api.TwitterDirectMessages.TwitterDirectMessages ApiTwitterDirectMessages = new Api.TwitterDirectMessages.TwitterDirectMessages();

            List<Domain.Myfashion.Domain.TwitterDirectMessages> _TwitterDirectMessages = (List<Domain.Myfashion.Domain.TwitterDirectMessages>)new JavaScriptSerializer().Deserialize(ApiTwitterDirectMessages.GetDistinctTwitterDirectMessagesByProfilesAndUserId(objUser.Id.ToString(), TwitterProfiles), typeof(List<Domain.Myfashion.Domain.TwitterDirectMessages>));
            if (_TwitterDirectMessages.Count > 0)
            {
                return PartialView("_InboxChatPartial", _TwitterDirectMessages);
            }
            else
            {
                return Content("no_data");
            }
        }

        
        public ActionResult ShowChat(string SenderId, string RecipientId)
        {
            ViewBag.ProfileId = RecipientId;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.TwitterDirectMessages.TwitterDirectMessages ApiTwitterDirectMessages = new Api.TwitterDirectMessages.TwitterDirectMessages();
            List<Domain.Myfashion.Domain.TwitterDirectMessages> _TwitterDirectMessages = (List<Domain.Myfashion.Domain.TwitterDirectMessages>)new JavaScriptSerializer().Deserialize(ApiTwitterDirectMessages.GetConversation(objUser.Id.ToString(), SenderId, RecipientId), typeof(List<Domain.Myfashion.Domain.TwitterDirectMessages>));
            return PartialView("_ShowChatPartial", _TwitterDirectMessages);
        }
	}
}