﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Myfashion.Domain;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    public class TumblrData : ISocialSiteData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId">Tumblr User id</param>
        public string GetData(object UserId,string TumblrId)
        {
            string ret = string.Empty;
            try
            {
                Guid userId = (Guid)UserId;
                TumblrFeed objTumblrFeed = new TumblrFeed();
                Api.TumblrFeed.TumblrFeed ApiObjTumblrFeed = new Api.TumblrFeed.TumblrFeed();
                Api.TumblrAccount.TumblrAccount ApiObjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                List<Domain.Myfashion.Domain.TumblrAccount> lstTumblrAccount = (List<Domain.Myfashion.Domain.TumblrAccount>)(new JavaScriptSerializer().Deserialize(ApiObjTumblrAccount.GetAllTumblrAccountsOfUser(userId.ToString()), typeof(List<Domain.Myfashion.Domain.TumblrAccount>)));
                foreach (TumblrAccount lstItem in lstTumblrAccount)
                {
                    try
                    {
                        Api.Tumblr.Tumblr ApiObjTumblr = new Api.Tumblr.Tumblr();
                        ret = ApiObjTumblr.getTumblrData(lstItem.UserId.ToString(), lstItem.tblrUserName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
