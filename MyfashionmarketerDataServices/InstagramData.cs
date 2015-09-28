using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Myfashion.Domain;
using System.Collections;
using System.Configuration;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    public class InstagramData : ISocialSiteData
    {
        public string GetData(object objUserId, string instagramid)
        {
            string ret = string.Empty;
            try
            {
                Guid UserId = (Guid)objUserId;
                Api.InstagramAccount.InstagramAccount ApiObjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                List<Domain.Myfashion.Domain.InstagramAccount> lstInstagramAccount = (List<Domain.Myfashion.Domain.InstagramAccount>)(new JavaScriptSerializer().Deserialize(ApiObjInstagramAccount.GetAllInstagramAccounts(UserId.ToString()), typeof(List<Domain.Myfashion.Domain.InstagramAccount>)));
                foreach (InstagramAccount item in lstInstagramAccount)
                {
                    try
                    {
                        Api.Instagram.Instagram apiObjInstagram = new Api.Instagram.Instagram();
                        ret = apiObjInstagram.getInstagramData(item.UserId.ToString(), item.InstagramId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
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
            throw new NotImplementedException();
        }

    }
}
