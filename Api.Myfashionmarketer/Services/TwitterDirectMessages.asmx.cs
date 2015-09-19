using Api.Myfashionmarketer.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

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
    public class TwitterDirectMessages : System.Web.Services.WebService
    {
        TwitterDirectMessageRepository objTwitterDirectMessageRepository = new TwitterDirectMessageRepository();


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //getAllDirectMessagesById
        [WebMethod]
        public string getAllDirectMessagesById(string Profileid)
        {
            List<Domain.Myfashion.Domain.TwitterDirectMessages> lsttwtmsg = objTwitterDirectMessageRepository.getAllDirectMessagesById(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }
        [WebMethod]
        public string GetDistinctTwitterDirectMessagesByProfilesAndUserId(string UserId, string Profiles)
        {
            List<Domain.Myfashion.Domain.TwitterDirectMessages> lstTDM = objTwitterDirectMessageRepository.GetDistinctTwitterDirectMessagesByProfilesAndUserId(Guid.Parse(UserId), Profiles);
            return new JavaScriptSerializer().Serialize(lstTDM);
        }
        [WebMethod]
        public string GetConversation(string UserId, string SenderId, string RecipientId)
        {
            List<Domain.Myfashion.Domain.TwitterDirectMessages> lstTDM = objTwitterDirectMessageRepository.GetConversation(Guid.Parse(UserId), SenderId, RecipientId);
            return new JavaScriptSerializer().Serialize(lstTDM);
        }

    }
}
