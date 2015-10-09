using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Web.Script.Serialization;


namespace WebSiteReviewData
{
    public class WebSiteDataData : ISocialSiteData
    {

        public string UpdateWebSiteData(string Id, string Url)
        {
            string ret = string.Empty;
            try
            {
                    try
                    {
                        Api.WebSiteReview.WebSiteReview objWebSiteReview = new Api.WebSiteReview.WebSiteReview();
                        objWebSiteReview.Timeout = 5000;
                        ret = objWebSiteReview.UpdateWebSiteData(Id,Url);
                        //ret = ApiObjTwitter.getTwitterDataWithPagination(itemTwt.UserId.ToString(), itemTwt.TwitterUserId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            return ret;
        }


        public void GetSearchData(object parameters)
        {
            //#region Twitter

            //Array arrayParams = (Array)parameters;

            //DiscoverySearch dissearch = (DiscoverySearch)arrayParams.GetValue(0);
            //DiscoverySearchRepository dissearchrepo=(DiscoverySearchRepository)arrayParams.GetValue(1);
            //DiscoverySearch discoverySearch = (DiscoverySearch)arrayParams.GetValue(2);

            //oAuthTwitter oauth = new oAuthTwitter();
            //oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
            //oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
            //oauth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"].ToString();


            //TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
            ////ArrayList alst = twtAccRepo.getAllTwitterAccounts();
            //ArrayList alst = twtAccRepo.getAllTwitterAccountsOfUser(discoverySearch.UserId);
            //foreach (TwitterAccount item in alst)
            //{
            //    oauth.AccessToken = item.OAuthToken;
            //    oauth.AccessTokenSecret = item.OAuthSecret;
            //    oauth.TwitterUserId = item.TwitterUserId;
            //    oauth.TwitterScreenName = item.TwitterScreenName;
            //    if (TwitterHelper.CheckTwitterToken(oauth, discoverySearch.SearchKeyword))
            //    {
            //        break;
            //    }
            //    else
            //    {

            //    }


            //}

            //GlobusTwitterLib.Twitter.Core.SearchMethods.Search search = new GlobusTwitterLib.Twitter.Core.SearchMethods.Search();
            //Newtonsoft.Json.Linq.JArray twitterSearchResult = search.Get_Search_Tweets(oauth, discoverySearch.SearchKeyword);

            //foreach (var item in twitterSearchResult)
            //{
            //    var results = item["statuses"];

            //    foreach (var chile in results)
            //    {
            //        try
            //        {
            //            dissearch.CreatedTime = SocioBoard.Helper.Extensions.ParseTwitterTime(chile["created_at"].ToString().TrimStart('"').TrimEnd('"')); ;
            //            dissearch.EntryDate = DateTime.Now;
            //            dissearch.FromId = chile["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.FromName = chile["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.ProfileImageUrl = chile["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.SearchKeyword = discoverySearch.SearchKeyword;
            //            dissearch.Network = "twitter";
            //            dissearch.Message = chile["text"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.MessageId = chile["id_str"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.Id = Guid.NewGuid();
            //            dissearch.UserId = discoverySearch.UserId;//user.Id;


            //            if (!dissearchrepo.isKeywordPresent(dissearch.SearchKeyword, dissearch.MessageId))
            //            {
            //                dissearchrepo.addNewSearchResult(dissearch);
            //            }


            //        }
            //        catch (Exception ex)
            //        {
            //            //logger.Error(ex.StackTrace);
            //            Console.WriteLine(ex.StackTrace);
            //        }
            //    }
            //}
            //#endregion
        }



        //public string GetData(object UserId, string profileid)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
