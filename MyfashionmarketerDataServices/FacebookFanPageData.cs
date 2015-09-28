using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Myfashion.Domain;
//using SocioboardDataServices.Helper;
using System.Web.Script.Serialization;
using System.Threading;
using BaseLib;

namespace SocioboardDataServices
{
    class FacebookFanPageData : ISocialSiteData
    {
        CustomMultithreads mt_AllParsersReader = null;
        static BaseLib.CustomMultithreadsManager processThreadManagerAllParsersREader = null;
       
        public FacebookFanPageData()
        {

            GetProcessManagerFromDictionary();

            mt_AllParsersReader = processThreadManagerAllParsersREader.GetCustomMultithreadsInstance(GetFbPost, 2, 20, "DirectUrlParser");
            //mt_AllParsersReader.processCompletedEvent.processCompletedEvent += new EventHandler(processCompletedEvent_processCompletedEventReader);

        }

        public static void GetProcessManagerFromDictionary()
        {
            try
            {
                CustomMultithreadManagerDictionary.dictionary_CustomMultithreadsManager.TryGetValue("DirectUrlParser", out processThreadManagerAllParsersREader);
                processThreadManagerAllParsersREader.poolName = "DirectUrlParser";
            }
            catch (Exception ex)
            {
            }
        }

        public string GetData(object userId, string profileId)
        {
            string ret = string.Empty;
            try
            {
                Guid UserId = (Guid)userId;
                //Api.FacebookFeed.FacebookFeed ApiObjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                //ApiObjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(userId.ToString(), profileId);

                Api.User.User _User = new Api.User.User();

                Domain.Myfashion.Domain.User _AcUser = new User();
                //_AcUser = (Domain.Myfashion.Domain.User)(new JavaScriptSerializer().Deserialize(_User.getUserInfoByEmail("navanandakishore@globussoft.com"), typeof(Domain.Myfashion.Domain.User)));

                Api.FacebookAccount.FacebookAccount ApiObjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                List<Domain.Myfashion.Domain.FacebookAccount> lstFacebookAccounts = (List<Domain.Myfashion.Domain.FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiObjFacebookAccount.GetFacebookAccountByUserId(UserId.ToString()), typeof(List<Domain.Myfashion.Domain.FacebookAccount>)));
                //List<Domain.Myfashion.Domain.FacebookAccount> lstFacebookAccounts = (List<Domain.Myfashion.Domain.FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiObjFacebookAccount.GetFacebookAccountByUserId(_AcUser.Id.ToString()), typeof(List<Domain.Myfashion.Domain.FacebookAccount>)));


                foreach (FacebookAccount itemFb in lstFacebookAccounts)
                {
                    
                    try
                    {
                        if ((itemFb.Type == "Page" || itemFb.Type == "page") && (string.IsNullOrEmpty(itemFb.AccessToken)))
                        {
                            GetFbPost(itemFb);
                            //processThreadManagerAllParsersREader.CallMultithreadedMethod(mt_AllParsersReader, itemFb);
                        }
                        else if ((itemFb.Type == "Page" || itemFb.Type == "page") && (!string.IsNullOrEmpty(itemFb.AccessToken))) 
                        {
                            GetFbPageData(ret, itemFb);
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

        private static string GetFbPageData(string ret, FacebookAccount itemFb)
        {
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            ret = ApiobjFacebook.GetFacebookPageData(itemFb.FbUserId, itemFb.UserId.ToString());
            return ret;
        }

        private static string GetFbPost(object objFacebookAccount)
        {
            FacebookAccount itemFb = (FacebookAccount)objFacebookAccount;
            clsFacebookDataScraper objFbDataScraper = new clsFacebookDataScraper();
            objFbDataScraper.GetFbPost(itemFb.UserId.ToString(), itemFb.FbUserId);

            return "";
        }


        public void GetSearchData(object parameters)
        {
            throw new NotImplementedException();
        }
    }
}
