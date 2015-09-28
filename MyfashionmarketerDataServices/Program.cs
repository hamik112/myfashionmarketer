using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Script.Serialization;


namespace SocioboardDataServices
{
    class Program
    {
        ISocialSiteData objSocialSiteData;
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StartService(args);

        }
        private static void RunDataService(string profiletype)
        {
            while (true)
            {
                try
                {

                    Api.SocialProfile.SocialProfile ApiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                    List<Domain.Myfashion.Domain.SocialProfile> lstSocialProfile = (List<Domain.Myfashion.Domain.SocialProfile>)(new JavaScriptSerializer().Deserialize(ApiobjSocialProfile.SocialProfileByProfilType(profiletype.ToString()), typeof(List<Domain.Myfashion.Domain.SocialProfile>)));

                    ThreadPool.SetMaxThreads(20, 4);
                    if (lstSocialProfile != null)
                    {
                        if (lstSocialProfile.Count != 0)
                        {
                            foreach (var item in lstSocialProfile)
                            {
                                try
                                {
                                    try
                                    {
                                        ThreadPool.SetMaxThreads(20, 4);
                                        //if (item.UserId.ToString() != "62b4eb64-d942-40bc-8af3-2a8b3644f6e2")
                                        //{
                                        //    continue;
                                        //}
                                        RunDataServiceInThreads(item);
                                        //ThreadPool.QueueUserWorkItem(new WaitCallback(RunDataServiceInThreads), item);
                                        //RunDataServiceHardCodedAccountsInThreads(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
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

                    Thread.Sleep(5 * 60 * 1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void RunDataServiceSirAccount(string profiletype)
        {
            while (true)
            {
                try
                {
                    Api.SocialProfile.SocialProfile ApiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                    List<Domain.Myfashion.Domain.SocialProfile> lstSocialProfile = (List<Domain.Myfashion.Domain.SocialProfile>)(new JavaScriptSerializer().Deserialize(ApiobjSocialProfile.SocialProfileByProfilType(profiletype.ToString()), typeof(List<Domain.Myfashion.Domain.SocialProfile>)));

                    ThreadPool.SetMaxThreads(20, 4);
                    if (lstSocialProfile != null)
                    {
                        if (lstSocialProfile.Count != 0)
                        {
                            foreach (var item in lstSocialProfile)
                            {
                                if ((item.UserId.ToString() != "e855ee26-da94-452f-ba74-5ef7e252083e") && (item.UserId.ToString() != "86ff0ec0-2ccd-4110-8c3b-5bd1e37cdced") && (item.UserId.ToString() != "2350e2cf-c790-4253-b17f-1f284fa244d5"))
                                {
                                    continue;
                                }
                                try
                                {
                                    ThreadPool.SetMaxThreads(20, 4);
                                    //ThreadPool.QueueUserWorkItem(new WaitCallback(RunDataServiceInThreads), item);
                                    RunDataServiceInThreads(item);
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

                    Thread.Sleep(5 * 60 * 1000); //Thread.Sleep(15 * 1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void RunDataServiceInThreads(object objitem)
        {
            try
            {
                Domain.Myfashion.Domain.SocialProfile item = (Domain.Myfashion.Domain.SocialProfile)objitem;
                clsSocialSiteDataFeedsFactory objclsSocialSiteDataFeedsFactory = new clsSocialSiteDataFeedsFactory(item.ProfileType);
                ISocialSiteData objSocialSiteDataFeeds = objclsSocialSiteDataFeedsFactory.CreateSocialSiteDataFeedsInstance();
                if (objSocialSiteDataFeeds != null)
                {
                   
                    Console.WriteLine(objSocialSiteDataFeeds.GetData((object)item.UserId, item.ProfileId));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //void StartService(string[] args)
        //{
        //    string check = string.Empty;
        //    //string[] str = { };
        //    try
        //    {
        //        check = args[0];
        //    }
        //    catch (Exception)
        //    {
        //        check = null;
        //    }
        //    if (string.IsNullOrEmpty(check))
        //    {
        //        Console.WriteLine("1. Facebook");
        //        Console.WriteLine("2. Twitter");
        //        Console.WriteLine("3. Linkedin");
        //        Console.WriteLine("4. Instagram");
        //        Console.WriteLine("5. Tumblr");
        //        Console.WriteLine("6. Youtube");
        //        Console.WriteLine("7. Facebook Page");
        //        Console.WriteLine("8. Social Ticketing");
        //        string[] str = { Console.ReadLine() };

        //        string profileType = str[0];

        //        switch (profileType)
        //        {
        //            case "1":
        //                profileType = "facebook";
        //                break;
        //            case "2":
        //                profileType = "twitter";
        //                break;
        //            case "3":
        //                profileType = "linkedin";
        //                break;
        //            case "4":
        //                profileType = "instagram";
        //                break;
        //            case "5":
        //                profileType = "tumblr";
        //                break;
        //            case "6":
        //                profileType = "youtube";
        //                break;
        //            case "7":
        //                profileType = "facebook_page";
        //                break;
        //            case "8":
        //                profileType = "Ticketing";
        //                //while (true)
        //                //{
        //                //    NegativeFeeds objNegativeFeeds = new NegativeFeeds();
        //                //    objNegativeFeeds.getAllNegativeFeeds();
        //                //    Thread.Sleep(15 * 1000);
        //                //}
        //                break;
        //            default:
        //                break;
        //        }

        //        if (profileType != "Ticketing")
        //        {
        //            //new Thread(() =>
        //            //{
        //                RunDataService(profileType);
        //            //}).Start();
        //                //new Thread(() =>
        //                //{
        //                //    RunDataServiceSirAccount(profileType);
        //                //}).Start();
        //        }
        //    }
        //}

        void StartService(string[] args)
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
                Console.WriteLine("6. Youtube");
                Console.WriteLine("7. Facebook Page");
                Console.WriteLine("8. Social Ticketing");
          
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
                        profileType = "youtube";
                        break;
                    case "7":
                        profileType = "facebook_page";
                        break;
                    case "8":
                        profileType = "Ticketing";
                        break;
                    default:
                        break;
                }
                if (profileType != "Ticketing")
                {

                    RunDataService(profileType);

                }
            }
        }
    }
}
