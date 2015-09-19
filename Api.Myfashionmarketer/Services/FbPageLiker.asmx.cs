using Api.Myfashionmarketer.Helper;
using Api.Socioboard.Model;
using System;
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
    public class FbPageLiker : System.Web.Services.WebService
    {
        FbPageLikerRepository objFbPageLikerRepository = new FbPageLikerRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLikeByPostId(string postId,string Userid)
        {
            try
            {
                Guid userid = Guid.Parse(Userid);
                List<Domain.Myfashion.Domain.FbPageLiker> LstDraft = objFbPageLikerRepository.GetLikeByPostId(postId, userid);
                return new JavaScriptSerializer().Serialize(LstDraft);
            }
            catch (Exception)
            {
                return new JavaScriptSerializer().Serialize("No Data available.");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool IsLikeExist(string Jdata)
        {
            Domain.Myfashion.Domain.FbPageLiker _FbPageLiker = (Domain.Myfashion.Domain.FbPageLiker)new JavaScriptSerializer().Deserialize(Jdata, typeof(Domain.Myfashion.Domain.FbPageLiker));
            return objFbPageLikerRepository.IsLikeByPostExist(_FbPageLiker);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool AddLikeDetails(string Jdata)
        {
            try
            {
                Domain.Myfashion.Domain.FbPageLiker _FbPageLiker = (Domain.Myfashion.Domain.FbPageLiker)new JavaScriptSerializer().Deserialize(Jdata, typeof(Domain.Myfashion.Domain.FbPageLiker));
                objFbPageLikerRepository.addFbPageLiker(_FbPageLiker);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
