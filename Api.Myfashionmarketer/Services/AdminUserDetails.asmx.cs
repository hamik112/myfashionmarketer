using Api.Socioboard.Model;
using Domain.Myfashion.Domain;
using log4net;
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
    /// Summary description for AdminUserDetails
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminUserDetails : System.Web.Services.WebService
    {
        UserRepository objUserRepo = new UserRepository();
        
        ILog logger = LogManager.GetLogger(typeof(Admin));
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllUsers(string Objuser)
        {
            try
            {
                Domain.Myfashion.Domain.User ObjUser = (Domain.Myfashion.Domain.User)(new JavaScriptSerializer().Deserialize(Objuser, typeof(Domain.Myfashion.Domain.User)));

                if (ObjUser.UserType == "SuperAdmin")
                {

                    List<Domain.Myfashion.Domain.User> lstUser = objUserRepo.getAllUsersByAdmin();
                    if (lstUser != null)
                    {
                        return new JavaScriptSerializer().Serialize(lstUser);
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize("Not User Found");
                    }
                }
                else
                {
                    return new JavaScriptSerializer().Serialize("You have no Authentication to call this method!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetUserById(string Id)
        {
            try
            {
                Domain.Myfashion.Domain.User user = new Domain.Myfashion.Domain.User();
                user = objUserRepo.getUsersById(Guid.Parse(Id));
                return new JavaScriptSerializer().Serialize(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }


        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateUserAccount(string Id, string UserName, string EmailId, string Package, string Status)
        {
            try
            {
                Domain.Myfashion.Domain.User objUser = new Domain.Myfashion.Domain.User();
                objUser.Id = Guid.Parse(Id);
                objUser.UserName = UserName;
                objUser.EmailId = EmailId;
                objUser.AccountType = Package;
                objUser.UserStatus = Convert.ToInt16(Status);
                UserRepository.UpdateAccountType(objUser);
                return new JavaScriptSerializer().Serialize("Update Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteUser(string Id)
        {
                      
            try 
	        {
                int delete = objUserRepo.DeleteUserByAdmin(Guid.Parse(Id));
                return new JavaScriptSerializer().Serialize(delete);
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize(0);
	        }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllDeletedUsers(string Objuser)
        {

            try
            {

                 Domain.Myfashion.Domain.User ObjUser = (Domain.Myfashion.Domain.User)(new JavaScriptSerializer().Deserialize(Objuser, typeof(Domain.Myfashion.Domain.User)));

                 if (ObjUser.UserType == "SuperAdmin")
                 {
                     List<Domain.Myfashion.Domain.User> lstUser = objUserRepo.getAllDeletedUsersByAdmin();
                     return new JavaScriptSerializer().Serialize(lstUser);
                 }
                 else
                 {
                     return new JavaScriptSerializer().Serialize("You have no Authentication to call this method!");
                 }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("No Record Exist");
            }
        }
    }
}
