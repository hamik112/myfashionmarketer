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
    /// Summary description for Team
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Team : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static void AddTeamByGroupIdUserId(Guid userId, string userEmailId, Guid groupId)
        {
            Domain.Myfashion.Domain.Team teams = new Domain.Myfashion.Domain.Team();
            TeamRepository objTeamRepository = new TeamRepository();

            teams.Id = Guid.NewGuid();
            teams.GroupId = groupId;
            teams.UserId = userId;
            teams.EmailId = userEmailId;
            teams.InviteStatus = 1;
            objTeamRepository.addNewTeam(teams);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTeam(string userid, string teamid, string UserName)
        {
            Domain.Myfashion.Domain.Team team = new Domain.Myfashion.Domain.Team();
            TeamRepository teamrepo = new TeamRepository();
            try
            {
                string[] fnamelname = UserName.Split(' ');
                string fname = fnamelname[0];
                string lname = string.Empty;
                for (int i = 1; i < fnamelname.Length; i++)
                {
                    lname += fnamelname[i];
                }
               
                team.Id = Guid.Parse(teamid);
                team.UserId = Guid.Parse(userid);
                team.FirstName = fname;
                team.LastName = lname;
                team.StatusUpdateDate = DateTime.Now;
                team.InviteStatus = 1;
                teamrepo.updateTeam(team);
                User objUser = new Services.User();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
            return new JavaScriptSerializer().Serialize(team);

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamByGroupId(string GroupId)
        {
            try
            {
                TeamRepository teamrepo = new TeamRepository();
                Domain.Myfashion.Domain.Team objTeam = teamrepo.GetTeamByGroupId(Guid.Parse(GroupId));
                return new JavaScriptSerializer().Serialize(objTeam);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamByUserId(string userid)
        {
            try
            {
                TeamRepository teamrepo = new TeamRepository();
                GroupsRepository objGroupsRepository = new GroupsRepository();
                List<Domain.Myfashion.Domain.Team> lstTeam = teamrepo.GetTeamByUserid(Guid.Parse(userid));
                List<Domain.Myfashion.Domain.Groups> lstGroups = new List<Domain.Myfashion.Domain.Groups>();
                foreach (var item in lstTeam)
                {
                    lstGroups.Add(objGroupsRepository.getGroupName(item.GroupId));
                }

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
        public string getAllTeamsOfUser(string UserId, string groupId, string emailId)
        {
            TeamRepository teamrepo = new TeamRepository();
            List<Domain.Myfashion.Domain.Team> lstTeam = new List<Domain.Myfashion.Domain.Team>();
            lstTeam = teamrepo.getAllTeamsOfUser(Guid.Parse(UserId), Guid.Parse(groupId), emailId);

            return new JavaScriptSerializer().Serialize(lstTeam);
        }

    }
}
