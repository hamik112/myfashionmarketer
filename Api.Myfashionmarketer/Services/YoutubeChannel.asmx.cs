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

    public class YoutubeChannel : System.Web.Services.WebService
    {
        YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllYoutubeChannelByUserIdAndProfileId(string UserId, string ProfileId)
        {
             Domain.Myfashion.Domain.YoutubeChannel lstYoutubeChannel=new Domain.Myfashion.Domain.YoutubeChannel ();
            try
            {
                if (objYoutubeChannelRepository.checkYoutubeChannelExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstYoutubeChannel = objYoutubeChannelRepository.getYoutubeChannelDetailsById(ProfileId, Guid.Parse(UserId));
                }
                else
                {
                    lstYoutubeChannel = objYoutubeChannelRepository.getYoutubeChannelDetailsById(ProfileId);
                }

                return new JavaScriptSerializer().Serialize(lstYoutubeChannel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        
    }
}
