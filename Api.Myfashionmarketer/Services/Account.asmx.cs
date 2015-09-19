using Api.Myfashionmarketer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Myfashion.Domain;

namespace Api.Myfashionmarketer.Services
{
    /// <summary>
    /// Summary description for Account
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Account : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static void AddAccountByUserId(Guid userId, string Business_name, Guid Company_id, string EmailId)
        {
            Domain.Myfashion.Domain.Account account = new Domain.Myfashion.Domain.Account();
            account.User_id = userId;
            account.Company_id = Company_id;
            account.EmailId = EmailId;
            account.Business_name = Business_name;
            AccountRepository.Add(account);

        }
    }
}
