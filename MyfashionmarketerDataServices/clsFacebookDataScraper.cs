using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SocioboardDtaService.classes;
using SocioboardDtaService;
using System.Web.Script.Serialization;
using System.IO;

namespace SocioboardDataServices
{
    public class clsFacebookDataScraper
    {
        private string user_id = string.Empty;
        //private string pageid = string.Empty;
        //public GlobusHttpHelper objGlobusHttpHelper = new GlobusHttpHelper();
        public void LoginUsingGlobusHttp(ref FacebookUser facebookUser)
        {
            ///Sign In
            try
            {
                GlobusHttpHelper objGlobusHttpHelper = facebookUser.globusHttpHelper;
                #region Post variable

                string fbpage_id = string.Empty;
                string fb_dtsg = string.Empty;
                string __user = string.Empty;
                string xhpc_composerid = string.Empty;
                string xhpc_targetid = string.Empty;
                string xhpc_composerid12 = string.Empty;
                #endregion

                int intProxyPort = 80;

                if (!string.IsNullOrEmpty(facebookUser.proxyport) && Utils.IdCheck.IsMatch(facebookUser.proxyport))
                {
                    intProxyPort = int.Parse(facebookUser.proxyport);
                }
                Console.WriteLine("Logging in with " + facebookUser.username);
                Console.WriteLine("Logging in with " + facebookUser.username);
                string pageSource = string.Empty;
                try
                {
                    pageSource = objGlobusHttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/login.php"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }

                if (pageSource == null || string.IsNullOrEmpty(pageSource))
                {
                    pageSource = objGlobusHttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/login.php"));
                }

                if (pageSource == null)
                {
                    pageSource = objGlobusHttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/login.php"));
                    return;
                }

                string valueLSD = GlobusHttpHelper.GetParamValue(pageSource, "lsd");
                string ResponseLogin = string.Empty;
                try
                {
                    ResponseLogin = objGlobusHttpHelper.postFormData(new Uri("https://www.facebook.com/login.php?login_attempt=1"), "charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "&locale=en_US&email=" + facebookUser.username.Split('@')[0].Replace("+", "%2B") + "%40" + facebookUser.username.Split('@')[1] + "&pass=" + Uri.EscapeDataString(facebookUser.password) + "&persistent=1&default_persistent=1&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "");           //"https://www.facebook.com/login.php?login_attempt=1"
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }
                if (string.IsNullOrEmpty(ResponseLogin))
                {
                    ResponseLogin = objGlobusHttpHelper.postFormData(new Uri("https://www.facebook.com/login.php?login_attempt=1"), "charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "&locale=en_US&email=" + facebookUser.username.Split('@')[0].Replace("+", "%2B") + "%40" + facebookUser.username.Split('@')[1] + "&pass=" + Uri.EscapeDataString(facebookUser.password) + "&persistent=1&default_persistent=1&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "");           //"https://www.facebook.com/login.php?login_attempt=1"
                }
                if (ResponseLogin == null)
                {
                    return;
                }

                string loginStatus = "";
                if (CheckLogin(ResponseLogin, facebookUser.username, facebookUser.password, facebookUser.proxyip, facebookUser.proxyport, facebookUser.proxyusername, facebookUser.proxypassword, ref loginStatus))
                {
                    Console.WriteLine("Logged in with Username : " + facebookUser.username);
                    Console.WriteLine("Logged in with Username : " + facebookUser.username);

                    facebookUser.isloggedin = true;

                }
                else
                {
                    Console.WriteLine("Couldn't login with Username : " + facebookUser.username);
                    facebookUser.isloggedin = false;


                    if (loginStatus == "account has been disabled")
                    {
                        // GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_DisabledAccount);
                    }

                    if (loginStatus == "Please complete a security check")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_SecurityCheckAccounts);
                    }


                    if (loginStatus == "Your account is temporarily locked")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_TemporarilyLockedAccount);

                    }
                    if (loginStatus == "have been blocked")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_havebeenblocked);

                    }
                    if (loginStatus == "For security reasons your account is temporarily locked")
                    {
                        // GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_SecurityCheckAccountsforsecurityreason);
                    }

                    if (loginStatus == "Account Not Confirmed")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_AccountNotConfirmed);
                    }
                    if (loginStatus == "Temporarily Blocked for 30 Days")
                    {
                        // GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_30daysBlockedAccount);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);

            }
        }

        public bool CheckLogin(string response, string username, string password, string proxyAddress, string proxyPort, string proxyUsername, string proxyPassword, ref string loginStatus)
        {

            try
            {
                if (!string.IsNullOrEmpty(response))
                {


                    if (response.ToLower().Contains("unusual login activity"))
                    {
                        loginStatus = "unusual login activity";
                        Console.WriteLine("Unusual Login Activity: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("incorrect username"))
                    {
                        loginStatus = "incorrect username";
                        Console.WriteLine("Incorrect username: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("Choose a verification method".ToLower()))
                    {
                        loginStatus = "Choose a verification method";
                        Console.WriteLine("Choose a verification method: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("not logged in".ToLower()))
                    {
                        loginStatus = "not logged in";
                        Console.WriteLine("not logged in: " + username);
                        return false;
                    }
                    if (response.Contains("Please log in to continue".ToLower()))
                    {
                        loginStatus = "Please log in to continue";
                        Console.WriteLine("Please log in to continue: " + username);
                        return false;
                    }
                    if (response.Contains("re-enter your password"))
                    {
                        loginStatus = "re-enter your password";
                        Console.WriteLine("Wrong password for: " + username);
                        return false;
                    }
                    if (response.Contains("Incorrect Email"))
                    {
                        loginStatus = "Incorrect Email";
                        Console.WriteLine("Incorrect email: " + username);

                        try
                        {
                            ///Write Incorrect Emails in text file
                            //GlobusFileHelper.AppendStringToTextfileNewLine(username + ":" + password, incorrectEmailFilePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                        return false;
                    }
                    if (response.Contains("have been blocked"))
                    {
                        loginStatus = "have been blocked";
                        Console.WriteLine("you have been blocked: " + username);
                        return false;
                    }
                    if (response.Contains("account has been disabled"))
                    {
                        loginStatus = "account has been disabled";
                        Console.WriteLine("your account has been disabled: " + username);
                        return false;
                    }
                    if (response.Contains("Please complete a security check"))
                    {
                        loginStatus = "Please complete a security check";
                        Console.WriteLine("Please complete a security check: " + username);
                        return false;
                    }
                    if (response.Contains("Please complete a security check"))
                    {
                        loginStatus = "Please complete a security check";
                        Console.WriteLine("You must log in to see this page: " + username);
                        return false;
                    }
                    if (response.Contains("<input value=\"Sign Up\" onclick=\"RegistrationBootloader.bootloadAndValidate();"))
                    {
                        loginStatus = "RegistrationBootloader.bootloadAndValidate()";
                        Console.WriteLine("Not logged in with: " + username);
                        return false;
                    }
                    if (response.Contains("Account Not Confirmed"))
                    {
                        loginStatus = "Account Not Confirmed";
                        Console.WriteLine("Account Not Confirmed " + username);
                        return false;
                    }
                    if (response.Contains("Your account is temporarily locked"))
                    {
                        loginStatus = "Your account is temporarily locked";
                        Console.WriteLine("Your account is temporarily locked: " + username);
                        return false;
                    }
                    if (response.Contains("Your account has been temporarily suspended"))
                    {
                        loginStatus = "Your account has been temporarily suspended";
                        Console.WriteLine("Your account has been temporarily suspended: " + username);
                        return false;
                    }
                    if (response.Contains("You must log in to see this page"))
                    {
                        Console.WriteLine("You must log in to see this page: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("you must log in to see this page"))
                    {
                        Console.WriteLine("You must log in to see this page: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("you entered an old password"))
                    {
                        loginStatus = "you entered an old password";
                        Console.WriteLine("You Entered An Old Password: " + username);
                        return false;
                    }
                    if (response.Contains("For security reasons your account is temporarily locked"))
                    {
                        loginStatus = "For security reasons your account is temporarily locked";
                        Console.WriteLine("For security reasons your account is temporarily locked: " + username);
                        return false;
                    }
                    if (response.Contains("Please Verify Your Identity") || response.Contains("please Verify Your Identity"))
                    {
                        loginStatus = "Please Verify Your Identity";
                        Console.WriteLine("Please Verify Your Identity: " + username);
                        return false;
                    }
                    if (response.Contains("Temporarily Blocked for 30 Days"))
                    {
                        loginStatus = "Temporarily Blocked for 30 Days";
                        Console.WriteLine("You're Temporarily Blocked for 30 Days: " + username);
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return true;
        }

        public void GetFbPost(string UserId, string FbUserId)
        {
            string fbsourcehtml = string.Empty;
            string fbloginuserid = string.Empty;
            string posturl = string.Empty;
            string ajaxreq = string.Empty;
            GlobusHttpHelper objGlobusHttpHelper = new GlobusHttpHelper();
            #region fb_login
            FacebookUser fbuser = new FacebookUser();
            string FbUserAccount = GetFBUser();
            string[] fb_user = Regex.Split(FbUserAccount,"::");
            fbuser.username = "avinash.verma@globussoft.com";
            fbuser.password = @"!N)\+1S?Y5)JuGS";

            //fbuser.username = fb_user[0];
            //fbuser.password = fb_user[1];
         
            fbuser.globusHttpHelper = objGlobusHttpHelper;
            LoginUsingGlobusHttp(ref fbuser);
            #endregion
            objGlobusHttpHelper = fbuser.globusHttpHelper;
            fbsourcehtml = objGlobusHttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com"));
            fbloginuserid = GlobusHttpHelper.GetParamValue(fbsourcehtml, "user"); 
          
            user_id = fbloginuserid;
            #region Get_1stpage data
            string mainurl = "https://www.facebook.com/" + FbUserId;
            string pageid = FbUserId;
            //string mainurl = "https://www.facebook.com/PequeninColombia";
            string mainhtml = objGlobusHttpHelper.getHtmlfromUrl(new Uri(mainurl));
            //string pageid = socioHelper.getBetween(mainhtml, "data-profileid=\"", "\" data-ownerid");
            string[] splitmainhtml = System.Text.RegularExpressions.Regex.Split(mainhtml, "feedbacktargets");
            foreach (string item in splitmainhtml)
            {
                if (!item.Contains("<!DOCTYPE html>"))
                {
                    GetPostCommentGraphApi(item, ref objGlobusHttpHelper, FbUserId, UserId);
                }
            }
            #endregion
            string[] ajaxarr = new string[3];
            string ajaxreq1 = "https://www.facebook.com/ajax/pagelet/generic.php/PagePostsSectionPagelet?data={\"segment_index\":1,\"page_index\":0,\"page\":" + pageid + ",\"column\":\"main\",\"post_section\":{\"profile_id\":" + pageid + ",\"start\":0,\"end\":1414825199,\"query_type\":36,\"filter\":1,\"is_pages_redesign\":true},\"section_index\":0,\"hidden\":false,\"posts_loaded\":3,\"show_all_posts\":false,\"pager_fired_on_init\":true}&__user=" + fbloginuserid + "&__a=1&__dyn=7n8ajEBQ9FoBZypQ9UoHFaeFDzECiq78hACF3pqzCC-C26m6oKexm49UJ6K4ahpoW8xG&__req=5&__rev=1458973";
            string ajaxreq2 = "https://www.facebook.com/ajax/pagelet/generic.php/PagePostsSectionPagelet?data={\"segment_index\":0,\"page_index\":1,\"page\":" + pageid + ",\"column\":\"main\",\"post_section\":{\"profile_id\":" + pageid + ",\"start\":0,\"end\":1414825199,\"query_type\":36,\"filter\":1,\"is_pages_redesign\":true},\"section_index\":0,\"hidden\":false,\"posts_loaded\":9,\"show_all_posts\":false,\"pager_fired_on_init\":true}&__user=" + fbloginuserid + "&__a=1&__dyn=7n8ajEBQ9FoBZypQ9UoHFaeFDzECiq78hACF3pqzCC-C26m6oKexm49UJ6K4ahpoW8xG&__req=5&__rev=1458973";
            string ajaxreq3 = "https://www.facebook.com/ajax/pagelet/generic.php/PagePostsSectionPagelet?data={\"segment_index\":1,\"page_index\":1,\"page\":" + pageid + ",\"column\":\"main\",\"post_section\":{\"profile_id\":" + pageid + ",\"start\":0,\"end\":1414825199,\"query_type\":36,\"filter\":1,\"is_pages_redesign\":true},\"section_index\":0,\"hidden\":false,\"posts_loaded\":15,\"show_all_posts\":false,\"pager_fired_on_init\":true}&__user=" + fbloginuserid + "&__a=1&__dyn=7n8ajEBQ9FoBZypQ9UoHFaeFDzECiq78hACF3pqzCC-C26m6oKexm49UJ6K4ahpoW8xG&__req=5&__rev=1458973";
            ajaxarr[0] = ajaxreq1;
            ajaxarr[1] = ajaxreq2;
            ajaxarr[2] = ajaxreq3;
            foreach (string item in ajaxarr)
            {
                string itemhtml = objGlobusHttpHelper.getHtmlfromUrl(new Uri(item));
                string[] splithtml = System.Text.RegularExpressions.Regex.Split(itemhtml, "feedbacktargets");
                foreach (string item_htm in splithtml)
                {
                    if (!item_htm.Contains("for (;;);"))
                    {
                        GetPostCommentGraphApi(item_htm, ref objGlobusHttpHelper, pageid, UserId);
                    }
                }
            }
            int segment_index = 0;
            int page_index = 0;
            int posts_loaded = 0;
            int count= 0;
            bool istrue = true;
            while (istrue)
            {
                ajaxreq = "https://www.facebook.com/ajax/pagelet/generic.php/PagePostsSectionPagelet?data={\"segment_index\":" + segment_index.ToString() + ",\"page_index\":" + page_index.ToString() + ",\"page\":" + pageid + ",\"column\":\"main\",\"post_section\":{\"profile_id\":" + pageid + ",\"start\":1388563200,\"end\":1420099199,\"query_type\":8,\"filter\":1,\"is_pages_redesign\":true,\"filter_after_timestamp\":1412809199},\"section_index\":1,\"hidden\":false,\"posts_loaded\":" + posts_loaded.ToString() + ",\"show_all_posts\":false}&__user=" + fbloginuserid + "&__a=1&__dyn=7n8ajEBQ9FoBZypQ9UoHFaeFDzECiq78hACF3pqzCC-C26m6oKexm49UJ6K4ahpoW8xG&__req=d&__rev=1458973";
                mainhtml = objGlobusHttpHelper.getHtmlfromUrl(new Uri(ajaxreq));
                string[] splithtml = System.Text.RegularExpressions.Regex.Split(mainhtml, "feedbacktargets");
                foreach (string item in splithtml)
                {
                    if (!item.Contains("for (;;);"))
                    {
                        GetPostCommentGraphApi(item, ref objGlobusHttpHelper, FbUserId, UserId);
                    }
                }
                if (!mainhtml.Contains("posts"))
                {
                    segment_index = 0;
                    page_index++;
                    posts_loaded = 0;
                }
                segment_index++;
                posts_loaded += 6;
                count++;
                if (count > 10)
                {
                    istrue = false;
                }
            }
        }
        public void GetPostCommentParsing(string posturl, ref GlobusHttpHelper objGlobusHttpHelper, string pageid, string UserId)
        {
            #region Variables resion
            string posthtml = string.Empty;
            string posturl1 = string.Empty;
            string post = string.Empty;
            int like = 0;
            string like_id = string.Empty;
            string like_name = string.Empty;
            int comment = 0;
            string commentid = string.Empty;
            string commentmsg = string.Empty;
            string commentcreated_time = string.Empty;
            string commentlike_count = string.Empty;
            string user_likes = string.Empty;
            int share = 0;
            string post_date = string.Empty;
            string linkurl = string.Empty;
            string pictureurl = string.Empty;
            string statustype = string.Empty;
            string type = string.Empty;
            string fromname = string.Empty;
            string postid = posturl.Split('/')[posturl.Split('/').Count() - 1].ToString();
            //string postid = string.Empty;
            string userid = string.Empty;
            string fromid = string.Empty;
            string getlikecomment = string.Empty;
            DateTime comment_date = new DateTime();
            #endregion
            if (posturl.Contains("https:"))
            {
                posturl1 = posturl;
            }
            else
            {
                posturl1 = "https://www.facebook.com" + posturl;
            }
            try
            {
                Domain.Myfashion.Domain.FbPagePost _fbpagepost = new Domain.Myfashion.Domain.FbPagePost();
                _fbpagepost.Type = "status";
                _fbpagepost.StatusType = "shared_story";
                posthtml = objGlobusHttpHelper.getHtmlfromUrl(new Uri(posturl1));
                //post = socioHelper.getBetween(posthtml, "class=\"hasCaption\">", "<i class=\"_4-k1 img sp_LWp1MpKGrs1 sx_35a5d8").Replace("<br />", " ");
                string post1 = socioHelper.getBetween(posthtml, "<p>", "<form rel=\"async").Replace("See Translation","");
                post = Regex.Replace(post1, "<[^>]+>|&nbsp;", "");
                try
                {
                    pictureurl = socioHelper.getBetween(posthtml, "scaledImageFitWidth img\" src=\"", "\" alt=\"").Replace("amp;", "");
                    if (pictureurl != "")
                    {
                        _fbpagepost.Type = "photo";
                        _fbpagepost.StatusType = "added_photos";
                    }
                    if (post == "" && pictureurl == "")
                    {
                        pictureurl = socioHelper.getBetween(posthtml, "scaledImageFitHeight img\" src=\"", "\" style=").Replace("amp;", "");
                        _fbpagepost.Type = "link";
                    }
                    if (post1.Contains("LinkshimAsyncLink"))
                    {
                        _fbpagepost.Type = "link";
                    }
                }
                catch (Exception ex)
                {

                }
                getlikecomment = socioHelper.getBetween(posthtml, "feedbacktargets\":", "mentionsdatasource\":");
                like = Int32.Parse(socioHelper.getBetween(getlikecomment, "likecount\":", ",\"likecountreduce"));
                comment = Int32.Parse(socioHelper.getBetween(getlikecomment, "commentcount\":", ",\"commentcountreduce"));
                share = Int32.Parse(socioHelper.getBetween(posthtml, "sharecount\":", ",\"sharecountreduced"));
                try
                {
                    post_date = socioHelper.getBetween(posthtml, "<abbr title=\"", "\" data-utime=").Replace(" at","").Trim();
                    DateTime post_date1 = DateTime.Parse(post_date);
                    _fbpagepost.PostDate = post_date1;
                }
                catch { }
                fromid = socioHelper.getBetween(posthtml, "actorid\":\"", "\",\"");
                if (!string.IsNullOrEmpty(fromid))
                {
                    try
                    {
                        var _FromDetails = JObject.Parse(objGlobusHttpHelper.getHtmlfromUrl(new Uri("http://graph.facebook.com/" + fromid)));
                        fromname = _FromDetails["name"].ToString().ToString().Replace("\"", string.Empty).Trim(); ;
                        _fbpagepost.FromName = fromname;
                        string from_Username = _FromDetails["username"].ToString().ToString().Replace("\"", string.Empty).Trim();
                    }
                    catch { };
                }
                _fbpagepost.UserId = Guid.Parse(UserId);
                _fbpagepost.Id = Guid.NewGuid();
                _fbpagepost.PageId = pageid;
                _fbpagepost.Likes = like;
                _fbpagepost.Comments = comment;
                _fbpagepost.Shares = share;
                _fbpagepost.FromId = fromid;
                
                _fbpagepost.Post = post;
                _fbpagepost.PostId = postid;
                _fbpagepost.PictureUrl = pictureurl;
                _fbpagepost.LinkUrl = posturl1;
                _fbpagepost.EntryDate = DateTime.Now;
                
                string JsonObjectOfFbPagePostWithValue =new JavaScriptSerializer().Serialize(_fbpagepost);
                Api.FbPagePost.FbPagePost objApiFbPagePost = new Api.FbPagePost.FbPagePost();
                if (!objApiFbPagePost.IsPostExist(JsonObjectOfFbPagePostWithValue))
                {
                    bool IspostAdd = objApiFbPagePost.AddPostDetails(JsonObjectOfFbPagePostWithValue);
                }
                else
                {
                    int isPostUpdate = objApiFbPagePost.UpdateFbPagePostStatus(JsonObjectOfFbPagePostWithValue);
                }
                JObject jobjectdata = JObject.Parse("{\"comments\":" + socioHelper.getBetween(posthtml, ",\"comments\":", ",\"actions\":[]") + "}");
                try
                {
                    var Comments = jobjectdata["comments"];
                    foreach (var _Commentitem in Comments)
                    {
                        int isuserlike = 0;
                        Domain.Myfashion.Domain.FbPageComment _FbPageComment = new Domain.Myfashion.Domain.FbPageComment();
                        string commentfrom_id = string.Empty;
                        string commentfrom_name = string.Empty;
                        commentid = _Commentitem["id"].ToString().Replace("\"", string.Empty).Trim();
                        commentmsg = _Commentitem["body"]["text"].ToString().Replace("\"", string.Empty).Trim();
                        commentfrom_id = _Commentitem["author"].ToString().Replace("\"", string.Empty).Trim();
                        if (!string.IsNullOrEmpty(commentfrom_id))
                        {
                            try
                            {
                                var _autherDetails = JObject.Parse(objGlobusHttpHelper.getHtmlfromUrl(new Uri("http://graph.facebook.com/" + commentfrom_id)));
                                commentfrom_name = _autherDetails["name"].ToString().ToString().Replace("\"", string.Empty).Trim(); ;
                                string commentfrom_Username = _autherDetails["username"].ToString().ToString().Replace("\"", string.Empty).Trim();
                            }
                            catch { };
                        }
                        try
                        {
                            commentcreated_time = _Commentitem["timestamp"]["verbose"].ToString().Replace("\"", string.Empty).Replace("at ", string.Empty).Trim();
                            comment_date = DateTime.Parse(commentcreated_time);
                        }
                        catch { };
                        commentlike_count = _Commentitem["likecount"].ToString().Replace("\"", string.Empty).Trim();
                        user_likes = _Commentitem["hasviewerliked"].ToString().Replace("\"", string.Empty).Trim();
                        if (user_likes == "False")
                        {
                            isuserlike = 0;
                        }
                        else { isuserlike = 1; }
                        _FbPageComment.Id = Guid.NewGuid();
                        _FbPageComment.UserId = Guid.Parse(UserId);
                        _FbPageComment.PostId = postid;
                        _FbPageComment.Likes = Int32.Parse(commentlike_count);
                        _FbPageComment.UserLikes = isuserlike;
                        _FbPageComment.Commentdate = comment_date;
                        _FbPageComment.CommentId = commentid;
                        _FbPageComment.FromId = commentfrom_id;
                        _FbPageComment.FromName = commentfrom_name;
                        _FbPageComment.PictureUrl = commentid;
                        _FbPageComment.Comment = commentmsg;
                        _FbPageComment.EntryDate = DateTime.Now;
                        string JsonObjectOfFbPageCommentWithValue =new JavaScriptSerializer().Serialize(_FbPageComment);
                        Api.FbPageComment.FbPageComment objApiFbPageComment = new Api.FbPageComment.FbPageComment();
                        if (!objApiFbPageComment.IsCommentExist(JsonObjectOfFbPageCommentWithValue))
                        {
                            bool iscmtAdd = objApiFbPageComment.AddCommentDetails(JsonObjectOfFbPageCommentWithValue);
                        }
                        else 
                        {
                            int isCmtUpdate = objApiFbPageComment.UpdateFbPageCommentStatus(JsonObjectOfFbPageCommentWithValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                try
                {
                    
                    //string likerurl = "https://www.facebook.com/ajax/browser/dialog/likes?id=" + postid + "&actorid=" + pageid + "&__asyncDialog=1&__user=" + user_id + "&__a=1&__dyn=7n8ajEBQ9FoBZypQ9UoHFaeFDzECiq78hACF3pqzCC-C26m6oKezpUgDyQqUgF5BzEy6E&__req=28&__rev=1461180";
                    string likerurl = "https://www.facebook.com/ajax/browser/dialog/likes?id=" + postid + "&actorid=" + pageid + "&__asyncDialog=1&__user=" + user_id + "&__a=1&__dyn=7nmajEyl2lm9o-t2u59G85ku699Esx6iqAdy9VQC-C26m6oKezob4q68K5Uc-dwIxbxjyV8izaG8Czrw&__req=1r&__rev=1696458&ft[tn]=]&ft[fbfeed_location]=5";
                    string likerhtml = objGlobusHttpHelper.getHtmlfromUrl(new Uri(likerurl));
                    string[] splitliker = Regex.Split(likerhtml, "_s0 _rw img");
                    foreach (string liker_item in splitliker)
                    {
                        if (!liker_item.Contains("for (;;);"))
                        {
                            Domain.Myfashion.Domain.FbPageLiker _FbPageLiker = new Domain.Myfashion.Domain.FbPageLiker();
                            like_id = socioHelper.getBetween(liker_item, "data-hovercard=\\\"\\/ajax\\/hovercard\\/user.php?id=", "&amp;extragetparams");
                            string[] splitliker1 = Regex.Split(liker_item, "hc_location=profile_browser");
                            like_name = socioHelper.getBetween(splitliker1[1], "\\\">", "\\u003C");
                            _FbPageLiker.Id = Guid.NewGuid();
                            _FbPageLiker.UserId = Guid.Parse(UserId);
                            _FbPageLiker.PostId = postid;
                            _FbPageLiker.FromId = like_id;
                            _FbPageLiker.FromName = like_name;
                            string JsonObjectOfFbPageLikerWithValue = new JavaScriptSerializer().Serialize(_FbPageLiker);
                            Api.FbPageLiker.FbPageLiker objApiFbPageLiker = new Api.FbPageLiker.FbPageLiker();
                            if (!objApiFbPageLiker.IsLikeExist(JsonObjectOfFbPageLikerWithValue))
                            {
                                bool IslikeAdd = objApiFbPageLiker.AddLikeDetails(JsonObjectOfFbPageLikerWithValue);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
        public void GetPostCommentGraphApi(string html, ref GlobusHttpHelper objGlobusHttpHelper, string pageid, string UserId)
        {
            #region variables region
            string posthtml = string.Empty;
            string posturl = string.Empty;
            string post = string.Empty;
            int like = 0;
            int comment = 0;
            int share = 0;
            string commentid = string.Empty;
            string commentmsg = string.Empty;
            string commentfrom_id = string.Empty;
            string commentfrom_name = string.Empty;
            string commentcreated_time = string.Empty;
            string commentlike_count = string.Empty;
            string user_likes = string.Empty;
            string like_id = string.Empty;
            string like_name = string.Empty;
            DateTime postdate = new DateTime();
            string linkurl = string.Empty;
            string pictureurl = string.Empty;
            string statustype = string.Empty;
            string type = string.Empty;
            string fromname = string.Empty;
            //string pageid = string.Empty;
            string fromid = string.Empty;
            string getlikecomment = string.Empty;
            string postid = string.Empty;
            string iconurl = string.Empty;
            #endregion
            try 
	        {
                Domain.Myfashion.Domain.FbPagePost _fbpagepost = new Domain.Myfashion.Domain.FbPagePost();
		        getlikecomment = socioHelper.getBetween(html, "[{\"actorforpost\":", "{\"value\":\"filtered\"");
                postid = socioHelper.getBetween(html, "targetfbid\":\"", "\",\"viewercanlike\":");
                string grafurl = "http://graph.facebook.com/" + postid;
                posthtml = objGlobusHttpHelper.getHtmlfromUrl(new Uri(grafurl));
                try
                {
                    like = Int32.Parse(socioHelper.getBetween(html, "likecount\":", ",\"likecountreduced"));
                    comment = Int32.Parse(socioHelper.getBetween(html, "commentcount\":", ",\"commentcountreduced"));
                    share = Int32.Parse(socioHelper.getBetween(html, "sharecount\":", ",\"sharecountreduced"));
                    //posturl = socioHelper.getBetween(html, "permalink\":\"", "/?").Replace(@"\", "");
                    posturl = socioHelper.getBetween(html, "permalink\":\"", "\"").Replace(@"\", "");
                    if(posturl=="")
                    {
                        posturl = socioHelper.getBetween(html, "permalink\":\"", "\"").Replace(@"\", "");
                    }
                    _fbpagepost.UserId = Guid.Parse(UserId);
                    _fbpagepost.Id = Guid.NewGuid();
                    _fbpagepost.PageId = pageid;
                    _fbpagepost.Likes = like;
                    _fbpagepost.Comments = comment;
                    _fbpagepost.Shares = share;
                    _fbpagepost.PostId = postid;
                    _fbpagepost.StatusType = "shared_story";
                }
                catch (Exception ex)
                {
                    posturl = "https://www.facebook.com/"+postid;
                }
                if (posthtml != "")
                {
                    JObject JData = JObject.Parse(posthtml);
                    try
                    {
                        fromid = JData["from"]["id"].ToString().Replace("\"", string.Empty).Trim();
                        _fbpagepost.FromId = fromid;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        iconurl = JData["icon"].ToString().Replace("\"", string.Empty).Trim();
                        _fbpagepost.IconUrl = iconurl;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        linkurl = JData["link"].ToString().Replace("\"", string.Empty).Trim();
                        if (linkurl.Contains("album"))
                        {
                            _fbpagepost.Type = "album";
                        }
                      _fbpagepost.LinkUrl = linkurl;
                    }
                    catch { }
                    try
                    {
                        post = JData["name"].ToString().Replace("\"", string.Empty).Trim();
                        _fbpagepost.Post = post;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        post = "";
                    }
                    try
                    {
                        fromname = JData["from"]["name"].ToString().Replace("\"", string.Empty).Trim();
                        _fbpagepost.FromName = fromname;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        pictureurl = JData["source"].ToString().Replace("\"", string.Empty).Trim();
                        if (pictureurl != "")
                        {
                            _fbpagepost.Type = "photo";
                            _fbpagepost.StatusType = "added_photos";
                        }
                        _fbpagepost.PictureUrl = pictureurl;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        string pdate = (JData["created_time"].ToString().Replace("\"", string.Empty).Replace("T", " ").Trim());
                        if (pdate.Contains("+"))
                        {
                            pdate = pdate.Substring(0, pdate.IndexOf("+"));
                        }
                        postdate = DateTime.Parse(pdate);
                        _fbpagepost.PostDate = postdate;
                        _fbpagepost.EntryDate = DateTime.Now;
                    }
                    catch { }
                    string JsonObjectOfFbPagePostWithValue = new JavaScriptSerializer().Serialize(_fbpagepost);
                    Api.FbPagePost.FbPagePost objApiFbPagePost = new Api.FbPagePost.FbPagePost();
                    if (!objApiFbPagePost.IsPostExist(JsonObjectOfFbPagePostWithValue))
                    {
                        bool IspostAdd = objApiFbPagePost.AddPostDetails(JsonObjectOfFbPagePostWithValue);
                    }
                    else
                    {
                        int isPostUpdate = objApiFbPagePost.UpdateFbPagePostStatus(JsonObjectOfFbPagePostWithValue);
                    }
                    if (comment != 0)
                    {
                        #region GetComments
                        try
                        {
                            
                            var Comments = JData["comments"]["data"];
                            string Comment_next = string.Empty;
                            try
                            {
                                Comment_next = JData["comments"]["paging"]["next"].ToString().Replace("\"", string.Empty);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                Comment_next = "";
                            }
                        getComments:
                            foreach (var _Commentitem in Comments)
                            {
                                try
                                {
                                    Domain.Myfashion.Domain.FbPageComment _FbPageComment = new Domain.Myfashion.Domain.FbPageComment();
                                    int isuserlike=0;
                                    commentid = _Commentitem["id"].ToString().Replace("\"", string.Empty).Trim();
                                    commentmsg = _Commentitem["message"].ToString().Replace("\"", string.Empty).Trim();
                                    commentfrom_id = _Commentitem["from"]["id"].ToString().Replace("\"", string.Empty).Trim();
                                    commentfrom_name = _Commentitem["from"]["name"].ToString().Replace("\"", string.Empty).Trim();
                                    commentcreated_time = _Commentitem["created_time"].ToString().Replace("\"", string.Empty).Trim();
                                    try
                                    {
                                        if (commentcreated_time.Contains("+"))
                                        {
                                            commentcreated_time = commentcreated_time.Substring(0, commentcreated_time.IndexOf("+"));
                                        }
                                        DateTime comment_date = DateTime.Parse(commentcreated_time);
                                        _FbPageComment.Commentdate = comment_date;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Write(ex.StackTrace);
                                    }
                                    commentlike_count = _Commentitem["like_count"].ToString().Replace("\"", string.Empty).Trim();
                                    user_likes = _Commentitem["user_likes"].ToString().Replace("\"", string.Empty).Trim();
                                    if(user_likes=="False")
                                    {
                                        isuserlike=0;
                                    }
                                    else{ isuserlike=1; }
                                    _FbPageComment.Id = Guid.NewGuid();
                                    _FbPageComment.UserId =Guid.Parse(UserId);
                                    _FbPageComment.PostId = postid;
                                    _FbPageComment.Likes =Int32.Parse(commentlike_count);
                                    _FbPageComment.UserLikes = isuserlike;
                                    
                                    _FbPageComment.CommentId = commentid;
                                    _FbPageComment.FromId = commentfrom_id;
                                    _FbPageComment.FromName = commentfrom_name;
                                    _FbPageComment.PictureUrl = commentid;
                                    _FbPageComment.Comment = commentmsg;
                                    _FbPageComment.EntryDate = DateTime.Now;
                                    string JsonObjectOfFbPageCommentWithValue = new JavaScriptSerializer().Serialize(_FbPageComment);
                                    Api.FbPageComment.FbPageComment objApiFbPageComment = new Api.FbPageComment.FbPageComment();
                                    if (!objApiFbPageComment.IsCommentExist(JsonObjectOfFbPageCommentWithValue))
                                    {
                                        bool iscmtAdd = objApiFbPageComment.AddCommentDetails(JsonObjectOfFbPageCommentWithValue);
                                    }
                                    else
                                    {
                                        int isCmtUpdate = objApiFbPageComment.UpdateFbPageCommentStatus(JsonObjectOfFbPageCommentWithValue);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }

                            }

                            if (!string.IsNullOrEmpty(Comment_next))
                            {
                                string commentpagesrc = objGlobusHttpHelper.getHtmlfromUrl(new Uri(Comment_next));
                                JObject JData_Comment_next = JObject.Parse(commentpagesrc);
                                Comments = JData_Comment_next["data"];
                                try
                                {
                                    Comment_next = JData_Comment_next["paging"]["next"].ToString().Replace("\"", string.Empty).Trim();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                    Comment_next = "";
                                }
                                goto getComments;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                        #endregion
                    }
                    if (like != 0)
                    {
                        #region GetLikes
                        try
                        {
                            
                            var Likes = JData["likes"]["data"];
                            string Likes_next = string.Empty;
                            try
                            {
                                Likes_next = JData["likes"]["paging"]["next"].ToString().Replace("\"", string.Empty);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                Likes_next = "";
                            }
                        getLikes:
                            foreach (var _Likeitem in Likes)
                            {
                                try
                                {
                                    Domain.Myfashion.Domain.FbPageLiker _FbPageLiker = new Domain.Myfashion.Domain.FbPageLiker();
                                    like_id = _Likeitem["id"].ToString().Replace("\"", string.Empty).Trim();
                                    like_name = _Likeitem["name"].ToString().Replace("\"", string.Empty).Trim();
                                    _FbPageLiker.Id = Guid.NewGuid();
                                    _FbPageLiker.UserId = Guid.Parse(UserId);
                                    _FbPageLiker.PostId = postid;
                                    _FbPageLiker.FromId = like_id;
                                    _FbPageLiker.FromName = like_name;
                                    string JsonObjectOfFbPageLikerWithValue = new JavaScriptSerializer().Serialize(_FbPageLiker);
                                    Api.FbPageLiker.FbPageLiker objApiFbPageLiker = new Api.FbPageLiker.FbPageLiker();
                                    if (!objApiFbPageLiker.IsLikeExist(JsonObjectOfFbPageLikerWithValue))
                                    {
                                        bool IslikeAdd = objApiFbPageLiker.AddLikeDetails(JsonObjectOfFbPageLikerWithValue);
                                    }
                                }
                                catch { };
                            }
                            if (!string.IsNullOrEmpty(Likes_next))
                            {
                                string likepagesrc = objGlobusHttpHelper.getHtmlfromUrl(new Uri(Likes_next));
                                JObject JData_Like_next = JObject.Parse(likepagesrc);
                                Likes = JData_Like_next["data"];
                                try
                                {
                                    Likes_next = JData_Like_next["paging"]["next"].ToString().Replace("\"", string.Empty).Trim();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                    Likes_next = "";
                                }
                                goto getLikes;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                        #endregion
                    }
                }
                else
                {
                    GetPostCommentParsing(posturl, ref objGlobusHttpHelper,pageid,UserId);

                } 
	}
	catch (Exception ex)
	{
        Console.WriteLine(ex.StackTrace);
	}
   }

        public string txtreader(string FileName)
        {
            string[] res;
            if (File.Exists(FileName))
            {
                res = File.ReadAllLines(FileName);
                if (res.Length == 1)
                    return res[0];
                else
                {
                    Random r = new Random();
                    return res[r.Next(res.Length)];
                }
            }
            return null;
        }

        public string GetFBUser()
        {
            string FbUser = "";
            string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FbUser = txtreader(Path + "\\" + "FB_Accounts.txt");
            return FbUser;
        }
    }
}
