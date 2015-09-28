using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Domain.Myfashion.Domain;
using System.Web.Script.Serialization;

namespace SocioboardDataScheduler
{
    class Program
    {

        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StartScheduler(args);

        }
        void StartScheduler(string[] args)
        {
            string check = string.Empty;
            //string[] str = { };
            try
            {
                check = args[0];
            }
            catch (Exception)
            {
                check = null;
            }
            if (string.IsNullOrEmpty(check))
            {
                Console.WriteLine("1. Facebook");
                Console.WriteLine("2. Twitter");
                Console.WriteLine("3. Linkedin");
                Console.WriteLine("4. Instagram");
                Console.WriteLine("5. Tumblr");
                Console.WriteLine("6. Facebook_Group");
                Console.WriteLine("7. Linkedin_Group");
                string[] str = { Console.ReadLine() };

                string profileType = str[0];

                switch (profileType)
                {
                    case "1":
                        profileType = "facebook";
                        break;
                    case "2":
                        profileType = "twitter";
                        break;
                    case "3":
                        profileType = "linkedin";
                        break;
                    case "4":
                        profileType = "instagram";
                        break;
                    case "5":
                        profileType = "tumblr";
                        break;
                    case "6":
                        profileType = "facebookgroup";
                        break;
                    case "7":
                        profileType = "linkedingroup";
                        break;
                    default:
                        break;
                }


                if (!string.IsNullOrEmpty(profileType))
                {
                    //RunDataSchedulerSirAccount(profileType);//RunDataScheduler(profileType);
                    RunDataScheduler(profileType);
                }
                else
                {
                    RunNewsLetterScheduler();
                }

            }
        }
        private static void RunDataScheduler(string profiletype)
        {
            while (true)
            {
                try
                {
                    Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                    List<Domain.Myfashion.Domain.ScheduledMessage> lstScheduledMessage = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.getScheduledMessageByProfileType(profiletype.ToString()), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
                    ThreadPool.SetMaxThreads(10, 4);
                    if (lstScheduledMessage != null)
                    {
                        if (lstScheduledMessage.Count != 0)
                        {
                            foreach (var item in lstScheduledMessage)
                            {
                                try
                                {
                                    clsSocialSiteScheduler objclsSocialSiteScheduler = new clsSocialSiteScheduler(item.ProfileType);
                                    IScheduler objSocialSiteDataScheduler = objclsSocialSiteScheduler.CreateSocialSiteSchedulerInstance();
                                    if (objSocialSiteDataScheduler != null)
                                    {
                                        Console.WriteLine(objSocialSiteDataScheduler.PostScheduleMessage(item.Id.ToString(), item.UserId.ToString(), item.ProfileId.ToString()));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                        else
                        {

                            Console.WriteLine("No active record in Database");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No active record in Database");
                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                Thread.Sleep(5 * 1000);
            }
        }

        private static void RunDataSchedulerSirAccount(string profiletype)
        {
            while (true)
            {
                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                List<Domain.Myfashion.Domain.ScheduledMessage> lstScheduledMessage = (List<Domain.Myfashion.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.getScheduledMessageByProfileType(profiletype.ToString()), typeof(List<Domain.Myfashion.Domain.ScheduledMessage>)));
                ThreadPool.SetMaxThreads(10, 4);
                if (lstScheduledMessage != null)
                {
                    if (lstScheduledMessage.Count != 0)
                    {
                        foreach (var item in lstScheduledMessage)
                        {
                            try
                            {
                                string testUserId = "2350e2cf-c790-4253-b17f-1f284fa244d5";
                                //if (item.UserId.ToString() != "e855ee26-da94-452f-ba74-5ef7e252083e")
                                //if (item.UserId.ToString() != testUserId)
                                //{
                                //    continue;
                                //}
                                clsSocialSiteScheduler objclsSocialSiteScheduler = new clsSocialSiteScheduler(item.ProfileType);
                                IScheduler objSocialSiteDataScheduler = objclsSocialSiteScheduler.CreateSocialSiteSchedulerInstance();
                                if (objSocialSiteDataScheduler != null)
                                {
                                    Console.WriteLine(objSocialSiteDataScheduler.PostScheduleMessage(item.Id.ToString(), item.UserId.ToString(), item.ProfileId.ToString()));
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else
                    {

                        Console.WriteLine("No active record in Database");
                    }
                }
                else
                {
                    Console.WriteLine("No active record in Database");
                }

                Thread.Sleep(5 * 1000);
            }
        }

        private static void RunNewsLetterScheduler()
        {
            NewsLetterScheduler.PostNewsLetter();
        }
    }
    public class ShareMessage
    {

    }
}

