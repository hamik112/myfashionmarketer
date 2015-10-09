using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace WebSiteReviewData
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StartService(args);
        }

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
                Console.WriteLine("1. websitereview");
                
                string[] str = { Console.ReadLine() };

                string profileType = str[0];

                switch (profileType)
                {
                    case "1":
                        profileType = "websitereview";
                        break;
                    default:
                        break;
                }
                if (profileType != "")
                {

                    RunDataService(profileType);

                }
                
            }
        }


        private static void RunDataService(string profiletype)
        {
            while (true)
            {
                try
                {

                    Api.WebSiteReview.WebSiteReview objWebSiteReview = new Api.WebSiteReview.WebSiteReview();
                    List<Domain.Myfashion.Domain.websitereviewdata> _websitereviewdata = new List<Domain.Myfashion.Domain.websitereviewdata>();
                    string getallwebsiteurl = objWebSiteReview.GetAllWebSite();
                    _websitereviewdata = (List<Domain.Myfashion.Domain.websitereviewdata>)new JavaScriptSerializer().Deserialize(getallwebsiteurl, typeof(List<Domain.Myfashion.Domain.websitereviewdata>)); 
                    //ThreadPool.SetMaxThreads(20, 4);
                    if (_websitereviewdata != null)
                    {
                        if (_websitereviewdata.Count != 0)
                        {
                            foreach (var item in _websitereviewdata)
                            {
                                try
                                {
                                    try
                                    {
                                        //ThreadPool.SetMaxThreads(20, 4);
                                      
                                        RunDataServiceInThreads(item);
                                        
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

        private static void RunDataServiceInThreads(object objitem)
        {
            try
            {
                Domain.Myfashion.Domain.websitereviewdata item = (Domain.Myfashion.Domain.websitereviewdata)objitem;
                clsSocialSiteDataFeedsFactory objclsSocialSiteDataFeedsFactory = new clsSocialSiteDataFeedsFactory("websitereview");
                ISocialSiteData objSocialSiteDataFeeds = objclsSocialSiteDataFeedsFactory.CreateSocialSiteDataFeedsInstance();
                if (objSocialSiteDataFeeds != null)
                {

                    Console.WriteLine(objSocialSiteDataFeeds.UpdateWebSiteData(item.Id.ToString(), item.websitename));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
