﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Myfashionmarketer.Models
{
    /// <summary>
    /// Summary description for FacebookFeed
    /// </summary>
   
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    
    [ScriptService]
    public class FacebookFeed : System.Web.Services.WebService
    {
        FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
        
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileId(string UserId, string ProfileId)
        {
             List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed=new List<Domain.Myfashion.Domain.FacebookFeed> ();
            try
            {
                if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeeds(Guid.Parse(UserId), ProfileId);
                }
                else
                {
                     lstFacebookFeed = objFacebookFeedRepository.getAllFacebookUserFeeds(ProfileId);
                }
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileIdUsingLimit(string UserId, string ProfileId, string noOfDataToSkip, string noOfResultsFromTop)
        {
            List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = new List<Domain.Myfashion.Domain.FacebookFeed>();
            try
            {
                if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithRangeAndProfileId(UserId, ProfileId, noOfDataToSkip, noOfResultsFromTop);
                }
                else
                {
                    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithRangeByProfileId(ProfileId, noOfDataToSkip, noOfResultsFromTop);
                }
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileId1(string UserId, string ProfileId, int count)
        {
            try
            {
                List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeeds(Guid.Parse(UserId), ProfileId, count);
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUnreadMessages(string ProfileId)
        {
            try
            {
                List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = FacebookFeedRepository.getUnreadMessages(ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllReadFbFeeds(string ProfileId)
        {
            try
            {
                List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllReadFbFeeds(ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getAllFeedDetail
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFeedDetail(string ProfileId)
        {
            try
            {
                List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFeedDetail(ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFeedDetail1(string ProfileId, string userid)
        {
            try
            {
                List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFeedDetail1(ProfileId, Guid.Parse(userid));
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookFeedByFeedId(string userid, string feedid)
        {
            try
            {
                Domain.Myfashion.Domain.FacebookFeed _FacebookFeed = objFacebookFeedRepository.GetFacebookFeedByFeedId(Guid.Parse(userid), feedid);
                return new JavaScriptSerializer().Serialize(_FacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getFacebookFeedByProfileId(string ProfileId, string FeedId)
        {
            try
            {
                Domain.Myfashion.Domain.FacebookFeed facebookfeed = objFacebookFeedRepository.getFacebookFeedByProfileId(ProfileId, FeedId);
                return new JavaScriptSerializer().Serialize(facebookfeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }


        //Added by Sumit Gupta [12-02-15]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdWithRange(string UserId, string keyword, string noOfDataToSkip)
        {
            List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = new List<Domain.Myfashion.Domain.FacebookFeed>();
            try
            {
                //if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithRange(UserId, keyword, noOfDataToSkip);
                }
                //else
                //{
                //    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookUserFeeds(ProfileId);
                //}
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getAllFacebookFeedsByUserIdAndProfileId1
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileId1WithRange(string UserId, string keyword, string ProfileId, string count)
        {
            try
            {
                List<Domain.Myfashion.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithProfileIdAndRange(UserId, ProfileId, keyword, count);
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int GetFeedCountByProfileIdAndUserId(string UserId, string ProfileIds)
        {
            try 
            {
                return objFacebookFeedRepository.GetFeedCountByProfileIdAndUserId(Guid.Parse(UserId), ProfileIds);
            }catch(Exception ex){
                return 0;
            }
        }

    }
}
