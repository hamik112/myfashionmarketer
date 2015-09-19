using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Myfashionmarketer.Models;
using System.Web.Script.Serialization;
using System.Configuration;
using Domain.Myfashion.Domain;
using Api.Myfashionmarketer.Helper;
namespace Api.Myfashionmarketer.Services
{
    /// <summary>
    /// Summary description for User
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class User : System.Web.Services.WebService
    {
        private static ILog logger = LogManager.GetLogger(typeof(User));
        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        Domain.Myfashion.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Myfashion.Domain.TeamMemberProfile();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Register(string EmailId, string Password, string AccountType, string Username,string Business_name, string ActivationStatus = "0")
        {
            UserRepository userrepo = new UserRepository();
            try
            {
                
                logger.Error("Register");

                if (!userrepo.IsUserExist(EmailId))
                {


                    Domain.Myfashion.Domain.User user = new Domain.Myfashion.Domain.User();
                    user.AccountType = AccountType;
                    user.EmailId = EmailId;
                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddDays(30);
                    user.Password = Utility.MD5Hash(Password);
                    user.PaymentStatus = "unpaid";
                    user.ProfileUrl = string.Empty;
                    user.TimeZone = string.Empty;
                    user.UserName = Username;
                    user.UserStatus = 1;
                    user.ActivationStatus = ActivationStatus;
                    user.Company = Business_name;
                    user.Id = Guid.NewGuid();
                    Guid PId =Guid.NewGuid();
                    user.PuId = PId.ToString()+Password;
                    user.Company_id = Guid.NewGuid();
                    UserRepository.Add(user);

                    try
                    {
                        Domain.Myfashion.Domain.Groups groups = AddGroupByUserId(user.Id);


                        BusinessSettingRepository busnrepo = new BusinessSettingRepository();
                        BusinessSetting.AddBusinessSetting(user.Id, groups.Id, groups.GroupName);
                        Team.AddTeamByGroupIdUserId(user.Id, user.EmailId, groups.Id);
                        Account.AddAccountByUserId(user.Id, user.Company, user.Company_id, user.EmailId);
                        UpdateTeam(EmailId, user.Id.ToString(), user.UserName);
                        try
                        {
                            Domain.Myfashion.Domain.Customers objcust = new Customers();
                            objcust.Activekeywordcount = 200;
                            objcust.Activeserpcampaignscount = 400;
                            objcust.Firstname = Username;
                            objcust.Allowedcampaignscount=200;
                            objcust.Allowedkeywordcount = 2000;
                            Customersrepository objcustrepo = new Customersrepository();
                            objcustrepo.Add(objcust);
                            int id= objcustrepo.lastRecord();
                            Domain.Myfashion.Domain.Users objusers = new Users();
                            objcust.Customerid = id;
                            objusers.Customers = objcust;
                            objusers.Loginid = EmailId;
                            objusers.Password = Password;
                            objusers.Active = 1;
                            objusers.Usertype = 0;
                            objusers.Token = "bcd213199780916aa7525d3972fa7bb15c1d1d31e955cfca5f7524b35ee5ee27_a373c25b036ac7ff";
                            
                            objusers.Addeddate = DateTime.Now;
                            UsersRepository.addUsers(objusers);

                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                            logger.Error("Error : " + ex.Message);
                            logger.Error("Error : " + ex.StackTrace);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error("Error : " + ex.Message);
                        logger.Error("Error : " + ex.StackTrace);
                    }


                    return new JavaScriptSerializer().Serialize(user);
                }
                else
                {
                    return "Email Already Exists";
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
        public string Login(string EmailId, string Password)
        {
            logger.Error("Checking Abhay123");
            try
            {
                UserRepository userrepo = new UserRepository();

                Domain.Myfashion.Domain.User objUser = new Domain.Myfashion.Domain.User();
                objUser = userrepo.getUserInfoByEmail(EmailId);
                if (objUser == null)
                {
                    return new JavaScriptSerializer().Serialize("Email Not Exist");
                }

                Domain.Myfashion.Domain.User user = userrepo.GetUserInfo(EmailId, Utility.MD5Hash(Password));
                if (user != null)
                {
                   
                    return new JavaScriptSerializer().Serialize(user);

                }
                else
                {
                    return new JavaScriptSerializer().Serialize("Not Exist");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.Message);
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }


        private static Domain.Myfashion.Domain.Groups AddGroupByUserId(Guid userId)
        {
            Domain.Myfashion.Domain.Groups groups = new Domain.Myfashion.Domain.Groups();
            GroupsRepository objGroupRepository = new GroupsRepository();

            groups.Id = Guid.NewGuid();
            groups.GroupName = ConfigurationManager.AppSettings["DefaultGroupName"];
            groups.UserId = userId;
            groups.EntryDate = DateTime.Now;

            objGroupRepository.AddGroup(groups);
            return groups;
        }

        private void UpdateTeam(string EmailId, string userid, string username)
        {
            Domain.Myfashion.Domain.Groups objGroups = objGroupsRepository.getGroupDetails(Guid.Parse(userid), ConfigurationManager.AppSettings["DefaultGroupName"]);
            List<Domain.Myfashion.Domain.Team> lstTeam = objTeamRepository.GetAllTeamOfUserEmail(EmailId, objGroups.Id);
            foreach (var team in lstTeam)
            {
                Team objTeam = new Team();
                objTeam.UpdateTeam(userid, team.Id.ToString(), username);
                AddTeamMembers(team.GroupId.ToString(), team.Id.ToString());
            }
        }

        public void AddTeamMembers(string groupid, string teamid)
        {
            List<Domain.Myfashion.Domain.GroupProfile> lstGroupProfile = objGroupProfileRepository.GetAllGroupProfiles(Guid.Parse(groupid));
            foreach (var GroupProfile in lstGroupProfile)
            {
                objTeamMemberProfile = new Domain.Myfashion.Domain.TeamMemberProfile();
                objTeamMemberProfile.Id = Guid.NewGuid();
                objTeamMemberProfile.TeamId = Guid.Parse(teamid);
                objTeamMemberProfile.ProfileId = GroupProfile.ProfileId;
                objTeamMemberProfile.ProfileType = GroupProfile.ProfileType;
                objTeamMemberProfile.Status = 1;
                objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUserInfoByEmail(string userEmail)
        {
            UserRepository userrepo = new UserRepository();
            Domain.Myfashion.Domain.User objUser = new Domain.Myfashion.Domain.User();
            objUser = userrepo.getUserInfoByEmail(userEmail);

            return new JavaScriptSerializer().Serialize(objUser);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUsersById(string UserId)
        {
            UserRepository userrepo = new UserRepository();
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            Domain.Myfashion.Domain.User objUser = new Domain.Myfashion.Domain.User();
            objUser = userrepo.getUsersById(Guid.Parse(UserId));

            return new JavaScriptSerializer().Serialize(objUser);
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdatePaymentSatus(string userid, string Accounttype)
        {
            UserRepository objuserreppo = new UserRepository();
            Domain.Myfashion.Domain.User objuser = new Domain.Myfashion.Domain.User();
            objuser.PaymentStatus = "paid";
            objuser.AccountType = Accounttype;
            objuser.ExpiryDate = DateTime.Now.AddDays(30);
            objuser.Id = Guid.Parse(userid);
            int i = objuserreppo.UpdatePaymentStatusByUserId(objuser);
            if (i == 1)
            {
                return "success";
            }
            else
            {
                return "failed";
            }
        }
    }
}
