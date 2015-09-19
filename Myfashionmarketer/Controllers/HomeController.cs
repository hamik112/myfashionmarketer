using Domain.Myfashion.Domain;
using Myfashionmarketer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Myfashionmarketer.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {

            if (Session["User"] != null)
            {
                User _user = (User)Session["User"];
                string pass = _user.Password;
                string pid = _user.PuId;
                string puid = pid.Substring(36, pid.Length - 36);
                ViewBag.PuId = puid;
                ViewBag.user = _user;
                Api.Team.Team ApiobjTeam = new Api.Team.Team();
                List<Domain.Myfashion.Domain.Groups> lstgroup = (List<Domain.Myfashion.Domain.Groups>)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjTeam.GetTeamByUserId(_user.Id.ToString()), typeof(List<Domain.Myfashion.Domain.Groups>)));
                foreach (Groups item in lstgroup)
                {
                    System.Web.HttpContext.Current.Session["group"] = item.Id;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult SocialMedia()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
          
        }

        public ActionResult UserProfile()
        {
            return PartialView("_HomeUserProfilePartial");
        }

        public ActionResult ProfileSnapshot(string op)
        {
            int CountProfileSnapshot = 0;

            try
            {
                if (op == null)
                {
                    Session["CountProfileSnapshot"] = 0;
                }
            }
            catch (Exception ex)
            {

            }

            if (Session["CountProfileSnapshot"] != null)
            {
                CountProfileSnapshot = (int)Session["CountProfileSnapshot"];
            }
            else
            {
                Session["CountProfileSnapshot"] = 0;
            }


            List<TeamMemberProfile> lstTeamMemberProfile = SBUtils.GetUserTeamMemberProfiles();
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, Dictionary<object, List<object>>> diclist = SBUtils.GetUserProfilesSnapsAccordingToGroup(lstTeamMemberProfile, CountProfileSnapshot);

            if (diclist.Count > 0 && diclist != null)
            {
                Session["CountProfileSnapshot"] = (int)Session["CountProfileSnapshot"] + diclist.Count;
            }

            return PartialView("_HomeProfileSnapshotPartial", diclist);
        }

        public ActionResult loadprofiles()
        {
            User objUser = (User)Session["User"];
            Dictionary<TeamMemberProfile, object> dict_TeamMember = new Dictionary<TeamMemberProfile, object>();
            if (Session["group"] != null)
            {
                dict_TeamMember = SBUtils.GetUserProfilesccordingToGroup();
            }
            return PartialView("_HomeUserProfilePartial", dict_TeamMember);
        }

        public ActionResult ComposeMessage()
        {
            User objUser = (User)Session["User"];
            Dictionary<TeamMemberProfile, object> dict_TeamMember = new Dictionary<TeamMemberProfile, object>();
            if (Session["group"] != null)
            {
                dict_TeamMember = SBUtils.GetUserProfilesccordingToGroup();
            }
            return PartialView("_ComposeMessagePartial", dict_TeamMember);
        }


        public ActionResult RecentProfiles()
        {
            User objUser = (User)Session["User"];
            Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
            ApiobjTwitter.Timeout = 300000;
            List<Domain.Myfashion.Helper.TwitterRecentFollower> lstTwitterRecentFollower = (List<Domain.Myfashion.Helper.TwitterRecentFollower>)(new JavaScriptSerializer().Deserialize(ApiobjTwitter.TwitterRecentFollower(objUser.Id.ToString()), typeof(List<Domain.Myfashion.Helper.TwitterRecentFollower>)));
            return PartialView("_RecentFollowerPartial", lstTwitterRecentFollower);
        }

        public ActionResult DisplayCount()
        {
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            int fbmsgcount = 0;
            int twtmsgcount = 0;
            int allsentmsgcount = 0;
            User objUser = (User)Session["User"];
            Dictionary<Domain.Myfashion.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    if (item.Key.ProfileType == "facebook" || item.Key.ProfileType == "facebook_page")
                    {
                        FbProfileId += item.Key.ProfileId + ',';
                    }
                    else if (item.Key.ProfileType == "twitter")
                    {
                        TwtProfileId += item.Key.ProfileId + ',';
                    }
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                FbProfileId = FbProfileId.Substring(0, FbProfileId.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                TwtProfileId = TwtProfileId.Substring(0, TwtProfileId.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                Api.FacebookFeed.FacebookFeed objFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                objFacebookFeed.Timeout = 300000;
                fbmsgcount = objFacebookFeed.GetFeedCountByProfileIdAndUserId(objUser.Id.ToString(), FbProfileId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                Api.TwitterMessage.TwitterMessage objTwitterMessage = new Api.TwitterMessage.TwitterMessage();
                objTwitterMessage.Timeout = 300000;
                twtmsgcount = objTwitterMessage.GetFeedCountByProfileIdAndUserId(objUser.Id.ToString(), FbProfileId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                Api.ScheduledMessage.ScheduledMessage objScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                objScheduledMessage.Timeout = 300000;
                allsentmsgcount = objScheduledMessage.GetSentMessageCountByProfileIdAndUserId(objUser.Id.ToString(), FbProfileId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            string _totalIncomingMessage = "0";
            string _totalSentMessage = "0";
            string _totalTwitterFollowers = "0";
            string _totalFacebookFan = "0";
            try
            {
                _totalIncomingMessage = (fbmsgcount + twtmsgcount).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                _totalSentMessage = allsentmsgcount.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                _totalTwitterFollowers = SBUtils.GetAllTwitterFollowersCountofUser(TwtProfileId, objUser.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                _totalFacebookFan = SBUtils.GetAllFacebookFancountofUser(FbProfileId, objUser.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            ViewBag._totalIncomingMessage = _totalIncomingMessage;
            ViewBag._totalSentMessage = _totalSentMessage;
            ViewBag._totalTwitterFollowers = _totalTwitterFollowers;
            ViewBag._totalFacebookFan = _totalFacebookFan;
            return PartialView("_HomeUserActivityPartial");
        }



        public ActionResult RemainingDays()
        {
            int daysremaining = 0;
            string remainingday = string.Empty;
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];

            if (Session["days_remaining"] == null)
            {
                if (objUser.PaymentStatus == "unpaid" && objUser.AccountType != "Free")
                {
                    daysremaining = (objUser.ExpiryDate.Date - DateTime.Now.Date).Days;
                    if (daysremaining < 0)
                    {
                        daysremaining = -1;
                    }
                    Session["days_remaining"] = daysremaining;
                    if (daysremaining <= -1)
                    {
                        remainingday = objUser.AccountType.ToString() + "##" + daysremaining.ToString();
                    }
                    else if (daysremaining == 0)
                    {
                        //remainingday = "Your trial " + objUser.AccountType.ToString() + " account will expire Today please upgrade to paid plan.";
                        remainingday = objUser.AccountType.ToString() + "##" + daysremaining.ToString();
                    }
                    else
                    {
                        //remainingday = "Your trial " + objUser.AccountType.ToString() + " account will expire in " + daysremaining + " days, please upgrade to paid plan.";
                        remainingday = objUser.AccountType.ToString() + "##" + daysremaining.ToString();
                    }
                }

            }

            return Content(remainingday);
        }


        public string LogOut()
        {
            Session["User"] = null;
            Session.RemoveAll();
            return "success";
        
        }

        public ActionResult globustracker()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult googleanalytics()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

        }
        public ActionResult googleacquisition()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

        }

        public ActionResult browserandMobiles()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

        }

        public ActionResult sitespeed()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult googleadwords()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult socials()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }
        public ActionResult seocampaign()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }


        public ActionResult videocampaign()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }
        public ActionResult youtubechecker()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult linkanalysis() {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }
        public ActionResult websiteReview() {

            if (Session["User"] != null)
            {

                return View();
            }
            else {
                return RedirectToAction("Index", "Index");
            }
        }
        public ActionResult serps() {

            if (Session["User"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }
        public ActionResult usersetting(){

            if (Session["User"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }
	}
}