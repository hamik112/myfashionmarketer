using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Myfashion.Domain;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    public class FacebookPageData : ISocialSiteData
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
            throw new NotImplementedException();
        }
    }
}
