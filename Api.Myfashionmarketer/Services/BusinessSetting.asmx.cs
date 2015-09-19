using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Myfashionmarketer.Services
{
    /// <summary>
    /// Summary description for BusinessSetting
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BusinessSetting : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static string AddBusinessSetting(Guid userId, Guid groupsId, string groupsGroupName)
        {
            Domain.Myfashion.Domain.BusinessSetting objbsnssetting = new Domain.Myfashion.Domain.BusinessSetting();
            BusinessSettingRepository busnrepo = new BusinessSettingRepository();

            if (!busnrepo.checkBusinessExists(userId, groupsGroupName))
            {
                objbsnssetting.Id = Guid.NewGuid();
                objbsnssetting.BusinessName = groupsGroupName;
                objbsnssetting.GroupId = groupsId;
                objbsnssetting.AssigningTasks = false;
                objbsnssetting.AssigningTasks = false;
                objbsnssetting.TaskNotification = false;
                objbsnssetting.TaskNotification = false;
                objbsnssetting.FbPhotoUpload = 0;
                objbsnssetting.UserId = userId;
                objbsnssetting.EntryDate = DateTime.Now;
                busnrepo.AddBusinessSetting(objbsnssetting);

                return new JavaScriptSerializer().Serialize(objbsnssetting);
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddBusinessByUser(string ObjBusinessSetting)
        {
            Domain.Myfashion.Domain.BusinessSetting objbsnssetting = (Domain.Myfashion.Domain.BusinessSetting)(new JavaScriptSerializer().Deserialize(ObjBusinessSetting, typeof(Domain.Myfashion.Domain.BusinessSetting)));
            BusinessSettingRepository busnrepo = new BusinessSettingRepository();


            busnrepo.AddBusinessSetting(objbsnssetting);

            return new JavaScriptSerializer().Serialize(objbsnssetting);

        }

        [WebMethod]
        //IsNotificationTaskEnable
        public string IsNotificationTaskEnable(Guid groupsId)
        {
            Domain.Myfashion.Domain.BusinessSetting objbsnssetting = new Domain.Myfashion.Domain.BusinessSetting();
            BusinessSettingRepository busnrepo = new BusinessSettingRepository();
            objbsnssetting = busnrepo.IsNotificationTaskEnable(groupsId);
            return new JavaScriptSerializer().Serialize(objbsnssetting);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetDetailsofBusinessOwner(string GroupId)
        {
            BusinessSettingRepository busnrepo = new BusinessSettingRepository();

            Domain.Myfashion.Domain.BusinessSetting ObjBsnsstng = busnrepo.GetDetailsofBusinessOwner(Guid.Parse(GroupId));
            try
            {
                if (ObjBsnsstng != null)
                {
                    return new JavaScriptSerializer().Serialize(ObjBsnsstng);
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(ObjBsnsstng);
                }


            }
            catch (Exception ex)
            {

                throw;
                return null;
            }

        }
    }
}
