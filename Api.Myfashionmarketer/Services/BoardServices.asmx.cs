using Api.Myfashionmarketer.Helper;
using Api.Socioboard.Model;
using Facebook;
using Google.Apis.Analytics.v3;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;

namespace Api.Myfashionmarketer.Models
{
    /// <summary>
    /// Summary description for BoardServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BoardServices : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(BoardServices));
        private BoardRepository boardrepo = new BoardRepository();
        private Companypage companypage = new Companypage();

        public Guid AddTag(string ProfileType, Guid BoardId, string HashTag) 
        {
            HashTag = Uri.EscapeUriString(HashTag);
            HashTag = HashTag.Replace("%E2%80%AA%E2%80%8E", string.Empty); 
           
            //Domain.Myfashion.Domain.Boardhashtags tag = new Domain.Myfashion.Domain.Boardhashtags();
            //tag.Boardid = BoardId;
            //tag.Id = Guid.NewGuid();
            //tag.Hashtag = HashTag;
            //tag.Profile = ProfileType;
            //boardrepo.addBoardTag(tag);
            //if (ProfileType.Equals("facebook")) 
            //{

            //}
            if (ProfileType.Equals("facebook"))
            {
                Domain.Myfashion.Domain.Boardfbpage boardfbpage = new Domain.Myfashion.Domain.Boardfbpage { Id = Guid.NewGuid(), BoardId = BoardId, About= string.Empty,Checkins= string.Empty,Companyname=HashTag,Entrydate= DateTime.Now,FbPageId="tag",fbPageName= HashTag,PageUrl=string.Empty,ProfileImageUrl= string.Empty,TalkingAboutCount= string.Empty };
                boardrepo.addBoardFbPage(boardfbpage);
                AddBoardFbTagRecentFeeds(HashTag, boardfbpage.Id);
                return boardfbpage.Id;
            }
             if (ProfileType.Equals("twitter")) 
            {
                Domain.Myfashion.Domain.Boardtwitteraccount twitteracc = new Domain.Myfashion.Domain.Boardtwitteraccount { Id = Guid.NewGuid(), Boardid = BoardId, Statuscount=string.Empty,Entrydate = DateTime.Now, Screenname = HashTag, Twitterprofileid = "tag",Friendscount=string.Empty,Followingscount =string.Empty,Followerscount=string.Empty,Favouritescount=string.Empty,Photosvideos=string.Empty,Url=string.Empty,Tweet=string.Empty,Profileimageurl="tag" };
                boardrepo.addBoardTwitterAccount(twitteracc);
                AddBoardTwitterHashTagFeeds(HashTag, twitteracc.Id, null);
                 return twitteracc.Id;
            }
            else if (ProfileType.Equals("instagram")) 
            {
                Domain.Myfashion.Domain.Boardinstagramaccount binstacc = new Domain.Myfashion.Domain.Boardinstagramaccount { Id = Guid.NewGuid(), Bio = "tag", Boardid = BoardId, Entrydate = DateTime.Now, Followedbycount = string.Empty, Followscount = string.Empty, Media = string.Empty, Profileid = HashTag, Profilepicurl = string.Empty, Username = HashTag };
                boardrepo.addBoardInstagramAccount(binstacc);
                AddBoardInstagramTagFeeds(HashTag, binstacc.Id);
                return binstacc.Id;
            }
            else if (ProfileType.Equals("gplus")) 
            {
                Domain.Myfashion.Domain.Boardgplusaccount bgpacc = new Domain.Myfashion.Domain.Boardgplusaccount { Id = Guid.NewGuid(), Aboutme = string.Empty, Boardid = BoardId, Circledbycount = string.Empty, Coverphotourl = string.Empty, Displayname = HashTag, Entrydate = DateTime.Now, Nickname = HashTag, Pageid = "tag", Pageurl = string.Empty, Plusonecount = string.Empty, Profileimageurl = string.Empty, Tagline = string.Empty };
                boardrepo.addBoardGPlusAccount(bgpacc);
                AddBoardGplusTagFeeds(HashTag, bgpacc.Id);
                return bgpacc.Id;
            }
             return Guid.Empty;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetUserBoards(string UserId, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            Guid userId = Guid.Parse(UserId);
            List<Domain.Myfashion.Domain.Boards> boards = boardrepo.getUserBoards(userId);

            return new JavaScriptSerializer().Serialize(boards.OrderByDescending(t => t.CreateDate));

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAnalytics(string BoardId)
        {
            Domain.Myfashion.Domain.GoogleAnalyticsViewModel ganalytics = new Domain.Myfashion.Domain.GoogleAnalyticsViewModel();
            AnalyticsService service;
            string filePath = Server.MapPath("/SocioboardBoardMe-1b20a17f8879.p12");
            service = Api.Myfashionmarketer.Helper.GoogleAnalyticsAuthenticationHelper.AuthenticateServiceAccount("244513724654-0th6146rg1cqvr0hmqhv1r5gjgm3rbg3@developer.gserviceaccount.com", filePath);
            GoogleAnaltyicsReportingHelper.OptionalValues options = new GoogleAnaltyicsReportingHelper.OptionalValues();
            options.Dimensions = "ga:year,ga:month,ga:day";
            options.Filter = "ga:pagePath==/board/" + BoardId;
            //Make sure the profile id you send is valid.  
            var x = GoogleAnaltyicsReportingHelper.Get(service, "101919148", "10daysAgo", "today", "ga:pageviews", options);
            List<Domain.Myfashion.Domain.AnalyticsGraphViewModel> analyticsGraphList = new List<Domain.Myfashion.Domain.AnalyticsGraphViewModel>();
            foreach (var item in x.Rows)
            {
                Domain.Myfashion.Domain.AnalyticsGraphViewModel anagraphviemodel = new Domain.Myfashion.Domain.AnalyticsGraphViewModel();
                anagraphviemodel.Date = new DateTime(int.Parse(item[0]), int.Parse(item[1]), int.Parse(item[2])).ToString("dd-MM-yyyy");
                anagraphviemodel.ViewsCount = item[3];
                analyticsGraphList.Add(anagraphviemodel);
            }
            ganalytics.PageViewsGraphData = analyticsGraphList;

            GoogleAnaltyicsReportingHelper.OptionalValues options1 = new GoogleAnaltyicsReportingHelper.OptionalValues();
            options1.Dimensions = "ga:pagePath";
            options1.Filter = "ga:pagePath==/board/" + BoardId;
            // options1.Dimensions = "ga:date";
            //Make sure the profile id you send is valid.  
            var x1 = GoogleAnaltyicsReportingHelper.Get(service, "101919148", "2015-01-01", "today", "ga:sessions,ga:pageviews", options1);

            try
            {

                ganalytics.TotalPageViews = x1.Rows[0][2];
                ganalytics.TotalSessions = x1.Rows[0][1];
            }
            catch 
            {

                ganalytics.TotalPageViews = "0";
            }


            GoogleAnalyticsRealTimeHelper.OptionalValues rtOptions = new GoogleAnalyticsRealTimeHelper.OptionalValues();
            rtOptions.Dimensions = "rt:userType, ga:pagePath";
            rtOptions.Filter = "ga:pagePath==/board/" + BoardId;
            //Make sure the profile id you send is valid.  
            var realTimeData = GoogleAnalyticsRealTimeHelper.Get(service, "101919148", "rt:activeUsers", rtOptions);
            ganalytics.RealTimePageViews = realTimeData.TotalsForAllResults.First().Value;



            GoogleAnaltyicsReportingHelper.OptionalValues optionss = new GoogleAnaltyicsReportingHelper.OptionalValues();
            optionss.Dimensions = "ga:country,ga:city";
            optionss.Filter = "ga:pagePath==/board/" + BoardId;
            //Make sure the profile id you send is valid.  
            var xs = GoogleAnaltyicsReportingHelper.Get(service, "101919148", "10daysAgo", "today", "ga:pageviews", optionss);
            List<Domain.Myfashion.Domain.GoogleAnalyticsCityPageViews> citypageviewlist = new List<Domain.Myfashion.Domain.GoogleAnalyticsCityPageViews>();
            try
            {
                foreach (var item in xs.Rows)
                {
                    Domain.Myfashion.Domain.GoogleAnalyticsCityPageViews cityview = new Domain.Myfashion.Domain.GoogleAnalyticsCityPageViews();
                    cityview.Count = item[2];
                    cityview.City = item[1];
                    cityview.Country = item[0];
                    citypageviewlist.Add(cityview);
                }
            }
            catch
            {

            }
            ganalytics.citypageviews = citypageviewlist;




            return new JavaScriptSerializer().Serialize(ganalytics);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool DeleteBoard(string BoardId) 
        {
            Domain.Myfashion.Domain.Boards board = boardrepo.getBoard(Guid.Parse(BoardId));
            return boardrepo.DeleteBoard(board);

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool UndoDeleteBoard(string BoardId)
        {
            Domain.Myfashion.Domain.Boards board = boardrepo.getDeletedBoard(Guid.Parse(BoardId));
            return boardrepo.UndoDeleteBoard(board);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddBoard(string CompanyName, string InstagramProfileId, string FbProfileId, string TwitterProfileId, string LinkedinProfileId, string YoutubeProfileId, string GPlusProfileId, string TumblrProfileId, string UserId)
        {
            string output = string.Empty;
            if (!boardrepo.checkgBoardExists(CompanyName))
            {
                Domain.Myfashion.Domain.Boards board = new Domain.Myfashion.Domain.Boards();
                board.BoardId = Guid.NewGuid();
                //companyprofiles.UserId = UserId;
                board.BoardName = CompanyName;
                board.CreateDate = DateTime.UtcNow;
                board.UserId = Guid.Parse(UserId);
                try
                {
                    if (!string.IsNullOrEmpty(FbProfileId))
                    {
                        if (!FbProfileId.Trim().StartsWith("#"))
                        {
                            //Domain.Myfashion.Domain.Boardfbpage bfbpage = Addfbpage(FbProfileId, board.BoardId.ToString());
                            //board.Fbprofileid = bfbpage.Id.ToString();

                            //if (bfbpage != null && bfbpage.Id != Guid.Empty && !string.IsNullOrEmpty(bfbpage.FbPageId))
                            //{
                            //    //AddBoardFbRecentFeeds(bfbpage.FbPageId, bfbpage.Id);
                            //}
                        }
                        else
                        {
                            board.Fbprofileid = AddTag("facebook", board.BoardId, FbProfileId.Replace(@"#", null).Trim()).ToString();
                        }
                    }
                }
                catch { }
                try
                {
                    if (!string.IsNullOrEmpty(InstagramProfileId))
                    {
                        if (!InstagramProfileId.Trim().StartsWith("#"))
                        {
                            Domain.Myfashion.Domain.Boardinstagramaccount binstfeeds = AddBoardInstagramPage(InstagramProfileId, board.BoardId.ToString());
                            board.Instagramprofileid = binstfeeds.Id.ToString();
                            if (binstfeeds != null && binstfeeds.Id != Guid.Empty && !string.IsNullOrEmpty(binstfeeds.Profileid))
                            {
                                AddBoardInstagramPageFeeds(binstfeeds.Profileid, binstfeeds.Id);
                            }
                        }
                        else
                        {
                            string keyword = InstagramProfileId.Replace(@"#", null).Trim();

                            board.Instagramprofileid = AddTag("instagram", board.BoardId, keyword).ToString();

                        }
                    }
                }
                catch
                {
                }
                try
                {
                    if (!string.IsNullOrEmpty(TwitterProfileId))
                    {
                        if (!TwitterProfileId.Trim().StartsWith("#"))
                        {
                            Domain.Myfashion.Domain.Boardtwitteraccount btwitteracc = AddTwitterPage(TwitterProfileId, board.BoardId.ToString());
                            board.Twitterprofileid = btwitteracc.Id.ToString();
                            if (btwitteracc != null && btwitteracc.Id != Guid.Empty && !string.IsNullOrEmpty(btwitteracc.Screenname))
                            {
                                AddBoardTwitterPageFeeds(btwitteracc.Screenname, btwitteracc.Id, null);
                            }
                        }
                        else
                        {
                            board.Twitterprofileid = AddTag("twitter", board.BoardId, TwitterProfileId.Replace(@"#", null).Trim()).ToString();
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    if (!GPlusProfileId.Trim().StartsWith("#"))
                    {
                        if (!string.IsNullOrEmpty(GPlusProfileId))
                        {
                            Domain.Myfashion.Domain.Boardgplusaccount bgpulspage = AddBoardGplusPage(GPlusProfileId, board.BoardId.ToString());
                            board.Gplusprofileid = bgpulspage.Id.ToString();
                            if (bgpulspage != null && bgpulspage.Id != Guid.Empty && !string.IsNullOrEmpty(bgpulspage.Pageid))
                            {
                                AddBoardGplusFeeds(bgpulspage.Pageid, bgpulspage.Id);
                            }
                        }
                    }
                    else
                    {
                        board.Gplusprofileid = AddTag("gplus", board.BoardId, GPlusProfileId.Replace(@"#", null).Trim()).ToString();
                    }
                }
                catch
                {
                }
                bool IsSaved = boardrepo.AddBoard(board);
                if (IsSaved)
                {
                    output = "Saved Successfully";
                }
                else
                {
                    output = "Fail to Save";
                }
            }
            else {
                output = "Board already exists";
            }
            return output;
        }




        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateBoard(string Id, string CompanyName, string InstagramProfileId, string FbProfileId, string TwitterProfileId, string LinkedinProfileId, string YoutubeProfileId, string GPlusProfileId, string TumblrProfileId)
        {
            string output = string.Empty;
            Domain.Myfashion.Domain.Boards board = boardrepo.getBoard(Guid.Parse(Id));
            //companyprofiles.UserId = UserId;
            board.BoardName = CompanyName;
            if (!string.IsNullOrEmpty(FbProfileId))
            {
                try
                {
                    board.Fbprofileid = JObject.Parse(companypage.SearchFacebookPage(FbProfileId))["id"].ToString();
                }
                catch (Exception e) { }
            }


            if (!string.IsNullOrEmpty(InstagramProfileId))
            {
                try
                {
                    board.Instagramprofileid = JObject.Parse(companypage.getInstagramCompanyPage(InstagramProfileId))["data"]["id"].ToString();
                }
                catch (Exception e)
                {
                }
            }
            if (!string.IsNullOrEmpty(LinkedinProfileId))
            {
                try
                {
                    XmlNode ResultCompany = null;
                    int followers = 0;
                    string result = string.Empty;
                    result = companypage.LinkedinSearch(LinkedinProfileId);
                    XmlDocument XmlResult = new XmlDocument();
                    XmlResult.Load(new StringReader(result));
                    XmlNodeList Companies = XmlResult.SelectNodes("company-search/companies/company");
                    foreach (XmlNode node in Companies)
                    {
                        if (Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText) > followers)
                        {
                            ResultCompany = node;
                            followers = Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText);
                        }
                    }
                    board.Linkedinprofileid = ResultCompany.SelectSingleNode("universal-name").InnerText;
                    //companyProfile.LinkedinProfileId = string.Empty;
                }
                catch (Exception e)
                {

                    // throw;
                }
            }
            if (!string.IsNullOrEmpty(TumblrProfileId))
            {
                try
                {
                    board.Tumblrprofileid = JObject.Parse(companypage.TumblrSearch(TumblrProfileId))["response"]["blog"]["name"].ToString();
                }
                catch (Exception e)
                {

                    //throw;
                }
            }
            if (!string.IsNullOrEmpty(TwitterProfileId))
            {
                try
                {
                    board.Twitterprofileid = JObject.Parse(companypage.TwitterSearch(TwitterProfileId))["screen_name"].ToString();
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(YoutubeProfileId))
            {
                try
                {
                    string result = companypage.YoutubeSearch(YoutubeProfileId);
                    if (!result.StartsWith("["))
                        result = "[" + result + "]";
                    JArray youtubechannels = JArray.Parse(result);
                    JObject resultPage = (JObject)youtubechannels[0];
                    JObject YtubeChannel = (JObject)resultPage["items"][0];
                    board.Youtubeprofileid = YtubeChannel["id"].ToString();
                }
                catch (Exception e)
                {

                    //throw;
                }
            }
            if (!string.IsNullOrEmpty(GPlusProfileId))
            {
                try
                {
                    board.Gplusprofileid = JObject.Parse(companypage.GooglePlusgetUser(GPlusProfileId))["id"].ToString();
                }
                catch (Exception e)
                {


                }
            }
            bool IsSaved = boardrepo.updateBoard(board);
            if (IsSaved)
            {
                output = "Saved Successfully";
            }
            else
            {
                output = "Fail to Save";
            }
            return output;
        }



        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Domain.Myfashion.Domain.Boards AddBoardInfo(string name)
        {
            string ret = string.Empty;
            Domain.Myfashion.Domain.Boards board = new Domain.Myfashion.Domain.Boards();
            board.BoardName = name;
            //boards.IsVerifited = false;

            try
            {
                board.Fbprofileid = JObject.Parse(companypage.SearchFacebookPage(name))["id"].ToString();
            }
            catch (Exception e) { }



            try
            {
                board.Instagramprofileid = JObject.Parse(companypage.getInstagramCompanyPage(name))["data"]["id"].ToString();
            }
            catch (Exception e)
            {
            }

            try
            {
                XmlNode ResultCompany = null;
                int followers = 0;
                string result = string.Empty;
                result = companypage.LinkedinSearch(name);
                XmlDocument XmlResult = new XmlDocument();
                XmlResult.Load(new StringReader(result));
                XmlNodeList Companies = XmlResult.SelectNodes("company-search/companies/company");
                foreach (XmlNode node in Companies)
                {
                    if (Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText) > followers)
                    {
                        ResultCompany = node;
                        followers = Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText);
                    }
                }
                board.Linkedinprofileid = ResultCompany.SelectSingleNode("universal-name").InnerText;
                //companyProfile.LinkedinProfileId = string.Empty;
            }
            catch (Exception e)
            {

                // throw;
            }

            try
            {
                board.Tumblrprofileid = JObject.Parse(companypage.TumblrSearch(name))["response"]["blog"]["url"].ToString();
            }
            catch (Exception e)
            {

                //throw;
            }

            try
            {
                board.Twitterprofileid = JObject.Parse(companypage.TwitterSearch(name))["screen_name"].ToString();
            }
            catch
            {
            }

            try
            {
                string result = companypage.YoutubeSearch(name);
                if (!result.StartsWith("["))
                    result = "[" + result + "]";
                JArray youtubechannels = JArray.Parse(result);
                JObject resultPage = (JObject)youtubechannels[0];
                JObject YtubeChannel = (JObject)resultPage["items"][0];
                board.Youtubeprofileid = YtubeChannel["id"].ToString();
            }
            catch (Exception e)
            {

                //throw;
            }

            try
            {
                board.Gplusprofileid = JObject.Parse(companypage.GooglePlusgetUser(name))["id"].ToString();
            }
            catch (Exception e)
            {


            }

            board.BoardId = Guid.NewGuid();
            boardrepo.AddBoard(board);
            return board;
        }

        //Suresh
        #region  get company information from table
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getBoardInfo(string keyword)
        {
            string output = string.Empty;
            Domain.Myfashion.Domain.Boards board = new Domain.Myfashion.Domain.Boards();
            try
            {
                Domain.Myfashion.Domain.Boards objboard = boardrepo.SearchBoardByName(keyword);
                board.BoardId = objboard.BoardId;
                board.BoardName = objboard.BoardName;
                try
                {
                    board.Fbprofileid = JObject.Parse(companypage.SearchFacebookPage(keyword))["id"].ToString();
                }
                catch (Exception e) { }



                try
                {
                    board.Instagramprofileid = JObject.Parse(companypage.getInstagramCompanyPage(keyword))["data"]["id"].ToString();
                }
                catch (Exception e)
                {
                }

                try
                {
                    XmlNode ResultCompany = null;
                    int followers = 0;
                    string result = string.Empty;
                    result = companypage.LinkedinSearch(keyword);
                    XmlDocument XmlResult = new XmlDocument();
                    XmlResult.Load(new StringReader(result));
                    XmlNodeList Companies = XmlResult.SelectNodes("company-search/companies/company");
                    foreach (XmlNode node in Companies)
                    {
                        if (Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText) > followers)
                        {
                            ResultCompany = node;
                            followers = Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText);
                        }
                    }
                    board.Linkedinprofileid = ResultCompany.SelectSingleNode("universal-name").InnerText;
                    //companyProfile.LinkedinProfileId = string.Empty;
                }
                catch (Exception e)
                {

                    // throw;
                }

                try
                {
                    board.Tumblrprofileid = JObject.Parse(companypage.TumblrSearch(keyword))["response"]["blog"]["url"].ToString();
                }
                catch (Exception e)
                {

                    //throw;
                }

                try
                {
                    board.Twitterprofileid = JObject.Parse(companypage.TwitterSearch(keyword))["id"].ToString();
                }
                catch
                {
                }

                try
                {
                    string result = companypage.YoutubeSearch(keyword);
                    if (!result.StartsWith("["))
                        result = "[" + result + "]";
                    JArray youtubechannels = JArray.Parse(result);
                    JObject resultPage = (JObject)youtubechannels[0];
                    JObject YtubeChannel = (JObject)resultPage["items"][0];
                    board.Youtubeprofileid = YtubeChannel["id"].ToString();
                }
                catch (Exception e)
                {

                    //throw;
                }

                try
                {
                    board.Gplusprofileid = JObject.Parse(companypage.GooglePlusgetUser(keyword))["id"].ToString();
                }
                catch (Exception e)
                {


                }

                return new JavaScriptSerializer().Serialize(board);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SearchBoard(string keyword)
        {
            string output = string.Empty;

            try
            {
                Domain.Myfashion.Domain.Boards objBoard = boardrepo.SearchBoardByName(keyword);
                if (objBoard == null)
                {
                    objBoard = AddBoardInfo(keyword);
                }
                return new JavaScriptSerializer().Serialize(objBoard);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardDetails(string id)
        {
            string output = string.Empty;
            try
            {
                Domain.Myfashion.Domain.Boards objBoard = boardrepo.getBoard(Guid.Parse(id));

                return new JavaScriptSerializer().Serialize(objBoard);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardFbPage(string BoardId) 
        {
            string output = string.Empty;
            try
            {
                Domain.Myfashion.Domain.Boardfbpage objBoardFbPage = boardrepo.getBoardFbpage(Guid.Parse(BoardId));

                return new JavaScriptSerializer().Serialize(objBoardFbPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardFbPageFeeds(string BoardfbPageId)
        {
            string output = string.Empty;
            try
            {
               List<Domain.Myfashion.Domain.Boardfbfeeds> objBoardFbPage = boardrepo.getBoardFbfeeds(Guid.Parse(BoardfbPageId));

                return new JavaScriptSerializer().Serialize(objBoardFbPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


       




        public Domain.Myfashion.Domain.Boardfbpage Addfbpage(string Facebookpageid, string Boardid)
        {
            string output = string.Empty;
            Domain.Myfashion.Domain.Boardfbpage objFbPage = new Domain.Myfashion.Domain.Boardfbpage();
            try
            {
                Guid pageId = Guid.Empty;
                try
                {
                    objFbPage.BoardId = Guid.Parse(Boardid);
                    pageId = Guid.Parse(Facebookpageid);
                }
                catch (Exception e) { }

                if (pageId == Guid.Empty || objFbPage.BoardId != Guid.Empty)
                {
                    JObject fbPage = JObject.Parse(companypage.SearchFacebookPage(Facebookpageid));
                    try
                    {
                        objFbPage.FbPageId = fbPage["id"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.PageUrl = fbPage["link"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.TalkingAboutCount = fbPage["talking_about_count"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.fbPageName = fbPage["username"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.Likes = fbPage["likes"].ToString();
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        objFbPage.Country = fbPage["location"]["country"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.City = fbPage["location"]["city"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.Phone = fbPage["location"]["city"].ToString();
                    }
                    catch (Exception e) { }

                    try
                    {
                        objFbPage.Website = fbPage["website"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.Founded = fbPage["founded"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.Mission = fbPage["mission"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.Description = fbPage["description"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.About = fbPage["about"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objFbPage.Companyname = fbPage["name"].ToString();
                    }
                    catch (Exception e) { }
                    objFbPage.Entrydate = DateTime.UtcNow;
                    objFbPage.BoardId = Guid.Parse(Boardid);
                    objFbPage.Id = Guid.NewGuid();
                    try
                    {
                        boardrepo.addBoardFbPage(objFbPage);
                    }
                    catch (Exception e)
                    {


                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                // return "Something Went Wrong";
            }

            return objFbPage;
        }

        public bool AddBoardFbRecentFeeds(string FacebookProfileId, Guid BoardfbPageId)
        {
            bool output = false;
            string fbpageNotesstring = companypage.getFacebookPageFeeds(FacebookProfileId);
            JObject fbpageNotes = JObject.Parse(fbpageNotesstring);
            foreach (JObject obj in JArray.Parse(fbpageNotes["data"].ToString()))
            {
                Domain.Myfashion.Domain.Boardfbfeeds fbfeed = new Domain.Myfashion.Domain.Boardfbfeeds();
                fbfeed.Id = Guid.NewGuid();
                fbfeed.Fbpageprofileid = BoardfbPageId;
                fbfeed.Isvisible = true;
                try
                {
                    fbfeed.Type = obj["type"].ToString();
                }
                catch { }

                try
                {
                    fbfeed.Link = obj["link"].ToString();
                }
                catch { }
                try
                {
                    fbfeed.Message = obj["message"].ToString();
                }
                catch { }
                try
                {
                    fbfeed.Createddate = Convert.ToDateTime(obj["created_time"].ToString());
                }
                catch { }
                try
                {
                    fbfeed.Description = obj["description"].ToString();
                }
                catch { }
                try
                {
                    fbfeed.Image = obj["picture"].ToString();
                }
                catch { }
                try
                {
                    fbfeed.Feedid = obj["id"].ToString();
                }
                catch { }
                try
                {
                    fbfeed.Videolink = obj["source"].ToString();
                }
                catch { }
                try
                {
                    fbfeed.Story = obj["story"].ToString();
                }
                catch { }
                try
                {
                    fbfeed.Likes = "http://graph.facebook.com/" + obj["id"] + "/likes"; ;
                }
                catch { }
                try
                {
                    fbfeed.FromId = obj["from"]["id"].ToString();
                }
                catch { }

                try
                {
                    fbfeed.FromName = obj["from"]["name"].ToString();
                }
                catch { }

                if (!boardrepo.checkFacebookFeedExists(fbfeed.Feedid, BoardfbPageId))
                {
                    boardrepo.addBoardFbPageFeed(fbfeed);
                }

            }
            output = true;
            return output;
        }


        public bool AddBoardFbTagRecentFeeds(string FacebookProfileId, Guid BoardfbPageId)
        {
            bool output = false;


            string fbpageNotesstring = companypage.getFacebookTagFeeds(FacebookProfileId, BoardfbPageId);
            //JObject fbpageNotes = JObject.Parse(fbpageNotesstring);
            //foreach (JObject obj in JArray.Parse(fbpageNotes["data"].ToString()))
            //{
            //    Domain.Myfashion.Domain.Boardfbfeeds fbfeed = new Domain.Myfashion.Domain.Boardfbfeeds();
            //    fbfeed.Id = Guid.NewGuid();
            //    fbfeed.Fbpageprofileid = BoardfbPageId;
            //    fbfeed.Isvisible = true;
            //    try
            //    {
            //        fbfeed.Type = obj["type"].ToString();
            //    }
            //    catch { }

            //    try
            //    {
            //        fbfeed.Link = obj["link"].ToString();
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Message = obj["message"].ToString();
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Createddate = Convert.ToDateTime(obj["created_time"].ToString());
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Description = obj["description"].ToString();
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Image = obj["picture"].ToString();
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Feedid = obj["id"].ToString();
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Videolink = obj["source"].ToString();
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Story = obj["story"].ToString();
            //    }
            //    catch { }
            //    try
            //    {
            //        fbfeed.Likes = "http://graph.facebook.com/" + obj["id"] + "/likes";
            //    }
            //    catch { }

            //    if (!boardrepo.checkFacebookFeedExists(fbfeed.Feedid, BoardfbPageId))
            //    {
            //        boardrepo.addBoardFbPageFeed(fbfeed);
            //    }

            //}
            output = true;
            return output;
        }

        
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateFbFeeds(string Key) 
        {
            if (Key.Equals("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988")) 
            {
                List<Domain.Myfashion.Domain.Boardfbpage> boardfbpages = boardrepo.getAllfbPages();
                foreach (Domain.Myfashion.Domain.Boardfbpage fbpage in boardfbpages) 
                {
                    AddBoardFbRecentFeeds(fbpage.FbPageId, fbpage.BoardId);
                }
            }
        }


        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetCompanyfbpage(string id)
        //{
        //    return new JavaScriptSerializer().Serialize(Getfbpage(id));
        //}




        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardTwitterFeedsCount(string BoardTwitterAccountId)
        {
            string output = string.Empty;
            try
            {
                int objBoardFbPage = boardrepo.getBoardTwitterfeedsCount(Guid.Parse(BoardTwitterAccountId));

                return new JavaScriptSerializer().Serialize(objBoardFbPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardTwitterFeedsRetweetsCount(string BoardTwitterAccountId)
        {
            string output = string.Empty;
            try
            {
                int objBoardFbPage = boardrepo.getBoardTwitterfeedsRetweetsCount(Guid.Parse(BoardTwitterAccountId));

                return new JavaScriptSerializer().Serialize(objBoardFbPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardTwitterFeedsFavoritedCount(string BoardTwitterAccountId)
        {
            string output = string.Empty;
            try
            {
                int objBoardFbPage = boardrepo.getBoardTwitterfeedsFavoritedCount(Guid.Parse(BoardTwitterAccountId));

                return new JavaScriptSerializer().Serialize(objBoardFbPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardTwitterToptenFeeds(string BoardTwitterAccountId)
        {
            string output = string.Empty;
            try
            {
                List<Domain.Myfashion.Domain.Boardtwitterfeeds> objBoardtwtfeeds = boardrepo.getPopularTweets(Guid.Parse(BoardTwitterAccountId));

                return new JavaScriptSerializer().Serialize(objBoardtwtfeeds);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardTwitterTopTweetsByFavorited(string BoardTwitterAccountId)
        {
            string output = string.Empty;
            try
            {
                List<Domain.Myfashion.Domain.Boardtwitterfeeds> objBoardtwtfeeds = boardrepo.getPopularTweetsByFavorited(Guid.Parse(BoardTwitterAccountId));

                return new JavaScriptSerializer().Serialize(objBoardtwtfeeds);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardTwitterAccount(string BoardId)
        {
            string output = string.Empty;
            try
            {
                Domain.Myfashion.Domain.Boardtwitteraccount objBoardFbPage = boardrepo.getBoardTwitterAccount(Guid.Parse(BoardId));

                return new JavaScriptSerializer().Serialize(objBoardFbPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardTwitterFeeds(string BoardTwitterAccountId)
        {
            string output = string.Empty;
            try
            {
                List<Domain.Myfashion.Domain.Boardtwitterfeeds> objBoardFbPage = boardrepo.getBoardTwitterfeeds(Guid.Parse(BoardTwitterAccountId));

                return new JavaScriptSerializer().Serialize(objBoardFbPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        public Domain.Myfashion.Domain.Boardtwitteraccount AddTwitterPage(string TwitterPageid, string Boardid)
        {
            string output = string.Empty;
            Domain.Myfashion.Domain.Boardtwitteraccount objTwitterPage = new Domain.Myfashion.Domain.Boardtwitteraccount();
            try
            {
                Guid pageId = Guid.Empty;
                try
                {
                    objTwitterPage.Boardid = Guid.Parse(Boardid);
                    pageId = Guid.Parse(TwitterPageid);
                }
                catch (Exception e) { }

                if (pageId == Guid.Empty || objTwitterPage.Boardid != Guid.Empty)
                {
                    JObject twitterPage = JObject.Parse(companypage.TwitterSearch(TwitterPageid));
                    try
                    {
                        objTwitterPage.Url = twitterPage["url"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objTwitterPage.Twitterprofileid = twitterPage["id"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objTwitterPage.Profileimageurl = twitterPage["profile_image_url"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objTwitterPage.Followerscount = twitterPage["followers_count"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objTwitterPage.Followingscount = twitterPage["following"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objTwitterPage.Favouritescount = twitterPage["favourites_count"].ToString();
                    }
                    catch (Exception e) { }

                    try
                    {
                        objTwitterPage.Screenname = twitterPage["screen_name"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objTwitterPage.Statuscount = twitterPage["statuses_count"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objTwitterPage.Friendscount = twitterPage["friends_count"].ToString();
                    }
                    catch (Exception e) { }
                    objTwitterPage.Entrydate = DateTime.UtcNow;
                    objTwitterPage.Id = Guid.NewGuid();
                    objTwitterPage.Boardid = Guid.Parse(Boardid);
                    boardrepo.addBoardTwitterAccount(objTwitterPage);
                }
                else
                {
                    //objTwitterPage = ObjTwitterPageRepository.getTwitterPageInfo(pageId);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                // return "Something Went Wrong";
            }
            return objTwitterPage;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateTwitterFeeds(string Key)
        {
            if (Key.Equals("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988"))
            {
                List<Domain.Myfashion.Domain.Boardtwitteraccount> boardtwitteracc = boardrepo.getAllBoardTwiiterAccounts();
                foreach (Domain.Myfashion.Domain.Boardtwitteraccount twitteracc in boardtwitteracc)
                {
                    string LastTweetId = boardrepo.getLastTweetId(twitteracc.Id);

                    if (twitteracc.Profileimageurl.Equals("tag"))
                    {
                        AddTwitterHashTagFeeds(twitteracc.Screenname, twitteracc.Id,LastTweetId);
                    }
                    else 
                    {
                        AddTwitterPageFeeds(twitteracc.Screenname, twitteracc.Id,LastTweetId);
                    }
                }
            }
        }
        public bool AddBoardTwitterPageFeeds(string TwitterScreenName, Guid Boardtwitterprofileid, string LastTweetId)
        {
            bool output = false;
            List<Domain.Myfashion.Domain.Boardtwitterfeeds> twtFeedsList = new List<Domain.Myfashion.Domain.Boardtwitterfeeds>();
            string timeline = companypage.TwitterBoardUserTimeLine(TwitterScreenName, LastTweetId);
            if (!string.IsNullOrEmpty(timeline) && !timeline.Equals("[]"))
            {
                foreach (JObject obj in JArray.Parse(timeline))
                {
                    Domain.Myfashion.Domain.Boardtwitterfeeds twitterfeed = new Domain.Myfashion.Domain.Boardtwitterfeeds();
                    twitterfeed.Id = Guid.NewGuid();


                    try
                    {
                        twitterfeed.Feedurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["url"].ToString();
                    }
                    catch
                    {
                        try
                        {
                            twitterfeed.Feedurl = JArray.Parse(obj["entities"]["urls"].ToString())[0]["expanded_url"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.Imageurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["media_url"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        foreach (JObject tag in JArray.Parse(obj["entities"]["hashtags"].ToString()))
                        {
                            try
                            {
                                twitterfeed.Hashtags = tag["text"].ToString() + ",";

                            }
                            catch (Exception e)
                            {
                                logger.Error(e.Message);
                                logger.Error(e.StackTrace);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Text = obj["text"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Retweetcount = Convert.ToInt32(obj["retweet_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Favoritedcount = Convert.ToInt32(obj["favorite_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
                        twitterfeed.Createdat = DateTime.ParseExact((string)obj["created_at"], Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Feedid = obj["id_str"].ToString();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            twitterfeed.Feedid = obj["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromId = obj["user"]["id_str"].ToString();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            twitterfeed.FromId = obj["user"]["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromName = obj["user"]["screen_name"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.FromPicUrl = obj["user"]["profile_image_url"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    twitterfeed.Isvisible = true;
                    twitterfeed.Twitterprofileid = Boardtwitterprofileid;
                    if (!boardrepo.checkTwitterFeedExists(twitterfeed.Feedid, Boardtwitterprofileid))
                    {
                        //twtFeedsList.Add(twitterfeed);
                        boardrepo.addBoardTwitterFeed(twitterfeed);
                    }
                    output = true;
                }
            }


            return output;
        }
        public bool AddTwitterPageFeeds(string TwitterScreenName, Guid Boardtwitterprofileid,string LastTweetId)
        {
            bool output = false;
            List<Domain.Myfashion.Domain.Boardtwitterfeeds> twtFeedsList = new List<Domain.Myfashion.Domain.Boardtwitterfeeds>();
            string timeline = companypage.TwitterUserTimeLine(TwitterScreenName, LastTweetId);
            if (!string.IsNullOrEmpty(timeline) && !timeline.Equals("[]"))
            {
                foreach (JObject obj in JArray.Parse(timeline))
                {
                    Domain.Myfashion.Domain.Boardtwitterfeeds twitterfeed = new Domain.Myfashion.Domain.Boardtwitterfeeds();
                    twitterfeed.Id = Guid.NewGuid();


                    try
                    {
                        twitterfeed.Feedurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["url"].ToString();
                    }
                    catch
                    {
                        try
                        {
                            twitterfeed.Feedurl = JArray.Parse(obj["entities"]["urls"].ToString())[0]["expanded_url"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.Imageurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["media_url"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        foreach (JObject tag in JArray.Parse(obj["entities"]["hashtags"].ToString()))
                        {
                            try
                            {
                                twitterfeed.Hashtags = tag["text"].ToString() + ",";

                            }
                            catch (Exception e)
                            {
                                logger.Error(e.Message);
                                logger.Error(e.StackTrace);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Text = obj["text"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Retweetcount = Convert.ToInt32(obj["retweet_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Favoritedcount = Convert.ToInt32(obj["favorite_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
                        twitterfeed.Createdat = DateTime.ParseExact((string)obj["created_at"], Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Feedid = obj["id_str"].ToString();
                    }
                    catch(Exception ex) {
                        try
                        {
                            twitterfeed.Feedid = obj["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromId = obj["user"]["id_str"].ToString();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            twitterfeed.FromId = obj["user"]["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromName = obj["user"]["screen_name"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.FromPicUrl = obj["user"]["profile_image_url"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    twitterfeed.Isvisible = true;
                    twitterfeed.Twitterprofileid = Boardtwitterprofileid;
                    if (!boardrepo.checkTwitterFeedExists(twitterfeed.Feedid, Boardtwitterprofileid))
                    {
                        //twtFeedsList.Add(twitterfeed);
                        boardrepo.addBoardTwitterFeed(twitterfeed);
                    }
                         output = true;
                }
            }
           

            return output;
        }
        public bool AddBoardTwitterHashTagFeeds(string HashTag, Guid BoardTagid, string LastTweetId)
        {
            bool output = false;
            List<Domain.Myfashion.Domain.Boardtwitterfeeds> twtFeedsList = new List<Domain.Myfashion.Domain.Boardtwitterfeeds>();
            string timeline = companypage.TwitterBoardHashTagSearch(HashTag, LastTweetId);
            if (!string.IsNullOrEmpty(timeline) && !timeline.Equals("[]"))
            {
                foreach (JObject obj in JArray.Parse(timeline))
                {
                    Domain.Myfashion.Domain.Boardtwitterfeeds twitterfeed = new Domain.Myfashion.Domain.Boardtwitterfeeds();
                    twitterfeed.Id = Guid.NewGuid();


                    try
                    {
                        twitterfeed.Feedurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["url"].ToString();
                    }
                    catch
                    {
                        try
                        {
                            twitterfeed.Feedurl = JArray.Parse(obj["entities"]["urls"].ToString())[0]["expanded_url"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.Imageurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["media_url"].ToString();
                    }
                    catch
                    {
                        try
                        {
                            twitterfeed.Imageurl = JArray.Parse(obj["entities"]["media"].ToString())[0]["media_url"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        foreach (JObject tag in JArray.Parse(obj["entities"]["hashtags"].ToString()))
                        {
                            try
                            {
                                twitterfeed.Hashtags = tag["text"].ToString() + ",";

                            }
                            catch (Exception e)
                            {
                                logger.Error(e.Message);
                                logger.Error(e.StackTrace);
                            }
                        }
                    }
                    catch { }
                    try
                    {
                        twitterfeed.Text = obj["text"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Retweetcount = Convert.ToInt32(obj["retweet_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Favoritedcount = Convert.ToInt32(obj["favorite_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
                        twitterfeed.Createdat = DateTime.ParseExact((string)obj["created_at"], Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Feedid = obj["id_str"].ToString();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            twitterfeed.Feedid = obj["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromId = obj["user"]["id_str"].ToString();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            twitterfeed.FromId = obj["user"]["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromName = obj["user"]["screen_name"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.FromPicUrl = obj["user"]["profile_image_url"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    twitterfeed.Isvisible = true;
                    twitterfeed.Twitterprofileid = BoardTagid;
                    if (!boardrepo.checkTwitterFeedExists(twitterfeed.Feedid, BoardTagid))
                    {
                        //twtFeedsList.Add(twitterfeed);
                        boardrepo.addBoardTwitterFeed(twitterfeed);
                    }
                    output = true;
                }
            }
            //if (twtFeedsList.Count() > 0) 
            //{
            //    boardrepo.addBoardTwitterFeed(twtFeedsList);
            //}
            return output;
        }


        public bool AddTwitterHashTagFeeds(string HashTag, Guid BoardTagid,string LastTweetId)
        {
            bool output = false;
            List<Domain.Myfashion.Domain.Boardtwitterfeeds> twtFeedsList = new List<Domain.Myfashion.Domain.Boardtwitterfeeds>();
            string timeline = companypage.TwitterHashTagSearch(HashTag, LastTweetId);
            if (!string.IsNullOrEmpty(timeline) && !timeline.Equals("[]"))
            {
                foreach (JObject obj in JArray.Parse(timeline))
                {
                    Domain.Myfashion.Domain.Boardtwitterfeeds twitterfeed = new Domain.Myfashion.Domain.Boardtwitterfeeds();
                    twitterfeed.Id = Guid.NewGuid();


                    try
                    {
                        twitterfeed.Feedurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["url"].ToString();
                    }
                    catch
                    {
                        try
                        {
                            twitterfeed.Feedurl = JArray.Parse(obj["entities"]["urls"].ToString())[0]["expanded_url"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.Imageurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["media_url"].ToString();
                    }
                    catch 
                    {
                        try
                        {
                            twitterfeed.Imageurl = JArray.Parse(obj["entities"]["media"].ToString())[0]["media_url"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        foreach (JObject tag in JArray.Parse(obj["entities"]["hashtags"].ToString()))
                        {
                            try
                            {
                                twitterfeed.Hashtags = tag["text"].ToString() + ",";

                            }
                            catch (Exception e)
                            {
                                logger.Error(e.Message);
                                logger.Error(e.StackTrace);
                            }
                        }
                    }
                    catch { }
                    try
                    {
                        twitterfeed.Text = obj["text"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Retweetcount = Convert.ToInt32(obj["retweet_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Favoritedcount = Convert.ToInt32(obj["favorite_count"].ToString());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
                        twitterfeed.Createdat = DateTime.ParseExact((string)obj["created_at"], Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.Feedid = obj["id_str"].ToString();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            twitterfeed.Feedid = obj["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromId = obj["user"]["id_str"].ToString();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            twitterfeed.FromId = obj["user"]["id"].ToString();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                            logger.Error(e.StackTrace);
                        }
                    }
                    try
                    {
                        twitterfeed.FromName = obj["user"]["screen_name"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    try
                    {
                        twitterfeed.FromPicUrl = obj["user"]["profile_image_url"].ToString();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    twitterfeed.Isvisible = true;
                    twitterfeed.Twitterprofileid = BoardTagid;
                    if (!boardrepo.checkTwitterFeedExists(twitterfeed.Feedid, BoardTagid))
                    {
                        //twtFeedsList.Add(twitterfeed);
                        boardrepo.addBoardTwitterFeed(twitterfeed);
                    }
                    output = true;
                }
            }
            //if (twtFeedsList.Count() > 0) 
            //{
            //    boardrepo.addBoardTwitterFeed(twtFeedsList);
            //}
            return output;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateTwitterFeedsBycount() 
        {
          List<Domain.Myfashion.Domain.Boardtwitterfeeds> twitterfeeds =  boardrepo.getAlltwitterfeeds();
            TwitterAccountRepository twtaccrepo = new TwitterAccountRepository();
            string accesstoken = string.Empty;
            string secretKey = string.Empty;
            int Count = 0;
          foreach (var feed in twitterfeeds) 
          {
              string timeline = string.Empty;
              Domain.Myfashion.Domain.Boardtwitterfeeds twitterfeed = new Domain.Myfashion.Domain.Boardtwitterfeeds();

              List<Domain.Myfashion.Domain.TwitterAccount> twtacc = twtaccrepo.getAllTwitterAccountsOfUser(boardrepo.GetUserIdBytwitterFeedId(feed.Id));
              if (twtacc.Count > 0)
              {
                  timeline = companypage.TwitterTweetDetails(feed.Feedid, twtacc[0].OAuthToken, twtacc[0].OAuthSecret);
                  if (!string.IsNullOrEmpty(timeline)) 
                  {
                      accesstoken = twtacc[0].OAuthToken;
                      secretKey = twtacc[0].OAuthSecret;
                      Count = 0;
                  }

              }
              else 
              {
                  if (string.IsNullOrEmpty(accesstoken) && !string.IsNullOrEmpty(secretKey) && Count < 70)
                  {
                      timeline = companypage.TwitterTweetDetails(feed.Feedid, accesstoken, secretKey);
                      Count++;
                  }
                  else 
                  {
                      //timeline = companypage.TwitterTweetDetailsByAppOnlyKey(feed.Feedid); 
                  }
              }
              twitterfeed.Id = feed.Id;
              if (string.IsNullOrEmpty(timeline)) 
              {
                  continue;
              }
              JObject obj = JObject.Parse(timeline);
              try
              {
                  twitterfeed.Feedurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["url"].ToString();
              }
              catch
              {
                  try
                  {
                      twitterfeed.Feedurl = JArray.Parse(obj["entities"]["urls"].ToString())[0]["expanded_url"].ToString();
                  }
                  catch(Exception e) {
                      logger.Error(e.Message);
                      logger.Error(e.StackTrace);
                  }
              }
              try
              {
                  twitterfeed.Imageurl = JArray.Parse(obj["extended_entities"]["media"].ToString())[0]["media_url"].ToString();
              }
              catch (Exception e)
              {
                  logger.Error(e.Message);
                  logger.Error(e.StackTrace);
              }
              try
              {
                  foreach (JObject tag in JArray.Parse(obj["entities"]["hashtags"].ToString()))
                  {
                      try
                      {
                          twitterfeed.Hashtags = tag["text"].ToString() + ",";

                      }
                      catch (Exception e)
                      {
                          logger.Error(e.Message);
                          logger.Error(e.StackTrace);
                      }
                  }
              }
              catch { }
              try
              {
                  twitterfeed.Text = obj["text"].ToString();
              }
              catch (Exception e)
              {
                  logger.Error(e.Message);
                  logger.Error(e.StackTrace);
              }
              try
              {
                  twitterfeed.Retweetcount = Convert.ToInt32(obj["retweet_count"].ToString());
              }
              catch (Exception e)
              {
                  logger.Error(e.Message);
                  logger.Error(e.StackTrace);
              }
              try
              {
                  twitterfeed.Favoritedcount = Convert.ToInt32(obj["favorite_count"].ToString());
              }
              catch (Exception e)
              {
                  logger.Error(e.Message);
                  logger.Error(e.StackTrace);
              }
              try
              {
                  string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
                  twitterfeed.Createdat = DateTime.ParseExact((string)obj["created_at"], Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
              }
              catch (Exception e)
              {
                  logger.Error(e.Message);
                  logger.Error(e.StackTrace);
              }
              try
              {
                  twitterfeed.Feedid = obj["id_str"].ToString();
              }
              catch (Exception ex)
              {
                  try
                  {
                      twitterfeed.Feedid = obj["id"].ToString();
                  }
                  catch
                  {
                  }
              }
              try
              {
                  twitterfeed.FromId = obj["user"]["id_str"].ToString();
              }
              catch (Exception ex)
              {
                  try
                  {
                      twitterfeed.FromId = obj["user"]["id"].ToString();
                  }
                  catch (Exception e)
                  {
                      logger.Error(e.Message);
                      logger.Error(e.StackTrace);
                  }
              }
              try
              {
                  twitterfeed.FromName = obj["user"]["screen_name"].ToString();
              }
              catch (Exception e)
              {
                  logger.Error(e.Message);
                  logger.Error(e.StackTrace);
              }
              try
              {
                  twitterfeed.FromPicUrl = obj["user"]["profile_image_url"].ToString();
              }
              catch (Exception e)
              {
                  logger.Error(e.Message);
                  logger.Error(e.StackTrace);
              }
              twitterfeed.Isvisible = feed.Isvisible;
              twitterfeed.Twitterprofileid = feed.Twitterprofileid;
              boardrepo.updateTwitterFeed(twitterfeed);
              
          }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetCompanytwitterpage(string id)
        //{
        //    return new JavaScriptSerializer().Serialize(GetTwitterPage(id));
        //}


        //public Domain.Myfashion.Domain.Linkedinpage GetLinkedinPage(string id)
        //{
        //    string output = string.Empty;
        //    Domain.Myfashion.Domain.Linkedinpage linkedinpage = new Domain.Myfashion.Domain.Linkedinpage();
        //    try
        //    {
        //        Guid pageid = Guid.Empty;
        //        try
        //        {
        //            pageid = Guid.Parse(id);
        //        }
        //        catch { }
        //        if (pageid == Guid.Empty)
        //        {
        //            XmlNode ResultCompany = null;
        //            int followers = 0;
        //            string result = string.Empty;
        //            result = companypage.LinkedinSearch(id);
        //            XmlDocument XmlResult = new XmlDocument();
        //            XmlResult.Load(new StringReader(result));
        //            XmlNodeList Companies = XmlResult.SelectNodes("company-search/companies/company");
        //            foreach (XmlNode node in Companies)
        //            {
        //                if (Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText) > followers)
        //                {
        //                    ResultCompany = node;
        //                    followers = Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText);
        //                }
        //            }

        //            linkedinpage.Id = Guid.NewGuid();
        //            try
        //            {
        //                linkedinpage.UniversalName = ResultCompany.SelectSingleNode("universal-name").InnerText;
        //            }
        //            catch (Exception e)
        //            {
        //            }
        //            try
        //            {
        //                linkedinpage.Description = ResultCompany.SelectSingleNode("description").InnerText;
        //            }
        //            catch (Exception e)
        //            {
        //            }
        //            try
        //            {
        //                linkedinpage.FoundedYear = ResultCompany.SelectSingleNode("founded-year").InnerText;
        //            }
        //            catch (Exception e)
        //            {
        //            }
        //            try
        //            {
        //                linkedinpage.Follower = Convert.ToInt32(ResultCompany.SelectSingleNode("num-followers").InnerText);
        //            }
        //            catch (Exception e)
        //            {
        //            }

        //            linkedinpage.EntryDate = DateTime.UtcNow;
        //            try
        //            {
        //                linkedinpage.Employees = ResultCompany.SelectSingleNode("employee-count-range/name").InnerText;
        //            }
        //            catch (Exception e)
        //            {
        //            }
        //            try
        //            {
        //                linkedinpage.Url = ResultCompany.SelectSingleNode("website-url").InnerText;
        //            }
        //            catch (Exception e)
        //            {
        //            }

        //            linkedinpage.LinkedinpageId = ResultCompany.SelectSingleNode("id").InnerText;
        //            Domain.Myfashion.Domain.Linkedinpage linkpagetemp = ObjLinkedinPageRepository.getLinkedincompanyinfobycompanyId(linkedinpage.LinkedinpageId);
        //            if (linkpagetemp == null)
        //            {
        //                ObjLinkedinPageRepository.AddLinkedinCompanyInfo(linkedinpage);
        //            }
        //            else
        //            {
        //                linkedinpage = linkpagetemp;
        //            }
        //        }
        //        else
        //        {
        //            linkedinpage = ObjLinkedinPageRepository.getLinkedinPageInfo(pageid);
        //        }


        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return linkedinpage;
        //}

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetCompanayLinkedinPage(string id)
        //{
        //    return new JavaScriptSerializer().Serialize(GetLinkedinPage(id));

        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardInstagramAccount(string BoardId)
        {
            string output = string.Empty;
            try
            {
                Domain.Myfashion.Domain.Boardinstagramaccount objBoardInstagramPage = boardrepo.getBoardInstagramAccount(Guid.Parse(BoardId));

                return new JavaScriptSerializer().Serialize(objBoardInstagramPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardInstagramFeeds(string BoardInstagramAccountId)
        {
            string output = string.Empty;
            try
            {
                List<Domain.Myfashion.Domain.Boardinstagramfeeds> objBoardInstagramPage = boardrepo.getBoardInstagramfeeds(Guid.Parse(BoardInstagramAccountId));

                return new JavaScriptSerializer().Serialize(objBoardInstagramPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        public Domain.Myfashion.Domain.Boardinstagramaccount AddBoardInstagramPage(string InstagramPage, string BoardId)
        {
            string output = string.Empty;
            Domain.Myfashion.Domain.Boardinstagramaccount objInstagramPage = new Domain.Myfashion.Domain.Boardinstagramaccount();
            try
            {
                Guid pageId = Guid.Empty;
                try
                {
                    pageId = Guid.Parse(InstagramPage);
                    objInstagramPage.Boardid = Guid.Parse(BoardId);
                }
                catch (Exception e) { }

                if (pageId == Guid.Empty)
                {
                    JObject instagramPage = JObject.Parse(companypage.getInstagramCompanyPage(InstagramPage));
                    try
                    {
                        objInstagramPage.Url = instagramPage["data"]["website"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objInstagramPage.Profileid = instagramPage["data"]["id"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objInstagramPage.Profilepicurl = instagramPage["data"]["profile_picture"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objInstagramPage.Followscount = instagramPage["data"]["counts"]["follows"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objInstagramPage.Followedbycount = instagramPage["data"]["counts"]["followed_by"].ToString();
                    }
                    catch (Exception e) { }

                    try
                    {
                        objInstagramPage.Username = instagramPage["data"]["username"].ToString();
                    }
                    catch (Exception e) { }

                    try
                    {
                        objInstagramPage.Bio = instagramPage["data"]["bio"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objInstagramPage.Media = instagramPage["data"]["counts"]["media"].ToString();
                    }
                    catch (Exception e) { }

                    objInstagramPage.Entrydate = DateTime.UtcNow;
                    objInstagramPage.Id = Guid.NewGuid();
                    objInstagramPage.Boardid = Guid.Parse(BoardId);
                    boardrepo.addBoardInstagramAccount(objInstagramPage);
                }
                else
                {
                    //objInstagramPage = ObjInstagramPageRepository.getInstagramPageInfo(pageId);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                objInstagramPage = null;
                // return "Something Went Wrong";
            }
            return objInstagramPage;

        }



        public bool AddBoardInstagramPageFeeds(string InstagramPageId, Guid BoardInstagrampageId)
        {
            bool output = false;
            try
            {
                JObject recentactivities = JObject.Parse(companypage.getInstagramUserRecentActivities(InstagramPageId));
                foreach (JObject obj in JArray.Parse(recentactivities["data"].ToString()))
                {
                    Domain.Myfashion.Domain.Boardinstagramfeeds binstfeed = new Domain.Myfashion.Domain.Boardinstagramfeeds();
                    binstfeed.Id = Guid.NewGuid();
                    binstfeed.Instagramaccountid = BoardInstagrampageId;
                    binstfeed.Isvisible = true;
                    try
                    {
                        binstfeed.Imageurl = obj["images"]["standard_resolution"]["url"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.Link = obj["link"].ToString();
                    }
                    catch { }
                    try
                    {
                        foreach (JValue tag in JArray.Parse(obj["tags"].ToString()))
                        {
                            try
                            {
                                binstfeed.Tags = tag.ToString() + ",";
                            }
                            catch { }
                        }
                    }
                    catch { }
                    try
                    {
                        binstfeed.Createdtime = new DateTime(1970, 1, 1).AddSeconds(Convert.ToInt64(obj["created_time"].ToString()));
                    }
                    catch {
                        //binstfeed.Createdtime = DateTime.UtcNow;
                    }
                    try
                    {
                        binstfeed.Feedid = obj["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.FromId = obj["user"]["username"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.FromName = obj["user"]["full_name"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.FromPicUrl = obj["user"]["profile_picture"].ToString();
                    }
                    catch { }

                    if (!boardrepo.checkInstagramFeedExists(binstfeed.Feedid, BoardInstagrampageId))
                    {
                        boardrepo.addBoardInstagramFeeds(binstfeed);
                    }
                }
            }
            catch { }

            return output;
        }





        public bool AddBoardInstagramTagFeeds(string InstagramTag, Guid BoardInstagramTagId)
        {
            bool output = false;
            try
            {
                JObject recentactivities = JObject.Parse(companypage.InstagramTagSearch(InstagramTag));
                foreach (JObject obj in JArray.Parse(recentactivities["data"].ToString()))
                {
                    Domain.Myfashion.Domain.Boardinstagramfeeds binstfeed = new Domain.Myfashion.Domain.Boardinstagramfeeds();
                    binstfeed.Id = Guid.NewGuid();
                    binstfeed.Instagramaccountid = BoardInstagramTagId;
                    binstfeed.Isvisible = true;
                    try
                    {
                        binstfeed.Imageurl = obj["images"]["standard_resolution"]["url"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.Link = obj["link"].ToString();
                    }
                    catch { }
                    try
                    {
                        foreach (JValue tag in JArray.Parse(obj["tags"].ToString()))
                        {
                            try
                            {
                                binstfeed.Tags = tag.ToString() + ",";
                            }
                            catch { }
                        }
                    }
                    catch { }
                    try
                    {
                        binstfeed.Createdtime = new DateTime(1970, 1, 1).AddSeconds(Convert.ToInt64(obj["created_time"].ToString()));
                    }
                    catch
                    {
                        //binstfeed.Createdtime = DateTime.UtcNow;
                    }
                    try
                    {
                        binstfeed.Feedid = obj["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.FromId = obj["user"]["username"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.FromName = obj["user"]["full_name"].ToString();
                    }
                    catch { }
                    try
                    {
                        binstfeed.FromPicUrl = obj["user"]["profile_picture"].ToString();
                    }
                    catch { }
                    if (!boardrepo.checkInstagramFeedExists(binstfeed.Feedid, BoardInstagramTagId))
                    {
                        boardrepo.addBoardInstagramFeeds(binstfeed);
                    }
                }
            }
            catch { }

            return output;
        }




        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateInstagramFeeds(string Key)
        {
            if (Key.Equals("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988"))
            {
                List<Domain.Myfashion.Domain.Boardinstagramaccount> boardinstgramacc = boardrepo.getAllBoardInstagramAccounts();
                foreach (Domain.Myfashion.Domain.Boardinstagramaccount instacc in boardinstgramacc)
                {
                    if (instacc.Bio.Equals("tag"))
                    {
                        AddBoardInstagramTagFeeds(instacc.Profileid, instacc.Id);
                    }
                    else 
                    {
                        AddBoardInstagramPageFeeds(instacc.Profileid, instacc.Id);
                    }
                }
            }
        }



        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetCompanayInstagramPage(string id)
        //{
        //    return new JavaScriptSerializer().Serialize(GetInstagramPage(id));

        //}



        //public Domain.Myfashion.Domain.Tumblrcompanyinfo GetTumblrPage(string id)
        //{
        //    string output = string.Empty;
        //    Domain.Myfashion.Domain.Tumblrcompanyinfo objTumblrPage = new Domain.Myfashion.Domain.Tumblrcompanyinfo();
        //    try
        //    {
        //        Guid pageId = Guid.Empty;
        //        try
        //        {
        //            pageId = Guid.Parse(id);
        //        }
        //        catch (Exception e) { }

        //        if (pageId == Guid.Empty)
        //        {
        //            JObject tumblrPage = JObject.Parse(companypage.TumblrSearch(id));
        //            try
        //            {
        //                objTumblrPage.Url = tumblrPage["response"]["blog"]["url"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objTumblrPage.Description = tumblrPage["response"]["blog"]["description"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objTumblrPage.Name = tumblrPage["response"]["blog"]["name"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objTumblrPage.Posts = tumblrPage["response"]["blog"]["posts"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objTumblrPage.TotalPosts = tumblrPage["response"]["blog"]["total_posts"].ToString();
        //            }
        //            catch (Exception e) { }



        //            objTumblrPage.EntryDate = DateTime.UtcNow;
        //            objTumblrPage.Id = Guid.NewGuid();
        //            Domain.Myfashion.Domain.Tumblrcompanyinfo tumblrpage = objTumblrRepository.getTumblrPageInfoByPageId(objTumblrPage.Url);
        //            if (tumblrpage == null)
        //            {
        //                objTumblrRepository.AddTumblrCompanyInfo(objTumblrPage);
        //            }
        //            else
        //            {
        //                objTumblrPage = tumblrpage;
        //            }

        //        }
        //        else
        //        {
        //            objTumblrPage = objTumblrRepository.getTumblrPageInfo(pageId);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        //Console.WriteLine(ex.StackTrace);
        //        //return "Something Went Wrong";
        //    }
        //    return objTumblrPage;

        //}



        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetCompanayTumblrPage(string id)
        //{
        //    return new JavaScriptSerializer().Serialize(GetTumblrPage(id));

        //}


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardGplusAccount(string BoardId)
        {
            string output = string.Empty;
            try
            {
                Domain.Myfashion.Domain.Boardgplusaccount objBoardGplusPage = boardrepo.getBoardGplusAccount(Guid.Parse(BoardId));

                return new JavaScriptSerializer().Serialize(objBoardGplusPage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetBoardGplusFeeds(string BoardInstagramAccountId)
        {
            string output = string.Empty;
            try
            {
                List<Domain.Myfashion.Domain.Boardgplusfeeds> objBoardGplusPagefeeds = boardrepo.getBoardGplusfeeds(Guid.Parse(BoardInstagramAccountId));

                return new JavaScriptSerializer().Serialize(objBoardGplusPagefeeds);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        public Domain.Myfashion.Domain.Boardgplusaccount AddBoardGplusPage(string GplusPageId, string BoardId)
        {
            string output = string.Empty;
            Domain.Myfashion.Domain.Boardgplusaccount objGplusPage = new Domain.Myfashion.Domain.Boardgplusaccount();
            try
            {
                Guid pageId = Guid.Empty;
                try
                {
                    pageId = Guid.Parse(GplusPageId);
                }
                catch (Exception e) { }

                if (pageId == Guid.Empty)
                {
                    JObject GplusPage = JObject.Parse(companypage.GooglePlusgetUser(GplusPageId));
                    try
                    {
                        objGplusPage.Nickname = GplusPage["nickname"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objGplusPage.Pageid = GplusPage["id"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objGplusPage.Displayname = GplusPage["displayName"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objGplusPage.Aboutme = GplusPage["aboutMe"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        JArray urls = JArray.Parse(GplusPage["urls"].ToString());
                        objGplusPage.Pageurl = urls[0]["value"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objGplusPage.Profileimageurl = GplusPage["image"]["url"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objGplusPage.Tagline = GplusPage["tagline"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        objGplusPage.Coverphotourl = GplusPage["cover"]["coverPhoto"]["url"].ToString();
                    }
                    catch (Exception e) { }

                    objGplusPage.Entrydate = DateTime.UtcNow;
                    objGplusPage.Boardid = Guid.Parse(BoardId);
                    objGplusPage.Id = Guid.NewGuid();
                    ////Domain.Myfashion.Domain.Gpluscompanypage gpluspage = ObjGooglePlusInfoRepository.getGPlusPageInfoByPageId(objGplusPage.Pageid);
                    //if (gpluspage == null)
                    //{
                    boardrepo.addBoardGPlusAccount(objGplusPage);
                    //}
                    //else
                    //{
                    //    objGplusPage = gpluspage;
                    //}

                }
                else
                {
                    // objGplusPage = ObjGooglePlusInfoRepository.getGPlusPageInfo(pageId);
                    objGplusPage = null;
                }

            }
            catch (Exception ex)
            {

                //Console.WriteLine(ex.StackTrace);
                //return "Something Went Wrong";
            }
            return objGplusPage;

        }

        public bool AddBoardGplusFeeds(string GplusPageId, Guid BoardId)
        {
            bool output = false;
            try
            {
                JObject RecentActivities = JObject.Parse(companypage.GooglePlusgetUserRecentActivities(GplusPageId));
                foreach (JObject obj in RecentActivities["items"])
                {
                    Domain.Myfashion.Domain.Boardgplusfeeds bgpfeed = new Domain.Myfashion.Domain.Boardgplusfeeds();
                    bgpfeed.Id = Guid.NewGuid();
                    bgpfeed.Gplusboardaccprofileid = BoardId;
                    try
                    {
                        bgpfeed.Feedlink = obj["url"].ToString();
                    }
                    catch { }
                    try
                    {
                        foreach (JObject att in JArray.Parse(obj["object"]["attachments"].ToString()))
                        {
                            if (att["objectType"].ToString().Equals("photo"))
                            {

                                bgpfeed.Imageurl = att["fullImage"]["url"].ToString() + ",";
                            }
                        }
                    }
                    catch { }
                    try 
                    {
                        bgpfeed.Publishedtime = DateTime.Parse(obj["published"].ToString());
                    }
                    catch { }
                    try
                    {
                        bgpfeed.Title =obj["title"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.Feedid = obj["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.FromId = obj["actor"]["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.FromName = obj["actor"]["displayName"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.FromPicUrl = obj["actor"]["image"]["url"].ToString();
                    }
                    catch { }
                    if (!boardrepo.checkgPlusFeedExists(bgpfeed.Feedid, BoardId))
                    {
                        boardrepo.addBoardGPlusFeed(bgpfeed);
                    }
                }
            }
            catch { }



            return output;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateGplusFeeds(string Key)
        {
            if (Key.Equals("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988"))
            {
                List<Domain.Myfashion.Domain.Boardgplusaccount> boardgplusacc = boardrepo.getAllBoardGplusAccounts();
                foreach (Domain.Myfashion.Domain.Boardgplusaccount gplusacc in boardgplusacc)
                {
                    if (gplusacc.Pageid.Equals("tag"))
                    {
                        AddBoardGplusTagFeeds(gplusacc.Nickname, gplusacc.Id);
                    }
                    else 
                    {
                        AddBoardGplusFeeds(gplusacc.Pageid, gplusacc.Id);
                    }
                }
            }
        }

        public bool AddBoardGplusTagFeeds(string GplusTagId, Guid BoardId)
        {
            bool output = false;
            try
            {
                JObject RecentActivities = JObject.Parse(companypage.GooglePlusgetUserRecentActivitiesByHashtag(GplusTagId));
                foreach (JObject obj in RecentActivities["items"])
                {
                    Domain.Myfashion.Domain.Boardgplusfeeds bgpfeed = new Domain.Myfashion.Domain.Boardgplusfeeds();
                    bgpfeed.Id = Guid.NewGuid();
                    bgpfeed.Gplusboardaccprofileid = BoardId;
                    try
                    {
                        bgpfeed.Feedlink = obj["url"].ToString();
                    }
                    catch { }
                    try
                    {
                        foreach (JObject att in JArray.Parse(obj["object"]["attachments"].ToString()))
                        {
                            if (att["objectType"].ToString().Equals("photo"))
                            {

                                bgpfeed.Imageurl = att["fullImage"]["url"].ToString() + ",";
                            }
                        }
                    }
                    catch { }
                    try
                    {
                        bgpfeed.Publishedtime = DateTime.Parse(obj["published"].ToString());
                    }
                    catch { }
                    try
                    {
                        bgpfeed.Title = obj["title"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.Feedid = obj["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.FromId = obj["actor"]["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.FromName = obj["actor"]["displayName"].ToString();
                    }
                    catch { }
                    try
                    {
                        bgpfeed.FromPicUrl = obj["actor"]["image"]["url"].ToString();
                    }
                    catch { }
                    if (!boardrepo.checkgPlusFeedExists(bgpfeed.Feedid, BoardId))
                    {
                        boardrepo.addBoardGPlusFeed(bgpfeed);
                    }
                }
            }
            catch { }



            return output;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getBoardGplusfeedsbyrange(string BoardGplusprofileId, string _noOfDataToSkip, string _noOfResultsFromTop)
        {
            try
            {
                List<Domain.Myfashion.Domain.Boardgplusfeeds> objBoardGplusPagefeeds = boardrepo.getBoardGplusfeedsbyrange(Guid.Parse(BoardGplusprofileId), Convert.ToInt32(_noOfDataToSkip), Convert.ToInt32(_noOfResultsFromTop));

                return new JavaScriptSerializer().Serialize(objBoardGplusPagefeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getBoardInstagramfeedsbyrange(string BoardInstagramAccountId, string _noOfDataToSkip, string _noOfResultsFromTop)
        {
            try
            {
                List<Domain.Myfashion.Domain.Boardinstagramfeeds> objBoardinstaPagefeeds = boardrepo.getBoardInstagramfeedsbyrange(Guid.Parse(BoardInstagramAccountId), Convert.ToInt32(_noOfDataToSkip), Convert.ToInt32(_noOfResultsFromTop));

                return new JavaScriptSerializer().Serialize(objBoardinstaPagefeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getBoardTwitterfeedsbyrange(string BoardtwitterprofileId, string _noOfDataToSkip, string _noOfResultsFromTop)
        {
            try
            {
                List<Domain.Myfashion.Domain.Boardtwitterfeeds> objBoardtwtPagefeeds = boardrepo.getBoardTwitterfeedsbyrange(Guid.Parse(BoardtwitterprofileId), Convert.ToInt32(_noOfDataToSkip), Convert.ToInt32(_noOfResultsFromTop));

                return new JavaScriptSerializer().Serialize(objBoardtwtPagefeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getBoardFbfeedsbyrange(string BoardfbprofileId, string _noOfDataToSkip, string _noOfResultsFromTop)
        {
            try
            {
                List<Domain.Myfashion.Domain.Boardfbfeeds> objBoardfbPagefeeds = boardrepo.getBoardFbfeedsbyrange(Guid.Parse(BoardfbprofileId), Convert.ToInt32(_noOfDataToSkip), Convert.ToInt32(_noOfResultsFromTop));

                return new JavaScriptSerializer().Serialize(objBoardfbPagefeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        //public Domain.Myfashion.Domain.Gpluscompanypage GetGplusPage(string id)
        //{
        //    string output = string.Empty;
        //    Domain.Myfashion.Domain.Gpluscompanypage objGplusPage = new Domain.Myfashion.Domain.Gpluscompanypage();
        //    try
        //    {
        //        Guid pageId = Guid.Empty;
        //        try
        //        {
        //            pageId = Guid.Parse(id);
        //        }
        //        catch (Exception e) { }

        //        if (pageId == Guid.Empty)
        //        {
        //            JObject GplusPage = JObject.Parse(companypage.GooglePlusSearch(id));
        //            try
        //            {
        //                objGplusPage.Nickname = GplusPage["nickname"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objGplusPage.Pageid = GplusPage["id"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objGplusPage.DisplayName = GplusPage["displayName"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objGplusPage.AboutMe = GplusPage["aboutMe"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                JArray urls = JArray.Parse(GplusPage["urls"].ToString());
        //                objGplusPage.PageUrl = urls[0]["value"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objGplusPage.ImageUrl = GplusPage["image"]["url"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objGplusPage.Tagline = GplusPage["tagline"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objGplusPage.CoverPhotoUrl = GplusPage["cover"]["coverPhoto"]["url"].ToString();
        //            }
        //            catch (Exception e) { }

        //            objGplusPage.EntryDate = DateTime.UtcNow;
        //            objGplusPage.Id = Guid.NewGuid();
        //            Domain.Myfashion.Domain.Gpluscompanypage gpluspage = ObjGooglePlusInfoRepository.getGPlusPageInfoByPageId(objGplusPage.Pageid);
        //            if (gpluspage == null)
        //            {
        //                ObjGooglePlusInfoRepository.AddGPlusCompanyInfo(objGplusPage);
        //            }
        //            else
        //            {
        //                objGplusPage = gpluspage;
        //            }

        //        }
        //        else
        //        {
        //            objGplusPage = ObjGooglePlusInfoRepository.getGPlusPageInfo(pageId);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        //Console.WriteLine(ex.StackTrace);
        //        //return "Something Went Wrong";
        //    }
        //    return objGplusPage;

        //}



        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetCompanayGPlusPage(string id)
        //{
        //    return new JavaScriptSerializer().Serialize(GetGplusPage(id));

        //}




        //public Domain.Myfashion.Domain.YoutubeChannel GetYoutubeChannel(string id)
        //{

        //    Domain.Myfashion.Domain.YoutubeChannel objYoutubeChannel = new Domain.Myfashion.Domain.YoutubeChannel();
        //    try
        //    {
        //        Guid pageId = Guid.Empty;
        //        try
        //        {
        //            pageId = Guid.Parse(id);
        //        }
        //        catch (Exception e) { }

        //        if (pageId == Guid.Empty)
        //        {
        //            string result = companypage.YoutubeSearch(id);
        //            if (!result.StartsWith("["))
        //                result = "[" + result + "]";
        //            JArray youtubechannels = JArray.Parse(result);
        //            JObject resultPage = (JObject)youtubechannels[0];
        //            JObject YtubeChannel = (JObject)resultPage["items"][0];
        //            try
        //            {
        //                objYoutubeChannel.Channelid = YtubeChannel["id"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.Likesid = YtubeChannel["contentDetails"]["relatedPlaylists"]["likes"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.Favoritesid = YtubeChannel["contentDetails"]["relatedPlaylists"]["uploads"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.Uploadsid = YtubeChannel["contentDetails"]["relatedPlaylists"]["uploads"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.Googleplususerid = YtubeChannel["contentDetails"]["googlePlusUserId"].ToString();
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.ViewCount = Convert.ToInt32(YtubeChannel["statistics"]["viewCount"].ToString());
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.CommentCount = Convert.ToInt32(YtubeChannel["statistics"]["commentCount"].ToString());
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.SubscriberCount = Convert.ToInt32(YtubeChannel["statistics"]["subscriberCount"].ToString());
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.VideoCount = Convert.ToInt32(YtubeChannel["statistics"]["videoCount"].ToString());
        //            }
        //            catch (Exception e) { }
        //            try
        //            {
        //                objYoutubeChannel.HiddenSubscriberCount = Convert.ToInt32(YtubeChannel["statistics"]["hiddenSubscriberCount"].ToString());
        //            }
        //            catch (Exception e)
        //            {
        //                objYoutubeChannel.HiddenSubscriberCount = 0;
        //            }
        //            objYoutubeChannel.Id = Guid.NewGuid();
        //            Domain.Myfashion.Domain.YoutubeChannel ychannel = ObjYoutubeChannelRepository.getYoutubeChannelDetailsById(objYoutubeChannel.Channelid);
        //            if (ychannel == null || ychannel.Id == Guid.Empty)
        //            {
        //                YoutubeChannelRepository.Add(objYoutubeChannel);
        //            }
        //            else
        //            {
        //                objYoutubeChannel = ychannel;
        //            }

        //        }
        //        else
        //        {
        //            objYoutubeChannel = ObjYoutubeChannelRepository.getYoutubeChannelById(pageId);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        //Console.WriteLine(ex.StackTrace);
        //        //return "Something Went Wrong";
        //    }
        //    return objYoutubeChannel;

        //}



        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetCompanayYoutubeChannel(string id)
        //{
        //    return new JavaScriptSerializer().Serialize(GetYoutubeChannel(id));

        //}







        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getPopulartweets(string BoardTwitterProfileId) 
        {
            string Output = string.Empty;
            List<Domain.Myfashion.Domain.Boardtwitterfeeds> twitterfeeds = boardrepo.getPopularTweets(Guid.Parse(BoardTwitterProfileId));


            return Output;
        }
        #endregion




    }
}
