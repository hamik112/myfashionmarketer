﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Myfashion.Domain;

namespace Api.Myfashionmarketer.Models
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class LinkedInFeed : System.Web.Services.WebService
    {
        LinkedInFeedRepository objLinkedInFeedRepository = new LinkedInFeedRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeeds(string UserId, string LinkedInId)
        {
             List<Domain.Myfashion.Domain.LinkedInFeed> lstlinkedinfeeds=new List<Domain.Myfashion.Domain.LinkedInFeed> ();
            try
            {
                if (objLinkedInFeedRepository.checkLinkedInUserExists(LinkedInId, Guid.Parse(UserId)))
                {
                    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfUser(Guid.Parse(UserId), LinkedInId);
                }
                else
                {
                    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInUserFeeds(LinkedInId);
                }
                return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeedsByUserIdAndProfileIdUsingLimit(string UserId, string LinkedInId, string noOfDataToSkip, string noOfResultsFromTop)
        {
            List<Domain.Myfashion.Domain.LinkedInFeed> lstlinkedinfeeds = new List<Domain.Myfashion.Domain.LinkedInFeed>();
            try
            {
                if (objLinkedInFeedRepository.checkLinkedInUserExists(LinkedInId, Guid.Parse(UserId)))
                {
                    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfUserOfSBUserWithRangeAndProfileId(Guid.Parse(UserId), LinkedInId, noOfDataToSkip, noOfResultsFromTop);
                }
                else
                {
                    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfUserOfSBUserWithRangeByProfileId(LinkedInId, noOfDataToSkip, noOfResultsFromTop);
                }
                return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeeds1(string UserId, string LinkedInId, int count)
        {
            try
            {
                List<Domain.Myfashion.Domain.LinkedInFeed> lstlinkedinfeeds =objLinkedInFeedRepository.getAllLinkedInFeedsOfUser(Guid.Parse(UserId), LinkedInId, count);
                return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllLinkedInFeedsOfProfileWithId(string ProfileId, string Id)
        {
            List<Domain.Myfashion.Domain.LinkedInFeed> lstlinkedinfeeds = new List<Domain.Myfashion.Domain.LinkedInFeed>();
            try
            {
                lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfProfileWithId(ProfileId, Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
        }
    }
}
