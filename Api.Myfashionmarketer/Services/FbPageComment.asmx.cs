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
    public class FbPageComment : System.Web.Services.WebService
    {
        FbPageCommentRepository objFbPageCommentRepository = new FbPageCommentRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetPostComments(string postid)
        {
            try
            {
                List<Domain.Myfashion.Domain.FbPageComment> lstfbmsgs = objFbPageCommentRepository.GetPostComments(postid);
                return new JavaScriptSerializer().Serialize(lstfbmsgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool AddCommentDetails(string Jdata)
        {
            try
            {
                Domain.Myfashion.Domain.FbPageComment _FbPageComment = (Domain.Myfashion.Domain.FbPageComment)new JavaScriptSerializer().Deserialize(Jdata, typeof(Domain.Myfashion.Domain.FbPageComment));
                objFbPageCommentRepository.addFbPageComment(_FbPageComment);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool IsCommentExist(string Jdata)
        {
            Domain.Myfashion.Domain.FbPageComment _FbPageComment = (Domain.Myfashion.Domain.FbPageComment)new JavaScriptSerializer().Deserialize(Jdata, typeof(Domain.Myfashion.Domain.FbPageComment));
            return objFbPageCommentRepository.IsPostCommentExist(_FbPageComment);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int UpdateFbPageCommentStatus(string Jdata)
        {
            Domain.Myfashion.Domain.FbPageComment _FbPageComment = (Domain.Myfashion.Domain.FbPageComment)new JavaScriptSerializer().Deserialize(Jdata, typeof(Domain.Myfashion.Domain.FbPageComment));
            int i = objFbPageCommentRepository.UpdateFbPageCommentStatus(_FbPageComment);
            return i;
        }

    }
}
