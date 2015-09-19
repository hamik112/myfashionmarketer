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
    public class UserPackageRelation : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public static void AddUserPackageRelation(Domain.Myfashion.Domain.User user)
        {
            Domain.Myfashion.Domain.UserPackageRelation objUserPackageRelation = new Domain.Myfashion.Domain.UserPackageRelation();
            UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
            PackageRepository objPackageRepository = new PackageRepository();

            Domain.Myfashion.Domain.Package objPackage = objPackageRepository.getPackageDetails(user.AccountType);
            objUserPackageRelation.Id = new Guid();
            objUserPackageRelation.PackageId = objPackage.Id;
            objUserPackageRelation.UserId = user.Id;
            objUserPackageRelation.PackageStatus = true;

            objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);
        }
    }
}
