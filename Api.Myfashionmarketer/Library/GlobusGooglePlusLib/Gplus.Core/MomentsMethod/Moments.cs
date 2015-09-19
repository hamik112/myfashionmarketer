﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.Authentication;
using GlobusGooglePlusLib.App.Core;

namespace GlobusGooglePlusLib.Gplus.Core.MomentsMethod
{
    public class Moments
    {
        /// <summary>
        /// Record a moment representing a user's activity such as making a purchase or commenting on a blog
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Post_Insert_Moment(string UserId, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strMoments + UserId + "/moments/vault?access_token=" + access;
            string response = string.Empty;
            try
            {
                response = objToken.WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.POST,RequestUrl,"");
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return JArray.Parse(response);
        }

        /// <summary>
        /// List all of the moments that your app has written for the authenticated user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Get_Moment_List(string UserId, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strMoments + UserId + "/moments/vault?access_token=" + access;
            string response = string.Empty;
            try
            {
                response = objToken.WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.GET, RequestUrl, "");
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return JArray.Parse(response);
        }

        /// <summary>
        /// Delete a moment that your app has written for the authenticated user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Remove_Moment(string UserId, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strRemoveMoments + UserId + "?access_token=" + access;
            string response = string.Empty;
            try
            {
                response = objToken.WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.DELETE, RequestUrl, "");
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return JArray.Parse(response);
        }
    }
}
