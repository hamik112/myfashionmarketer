using Api.Myfashionmarketer.Models;
using log4net;
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
    /// Summary description for Groups
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Groups : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(Groups));
        Domain.Myfashion.Domain.Groups group = new Domain.Myfashion.Domain.Groups();
        GroupsRepository grouprepo = new GroupsRepository();
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        UserRepository objUserRepository = new UserRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddGroup(string GroupName, string UserId)
        {
            try
            {
                // GroupRepository grouprepo = new GroupRepository();
                if (!grouprepo.checkGroupExists(Guid.Parse(UserId), GroupName))
                {
                    // Domain.Socioboard.Domain.Groups group = new Domain.Socioboard.Domain.Groups();
                    group.GroupName = GroupName;
                    group.UserId = Guid.Parse(UserId);
                    group.EntryDate = DateTime.Now;
                    group.Id = Guid.NewGuid();
                    grouprepo.AddGroup(group);
                    Domain.Myfashion.Domain.User objUser = objUserRepository.getUsersById(Guid.Parse(UserId));
                    Team.AddTeamByGroupIdUserId(objUser.Id, objUser.EmailId, group.Id);
                    BusinessSetting ApiobjBusinesssSetting = new BusinessSetting();
                    Domain.Myfashion.Domain.BusinessSetting ObjBsnsStng = new Domain.Myfashion.Domain.BusinessSetting();
                    ObjBsnsStng.Id = Guid.NewGuid();
                    ObjBsnsStng.BusinessName = GroupName;
                    ObjBsnsStng.GroupId = group.Id;
                    ObjBsnsStng.AssigningTasks = false;
                    ObjBsnsStng.TaskNotification = false;
                    ObjBsnsStng.FbPhotoUpload = 0;
                    ObjBsnsStng.UserId = Guid.Parse(UserId);
                    ObjBsnsStng.EntryDate = DateTime.Now;
                    string ObjBsnsStg = (new JavaScriptSerializer().Serialize(ObjBsnsStng));
                    string BsnsMessage = ApiobjBusinesssSetting.AddBusinessByUser(ObjBsnsStg);
                    return new JavaScriptSerializer().Serialize(group);
                }
                else
                {
                    return "Group Already Exists";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGroupDetailsByGroupId(string GroupId)
        {
            logger.Error("GetGroupDetailsByGroupId inside function");
            try
            {
                Domain.Myfashion.Domain.Groups objGroups = grouprepo.getGroupName(Guid.Parse(GroupId));
                return new JavaScriptSerializer().Serialize(objGroups);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGroupDetailsByUserId(string UserId)
        {
            try
            {
                List<Domain.Myfashion.Domain.Groups> lstGroups = grouprepo.getAllGroups(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstGroups);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteGroupByUserId(string UserId)
        {
            try
            {
                int i = grouprepo.DeleteGroup(Guid.Parse(UserId));
                if (i == 1)
                {
                    return "Group Deleted Successfully";
                }
                else
                {
                    return "Invalid UserId";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteGroupByName(string UserId, string GroupName)
        {
            try
            {
                group.UserId = Guid.Parse(UserId);
                group.GroupName = GroupName;

                int i = grouprepo.DeleteGroup(group);
                if (i == 1)
                {
                    return "Group Deleted Successfully";
                }
                else
                {
                    return "Invalid UserId";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteGroupById(string GroupId, string Userid)
        {
            try
            {

                objGroupProfileRepository.DeleteAllGroupProfile(Guid.Parse(GroupId));

                grouprepo.DeleteGroup(Guid.Parse(GroupId));
                List<Domain.Myfashion.Domain.Team> lstTeam = objTeamRepository.GetAllTeamExcludeUser(Guid.Parse(GroupId), Guid.Parse(Userid));
                foreach (var item in lstTeam)
                {
                    objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamId(item.Id);
                }

                // int i = grouprepo.DeleteGroup(Guid.Parse(GroupId));
                //if (i == 1)
                //{
                //    return "Group Deleted Successfully";
                //}
                //else
                //{
                //    return "Invalid UserId";
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
            return "";
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGroupDeUserId(string UserId)
        {
            try
            {
                List<Domain.Myfashion.Domain.Groups> lstGroups = grouprepo.getAllGroups(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstGroups[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
    }
}
