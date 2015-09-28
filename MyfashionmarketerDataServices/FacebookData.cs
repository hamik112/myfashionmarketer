﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Myfashion.Domain;
//using SocioboardDataServices.Helper;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    public class FacebookData : ISocialSiteData
    {

        public string GetData(object userId, string profileId)
        {
            string ret = string.Empty;
            try
            {
                Guid UserId = (Guid)userId;
                Api.FacebookFeed.FacebookFeed ApiObjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                ApiObjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(userId.ToString(), profileId);
                Api.FacebookAccount.FacebookAccount ApiObjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                List<Domain.Myfashion.Domain.FacebookAccount> lstFacebookAccounts = (List<Domain.Myfashion.Domain.FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiObjFacebookAccount.GetFacebookAccountByUserId(UserId.ToString()), typeof(List<Domain.Myfashion.Domain.FacebookAccount>)));
                //FacebookHelper fbhelper = new FacebookHelper();

                foreach (FacebookAccount itemFb in lstFacebookAccounts)
                {
                    //FacebookHelper objFbHelper = new FacebookHelper();
                    try
                    {
                        //Facebook profile data
                        if (!string.IsNullOrEmpty(itemFb.AccessToken))
                        {
                            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                            ret = ApiobjFacebook.GetFacebookData(itemFb.FbUserId, itemFb.UserId.ToString());

                            //Add Facebook Stats
                            Api.FacebookStats.FacebookStats _FacebookStats = new Api.FacebookStats.FacebookStats();
                            bool abc = _FacebookStats.AddFacebookFriendsGender(itemFb.UserId.ToString(), itemFb.FbUserId);

                            Console.WriteLine(ret);
                        }

                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;
        }


        public string GetDataSirAccount(object userId, string profileId)
        {
            string ret = string.Empty;
            try
            {
                Guid UserId = (Guid)userId;
                Api.FacebookFeed.FacebookFeed ApiObjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                ApiObjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(userId.ToString(), profileId);
                Api.FacebookAccount.FacebookAccount ApiObjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                List<Domain.Myfashion.Domain.FacebookAccount> lstFacebookAccounts = (List<Domain.Myfashion.Domain.FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiObjFacebookAccount.GetFacebookAccountByUserId(UserId.ToString()), typeof(List<Domain.Myfashion.Domain.FacebookAccount>)));
                //FacebookHelper fbhelper = new FacebookHelper();

                foreach (FacebookAccount itemFb in lstFacebookAccounts)
                {
                    //FacebookHelper objFbHelper = new FacebookHelper();
                    try
                    {
                        //Facebook profile data
                        if (!string.IsNullOrEmpty(itemFb.AccessToken))
                        {
                            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                            ret = ApiobjFacebook.GetFacebookData(itemFb.FbUserId, itemFb.UserId.ToString());


                            //Add Facebook Stats
                            Api.FacebookStats.FacebookStats _FacebookStats = new Api.FacebookStats.FacebookStats();
                            bool abc = _FacebookStats.AddFacebookFriendsGender(itemFb.UserId.ToString(), itemFb.FbUserId);
                        }

                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;
        }

      
        
        public void GetSearchData(object parameters)
        {
            #region Facebook
            //try
            //{
            //    Array arrayParams = (Array)parameters;

            //    DiscoverySearch dissearch = (DiscoverySearch)arrayParams.GetValue(0);
            //    Api.DiscoverySearch.DiscoverySearch ApiObjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();

            //    DiscoverySearch discoverySearch = (DiscoverySearch)arrayParams.GetValue(2);


            //    #region FacebookSearch

            //    string accesstoken = string.Empty;

            //    FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
            //    //ArrayList asltFbAccount = fbAccRepo.getAllFacebookAccounts();
            //    ArrayList asltFbAccount = fbAccRepo.getAllFacebookAccountsOfUser(discoverySearch.UserId);

            //    foreach (FacebookAccount item in asltFbAccount)
            //    {
            //        accesstoken = item.AccessToken;
            //        if (FacebookHelper.CheckFacebookToken(accesstoken, discoverySearch.SearchKeyword))
            //        {

            //            break;
            //        }
            //    }

            //    string facebookSearchUrl = "https://graph.facebook.com/search?q=" + discoverySearch.SearchKeyword + " &type=post&access_token=" + accesstoken;
            //    var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
            //    facerequest.Method = "GET";
            //    string outputface = string.Empty;
            //    using (var response = facerequest.GetResponse())
            //    {
            //        using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
            //        {
            //            outputface = stream.ReadToEnd();
            //        }
            //    }
            //    if (!outputface.StartsWith("["))
            //        outputface = "[" + outputface + "]";


            //    Newtonsoft.Json.Linq.JArray facebookSearchResult = Newtonsoft.Json.Linq.JArray.Parse(outputface);

            //    foreach (var item in facebookSearchResult)
            //    {
            //        var data = item["data"];

            //        foreach (var chile in data)
            //        {
            //            try
            //            {
            //                dissearch.CreatedTime = DateTime.Parse(chile["created_time"].ToString());

            //                dissearch.EntryDate = DateTime.Now;

            //                dissearch.FromId = chile["from"]["id"].ToString();

            //                dissearch.FromName = chile["from"]["name"].ToString();

            //                try
            //                {
            //                    dissearch.ProfileImageUrl = "http://graph.facebook.com/" + chile["from"]["id"] + "/picture?type=small";
            //                }
            //                catch { }

            //                dissearch.SearchKeyword = discoverySearch.SearchKeyword;

            //                dissearch.Network = "facebook";

            //                try
            //                {
            //                    dissearch.Message = chile["message"].ToString();
            //                }
            //                catch { }
            //                try
            //                {
            //                    dissearch.Message = chile["story"].ToString();
            //                }
            //                catch { }

            //                dissearch.MessageId = chile["id"].ToString();

            //                dissearch.Id = Guid.NewGuid();

            //                dissearch.UserId = discoverySearch.UserId;

            //                if (!dissearchrepo.isKeywordPresent(dissearch.SearchKeyword, dissearch.MessageId))
            //                {
            //                    dissearchrepo.addNewSearchResult(dissearch);
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                //logger.Error(ex.StackTrace);
            //                Console.WriteLine(ex.StackTrace);
            //            }


            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    //logger.Error(ex.StackTrace);
            //    Console.WriteLine(ex.StackTrace);
            //}
            //    #endregion

            #endregion
        }
    }
}
