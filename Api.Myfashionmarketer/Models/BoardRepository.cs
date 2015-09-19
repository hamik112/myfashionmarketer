using Api.Myfashionmarketer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Criterion;
using System.Text;
namespace Api.Socioboard.Model
{
    public class BoardRepository
    {
        public bool AddBoard(Domain.Myfashion.Domain.Boards board)
        {
            bool IsSuccess = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Save data.
                    try
                    {
                        session.Save(board);
                        transaction.Commit();
                        IsSuccess = true;
                        return IsSuccess;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        return IsSuccess;
                    }

                }//End Transaction
            }//End Session
        }

        public List<Domain.Myfashion.Domain.Boards> getUserBoards(Guid UserId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boards> objlstfb = session.CreateQuery("from Boards where UserId = :UserId and IsHidden = false ")
                            .SetParameter("UserId", UserId)
                       .List<Domain.Myfashion.Domain.Boards>().ToList<Domain.Myfashion.Domain.Boards>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End session
        }

        public Domain.Myfashion.Domain.Boards getBoard(Guid Id)
        {
            Domain.Myfashion.Domain.Boards board = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Boards where BoardId = :ID and IsHidden = false");
                        query.SetParameter("ID", Id);
                        Domain.Myfashion.Domain.Boards result = (Domain.Myfashion.Domain.Boards)query.UniqueResult();
                        board = result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session

            return board;
        }

        public Domain.Myfashion.Domain.Boards getDeletedBoard(Guid Id)
        {
            Domain.Myfashion.Domain.Boards board = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Boards where BoardId = :ID");
                        query.SetParameter("ID", Id);
                        Domain.Myfashion.Domain.Boards result = (Domain.Myfashion.Domain.Boards)query.UniqueResult();
                        board = result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session

            return board;
        }


        public bool updateBoard(Domain.Myfashion.Domain.Boards board)
        {
            bool isUpdate = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update Boards set BoardName=:BoardName,FbProfileId=:fbPfofileId,LinkediProfileId=:linkedinPfofileId,GPlusProfileId=:gplusProfileId,InstagramProfileId=:instagramProfileId,TwitterProfileId=:twitterProfileId,TumblrProfileId=:tumblrProfileId,YoutubeProfileId=:youtubeProfileId where BoardId = :BoardId")
                            .SetParameter("BoardName", board.BoardName)
                            .SetParameter("BoardId", board.BoardId)
                            .SetParameter("fbPfofileId", board.Fbprofileid)
                            .SetParameter("gplusProfileId", board.Gplusprofileid)
                            .SetParameter("instagramProfileId", board.Instagramprofileid)
                            .SetParameter("linkedinPfofileId", board.Linkedinprofileid)
                            .SetParameter("twitterPfofileId", board.Twitterprofileid)
                            .SetParameter("tumblrProfileId", board.Tumblrprofileid)
                            .SetParameter("youtubeProfileId", board.Youtubeprofileid)
                            .ExecuteUpdate();
                        transaction.Commit();
                        isUpdate = true;
                        return isUpdate;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return isUpdate;
                    }
                }//End Transaction
            }//End session
        }


        public Domain.Myfashion.Domain.Boards SearchBoardByName(string Keywords)
        {
            Domain.Myfashion.Domain.Boards board = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Myfashion.Domain.Boards> result = session.CreateQuery("from Boards where BoardName=:BoardName and IsHidden = false").SetParameter("BoardName", Keywords).List<Domain.Myfashion.Domain.Boards>().ToList<Domain.Myfashion.Domain.Boards>();

                        board = result[0];
                    }
                    catch (Exception e) { }
                }//End Transaction
            }//End session

            return board;
        }


        #region Facebook

        public void addBoardFbPage(Domain.Myfashion.Domain.Boardfbpage fbpage)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(fbpage);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }

        public Domain.Myfashion.Domain.Boardfbpage getBoardFbpage(Guid BoardId)
        {

            Domain.Myfashion.Domain.Boardfbpage result = new Domain.Myfashion.Domain.Boardfbpage();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardfbpage> objlstfb = session.CreateQuery("from Boardfbpage where BoardId = :BoardId ")
                            .SetParameter("BoardId", BoardId)
                       .List<Domain.Myfashion.Domain.Boardfbpage>().ToList<Domain.Myfashion.Domain.Boardfbpage>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }

        public List<Domain.Myfashion.Domain.Boardfbpage> getAllfbPages()
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardfbpage> objlstfb = session.CreateQuery("from Boardfbpage")
                       .List<Domain.Myfashion.Domain.Boardfbpage>().ToList<Domain.Myfashion.Domain.Boardfbpage>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End se
        }

        public bool checkFacebookFeedExists(string feedsid, Guid BoardfbPageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feed by messages id.
                        //NHibernate.IQuery query = session.CreateQuery("from Boardfbfeeds where  FeedId = :msgid and  FbPageProfileId=:profileid ");
                        //query.SetParameter("msgid", feedsid);
                        //query.SetParameter("profileid", BoardfbPageId);
                        //var resutl = query.UniqueResult();

                        //if (resutl == null)
                        //    return false;
                        //else
                        //    return true;

                        bool exist = session.Query<Domain.Myfashion.Domain.Boardfbfeeds>()
                            .Any(x => x.Feedid == feedsid && x.Fbpageprofileid==BoardfbPageId);
                        return exist;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End Session
        }






        public List<Domain.Myfashion.Domain.Boardfbfeeds> getBoardFbfeeds(Guid BoardfbprofileId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardfbfeeds> objlstfb = session.CreateQuery("from Boardfbfeeds where FbPageProfileId = :BoardfbprofileId ")
                            .SetParameter("BoardfbprofileId", BoardfbprofileId)
                       .List<Domain.Myfashion.Domain.Boardfbfeeds>().ToList<Domain.Myfashion.Domain.Boardfbfeeds>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End session
        }

        public List<Domain.Myfashion.Domain.Boardfbfeeds> getBoardFbfeedsbyrange(Guid BoardfbprofileId, int _noOfDataToSkip, int _noOfResultsFromTop)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardfbfeeds> objlstfb = session.Query<Domain.Myfashion.Domain.Boardfbfeeds>().Where(p => p.Fbpageprofileid == BoardfbprofileId).OrderByDescending(x => x.Createddate).Skip(Convert.ToInt32(_noOfDataToSkip)).Take(Convert.ToInt32(_noOfResultsFromTop)).ToList<Domain.Myfashion.Domain.Boardfbfeeds>();
                    return objlstfb;
                }//End Transaction
            }//End session
        }

        public void addBoardFbPageFeed(Domain.Myfashion.Domain.Boardfbfeeds fbfeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(fbfeed);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }
        # endregion

        #region Twitter

        public void addBoardTwitterAccount(Domain.Myfashion.Domain.Boardtwitteraccount twitteraccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(twitteraccount);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }

        public Domain.Myfashion.Domain.Boardtwitteraccount getBoardTwitterAccount(Guid BoardId)
        {

            Domain.Myfashion.Domain.Boardtwitteraccount result = new Domain.Myfashion.Domain.Boardtwitteraccount();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardtwitteraccount> objlstfb = session.CreateQuery("from Boardtwitteraccount where BoardId = :BoardId ")
                            .SetParameter("BoardId", BoardId)
                       .List<Domain.Myfashion.Domain.Boardtwitteraccount>().ToList<Domain.Myfashion.Domain.Boardtwitteraccount>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }

        public List<Domain.Myfashion.Domain.Boardtwitteraccount> getAllBoardTwiiterAccounts()
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardtwitteraccount> objlstfb = session.CreateQuery("from Boardtwitteraccount")
                       .List<Domain.Myfashion.Domain.Boardtwitteraccount>().ToList<Domain.Myfashion.Domain.Boardtwitteraccount>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End se
        }
        public bool checkTwitterFeedExists(string feedsid, Guid BoardtwitterPageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feed by messages id.
                        //NHibernate.IQuery query = session.CreateQuery("from Boardtwitterfeeds where  FeedId = :msgid and  TwitterProfileId=:profileid ");
                        //query.SetParameter("msgid", feedsid);
                        //query.SetParameter("profileid", BoardtwitterPageId);
                        //var resutl = query.UniqueResult();

                        //if (resutl == null)
                        //    return false;
                        //else
                        //    return true;

                        bool exist = session.Query<Domain.Myfashion.Domain.Boardtwitterfeeds>()
                           .Any(x => x.Feedid == feedsid && x.Twitterprofileid == BoardtwitterPageId);
                        return exist;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End Session
        }


        public List<Domain.Myfashion.Domain.Boardtwitterfeeds> getBoardTwitterfeeds(Guid BoardtwitterprofileId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardtwitterfeeds> objlstfb = session.CreateQuery("from Boardtwitterfeeds where TwitterProfileId = :TwitterProfileId ")
                            .SetParameter("TwitterProfileId", BoardtwitterprofileId)
                       .List<Domain.Myfashion.Domain.Boardtwitterfeeds>().ToList<Domain.Myfashion.Domain.Boardtwitterfeeds>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End session
        }




        public List<Domain.Myfashion.Domain.Boardtwitterfeeds> getAlltwitterfeeds()
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardtwitterfeeds> objlstfb = session.CreateQuery("from Boardtwitterfeeds")
                       .List<Domain.Myfashion.Domain.Boardtwitterfeeds>().ToList<Domain.Myfashion.Domain.Boardtwitterfeeds>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End se
        }


        public bool updateTwitterFeed(Domain.Myfashion.Domain.Boardtwitterfeeds twitterfeed)
        {
            bool isUpdate = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update Boardtwitterfeeds set CreatedAt=:CreatedAt,FeedId=:FeedId,FeedUrl=:FeedUrl,ImageUrl=:ImageUrl,Text=:Text,HashTags=:HashTags,TwitterProfileId=:TwitterProfileId,IsVisible=:IsVisible,FromName=:FromName,FromId=:FromId,RetweetCount=:RetweetCount,FavoritedCount=:FavoritedCount,FromPicUrl=:FromPicUrl where Id = :Id")
                            .SetParameter("CreatedAt", twitterfeed.Createdat)
                            .SetParameter("FeedId", twitterfeed.Feedid)
                            .SetParameter("FeedUrl", twitterfeed.Feedurl)
                            .SetParameter("ImageUrl", twitterfeed.Imageurl)
                            .SetParameter("Text", twitterfeed.Text)
                            .SetParameter("HashTags", twitterfeed.Hashtags)
                            .SetParameter("TwitterProfileId", twitterfeed.Twitterprofileid)
                            .SetParameter("IsVisible", twitterfeed.Isvisible)
                            .SetParameter("FromName", twitterfeed.FromName)
                             .SetParameter("FromId", twitterfeed.FromId)
                            .SetParameter("RetweetCount", twitterfeed.Retweetcount)
                             .SetParameter("FavoritedCount", twitterfeed.Favoritedcount)
                             .SetParameter("FromPicUrl", twitterfeed.FromPicUrl)
                            .SetParameter("Id", twitterfeed.Id)
                            .ExecuteUpdate();
                        transaction.Commit();
                        isUpdate = true;
                        return isUpdate;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return isUpdate;
                    }
                }//End Transaction
            }//End session
        }


        public Guid GetUserIdBytwitterFeedId(Guid feedId)
        {
            Domain.Myfashion.Domain.Boardtwitteraccount board = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Boardtwitterfeeds where Id = :ID ");
                        query.SetParameter("ID", feedId);
                        Domain.Myfashion.Domain.Boardtwitterfeeds result = (Domain.Myfashion.Domain.Boardtwitterfeeds)query.UniqueResult();

                        query = session.CreateQuery("from Boardtwitteraccount where Id = :ID ");
                        query.SetParameter("ID", result.Twitterprofileid);
                        Domain.Myfashion.Domain.Boardtwitteraccount twtacc = (Domain.Myfashion.Domain.Boardtwitteraccount)query.UniqueResult();

                        query = session.CreateQuery("from Boards where BoardId = :ID ");
                        query.SetParameter("ID", twtacc.Boardid);
                        Domain.Myfashion.Domain.Boards Board = (Domain.Myfashion.Domain.Boards)query.UniqueResult();
                        return Board.UserId;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
            return Guid.Empty;
        }


        public List<Domain.Myfashion.Domain.Boardtwitterfeeds> getBoardTwitterfeedsbyrange(Guid BoardtwitterprofileId, int _noOfDataToSkip, int _noOfResultsFromTop)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardtwitterfeeds> objlstfb = session.Query<Domain.Myfashion.Domain.Boardtwitterfeeds>().Where(p => p.Twitterprofileid == BoardtwitterprofileId).OrderByDescending(x => x.Createdat).Skip(Convert.ToInt32(_noOfDataToSkip)).Take(Convert.ToInt32(_noOfResultsFromTop)).ToList<Domain.Myfashion.Domain.Boardtwitterfeeds>();
                    return objlstfb;
                }//End Transaction
            }//End session
        }

        public void addBoardTwitterFeed(Domain.Myfashion.Domain.Boardtwitterfeeds twitterfeeds)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                            session.Save(twitterfeeds);
                            transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }



        public int getBoardTwitterfeedsRetweetsCount(Guid BoardtwitterprofileId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int output = 0;
                    try
                    {
                        byte[] guidBytes = BoardtwitterprofileId.ToByteArray();
                        StringBuilder guidBinary = new StringBuilder();
                        foreach (byte guidByte in guidBytes)
                        {
                            guidBinary.AppendFormat(@"{0}", guidByte.ToString("x2"));
                        }
                        NHibernate.IQuery query = session.CreateSQLQuery("select sum(boardtwitterfeeds.RetweetCount) from boardtwitterfeeds where boardtwitterfeeds.TwitterProfileId =x'" + guidBinary.ToString() + "'");
                        var count = query.UniqueResult();
                        output = Convert.ToInt32(count);
                    }
                    catch (Exception e) { }

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).


                    return output;
                }//End Transaction
            }//End session
        }




        public int getBoardTwitterfeedsFavoritedCount(Guid BoardtwitterprofileId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int output = 0;
                    try
                    {
                        byte[] guidBytes = BoardtwitterprofileId.ToByteArray();
                        StringBuilder guidBinary = new StringBuilder();
                        foreach (byte guidByte in guidBytes)
                        {
                            guidBinary.AppendFormat(@"{0}", guidByte.ToString("x2"));
                        }
                        NHibernate.IQuery query = session.CreateSQLQuery("select sum(boardtwitterfeeds.RetweetCount) from boardtwitterfeeds where boardtwitterfeeds.TwitterProfileId =x'" + guidBinary.ToString() + "'");
                        var count = query.UniqueResult();
                        output = Convert.ToInt32(count);
                    }
                    catch (Exception e) { }

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).


                    return output;
                }//End Transaction
            }//End session
        }




        public int getBoardTwitterfeedsCount(Guid BoardtwitterprofileId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int output = 0;
                    try
                    {
                        byte[] guidBytes = BoardtwitterprofileId.ToByteArray();
                        StringBuilder guidBinary = new StringBuilder();
                        foreach (byte guidByte in guidBytes)
                        {
                            guidBinary.AppendFormat(@"{0}", guidByte.ToString("x2"));
                        }
                        NHibernate.IQuery query = session.CreateSQLQuery("select count(boardtwitterfeeds.FavoritedCount) from boardtwitterfeeds where boardtwitterfeeds.TwitterProfileId =x'" + guidBinary.ToString() + "'");
                        var count = query.UniqueResult();
                        output = Convert.ToInt32(count);
                    }
                    catch (Exception e) { }

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).


                    return output;
                }//End Transaction
            }//End session
        }





        public List<Domain.Myfashion.Domain.Boardtwitterfeeds> getPopularTweets(Guid BoardTwitterProfileId)
        {
            //List<Domain.Myfashion.Domain.Boardtwitterfeeds> twtFeedsList = new List<Domain.Myfashion.Domain.Boardtwitterfeeds>();

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardtwitterfeeds> objlsttwt = session.CreateQuery("from Boardtwitterfeeds where TwitterProfileId = :TwitterProfileId")
                            .SetParameter("TwitterProfileId", BoardTwitterProfileId)
                       .List<Domain.Myfashion.Domain.Boardtwitterfeeds>().OrderByDescending(feed => feed.Retweetcount).Take(5).ToList<Domain.Myfashion.Domain.Boardtwitterfeeds>();

                    //int count = objlstfb.Where(t => t.Createdat != null && t.Createdat.Value.Date >= DateTime.Now.AddDays(-30)).Count();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlsttwt;
                }//End Transaction
            }//End session





            // return twtFeedsList;
        }

        public List<Domain.Myfashion.Domain.Boardtwitterfeeds> getPopularTweetsByFavorited(Guid BoardTwitterProfileId)
        {
            //List<Domain.Myfashion.Domain.Boardtwitterfeeds> twtFeedsList = new List<Domain.Myfashion.Domain.Boardtwitterfeeds>();

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).

                    List<Domain.Myfashion.Domain.Boardtwitterfeeds> objlstttwwt = session.CreateQuery("from Boardtwitterfeeds where TwitterProfileId = :TwitterProfileId")
                           .SetParameter("TwitterProfileId", BoardTwitterProfileId)
                      .List<Domain.Myfashion.Domain.Boardtwitterfeeds>().OrderByDescending(feed => feed.Favoritedcount).Take(5).ToList<Domain.Myfashion.Domain.Boardtwitterfeeds>();
                    //int count = objlstfb.Where(t => t.Createdat != null && t.Createdat.Value.Date >= DateTime.Now.AddDays(-30)).Count();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstttwwt;
                }//End Transaction
            }//End session





            // return twtFeedsList;
        }

        public string getLastTweetId(Guid BoardTwitterProfileId)
        {
            //List<Domain.Myfashion.Domain.Boardtwitterfeeds> twtFeedsList = new List<Domain.Myfashion.Domain.Boardtwitterfeeds>();

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    string MaxtweetID = session.CreateQuery("from Boardtwitterfeeds where TwitterProfileId = :TwitterProfileId")
                            .SetParameter("TwitterProfileId", BoardTwitterProfileId)
                       .List<Domain.Myfashion.Domain.Boardtwitterfeeds>().Max(t => t.Feedid);

                    //int count = objlstfb.Where(t => t.Createdat != null && t.Createdat.Value.Date >= DateTime.Now.AddDays(-30)).Count();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return MaxtweetID;
                }//End Transaction
            }//End session





            // return twtFeedsList;
        }





        # endregion


        #region Instagram

        public void addBoardInstagramAccount(Domain.Myfashion.Domain.Boardinstagramaccount boardinstagramaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(boardinstagramaccount);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }


        public void addBoardInstagramFeeds(Domain.Myfashion.Domain.Boardinstagramfeeds boardinstagramfeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(boardinstagramfeed);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }

        public Domain.Myfashion.Domain.Boardinstagramaccount getBoardInstagramAccount(Guid BoardId)
        {

            Domain.Myfashion.Domain.Boardinstagramaccount result = new Domain.Myfashion.Domain.Boardinstagramaccount();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardinstagramaccount> objlstfb = session.CreateQuery("from Boardinstagramaccount where BoardId = :BoardId ")
                            .SetParameter("BoardId", BoardId)
                       .List<Domain.Myfashion.Domain.Boardinstagramaccount>().ToList<Domain.Myfashion.Domain.Boardinstagramaccount>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }


        public bool checkInstagramFeedExists(string feedsid, Guid BoardInstagramPageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feed by messages id.
                        //NHibernate.IQuery query = session.CreateQuery("from Boardinstagramfeeds where  FeedId = :msgid and  InstagramAccountId=:profileid ");
                        //query.SetParameter("msgid", feedsid);
                        //query.SetParameter("profileid", BoardInstagramPageId);
                        //var resutl = query.UniqueResult();

                        //if (resutl == null)
                        //    return false;
                        //else
                        //    return true;
                        bool exist = session.Query<Domain.Myfashion.Domain.Boardinstagramfeeds>()
                            .Any(x => x.Feedid == feedsid && x.Instagramaccountid==BoardInstagramPageId);
                        return exist;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End Session
        }

        public List<Domain.Myfashion.Domain.Boardinstagramaccount> getAllBoardInstagramAccounts()
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardinstagramaccount> objlstfb = session.CreateQuery("from Boardinstagramaccount")
                       .List<Domain.Myfashion.Domain.Boardinstagramaccount>().ToList<Domain.Myfashion.Domain.Boardinstagramaccount>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End se
        }



        public List<Domain.Myfashion.Domain.Boardinstagramfeeds> getBoardInstagramfeeds(Guid BoardinstagramprofileId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardinstagramfeeds> objlstfb = session.CreateQuery("from Boardinstagramfeeds where InstagramAccountId = :InstagramAccountId ")
                            .SetParameter("InstagramAccountId", BoardinstagramprofileId)
                       .List<Domain.Myfashion.Domain.Boardinstagramfeeds>().ToList<Domain.Myfashion.Domain.Boardinstagramfeeds>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End session
        }

        public List<Domain.Myfashion.Domain.Boardinstagramfeeds> getBoardInstagramfeedsbyrange(Guid BoardinstagramprofileId, int _noOfDataToSkip, int _noOfResultsFromTop)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardinstagramfeeds> objlstfb = session.Query<Domain.Myfashion.Domain.Boardinstagramfeeds>().Where(p => p.Instagramaccountid == BoardinstagramprofileId).OrderByDescending(x => x.Createdtime).Skip(Convert.ToInt32(_noOfDataToSkip)).Take(Convert.ToInt32(_noOfResultsFromTop)).ToList<Domain.Myfashion.Domain.Boardinstagramfeeds>();
                    return objlstfb;
                }//End Transaction
            }//End session
        }

        # endregion

        #region GPlus

        public void addBoardGPlusAccount(Domain.Myfashion.Domain.Boardgplusaccount boardgplusaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(boardgplusaccount);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }


        public void addBoardGPlusFeed(Domain.Myfashion.Domain.Boardgplusfeeds boardgplusfeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(boardgplusfeed);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }

        public Domain.Myfashion.Domain.Boardgplusaccount getBoardGplusAccount(Guid BoardId)
        {

            Domain.Myfashion.Domain.Boardgplusaccount result = new Domain.Myfashion.Domain.Boardgplusaccount();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardgplusaccount> objlstfb = session.CreateQuery("from Boardgplusaccount where BoardId = :BoardId ")
                            .SetParameter("BoardId", BoardId)
                       .List<Domain.Myfashion.Domain.Boardgplusaccount>().ToList<Domain.Myfashion.Domain.Boardgplusaccount>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }

        public bool checkgPlusFeedExists(string feedsid, Guid BoardGplusPageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feed by messages id.
                        //NHibernate.IQuery query = session.CreateQuery("from Boardgplusfeeds where  FeedId = :msgid and  gplusboardaccProfileId=:profileid ");
                        //query.SetParameter("msgid", feedsid);
                        //query.SetParameter("profileid", BoardGplusPageId);
                        //var resutl = query.UniqueResult();

                        //if (resutl == null)
                        //    return false;
                        //else
                        //    return true;
                        bool exist = session.Query<Domain.Myfashion.Domain.Boardgplusfeeds>()
                             .Any(x => x.Feedid == feedsid && x.Gplusboardaccprofileid==BoardGplusPageId);
                        return exist;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End Session
        }


        public bool checkgBoardExists(string boardname)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feed by messages id.
                        //NHibernate.IQuery query = session.CreateQuery("from Boards where BoardName =:boardname");
                        //query.SetParameter("boardname", boardname);
                        //var resutl = query.UniqueResult();

                        //if (resutl == null)
                        //    return false;
                        //else
                        //    return true;

                        bool exist = session.Query<Domain.Myfashion.Domain.Boards>()
                             .Any(x => x.BoardName == boardname);
                        return exist;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End Session
        }

        public List<Domain.Myfashion.Domain.Boardgplusfeeds> getBoardGplusfeeds(Guid BoardGplusprofileId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardgplusfeeds> objlstfb = session.CreateQuery("from Boardgplusfeeds where gplusboardaccProfileId = :gplusboardaccProfileId ")
                            .SetParameter("gplusboardaccProfileId", BoardGplusprofileId)
                       .List<Domain.Myfashion.Domain.Boardgplusfeeds>().ToList<Domain.Myfashion.Domain.Boardgplusfeeds>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End session
        }


        public List<Domain.Myfashion.Domain.Boardgplusfeeds> getBoardGplusfeedsbyrange(Guid BoardGplusprofileId, int _noOfDataToSkip, int _noOfResultsFromTop)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardgplusfeeds> objlstfb = session.Query<Domain.Myfashion.Domain.Boardgplusfeeds>().Where(p => p.Gplusboardaccprofileid == BoardGplusprofileId).OrderByDescending(x => x.Publishedtime).Skip(Convert.ToInt32(_noOfDataToSkip)).Take(Convert.ToInt32(_noOfResultsFromTop)).ToList<Domain.Myfashion.Domain.Boardgplusfeeds>();
                    return objlstfb;
                }//End Transaction
            }//End session
        }

        public List<Domain.Myfashion.Domain.Boardgplusaccount> getAllBoardGplusAccounts()
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Myfashion.Domain.Boardgplusaccount> objlstfb = session.CreateQuery("from Boardgplusaccount")
                       .List<Domain.Myfashion.Domain.Boardgplusaccount>().ToList<Domain.Myfashion.Domain.Boardgplusaccount>();
                    //if (objlstfb.Count > 0)
                    //{
                    //    result = objlstfb[0];
                    //}
                    return objlstfb;
                }//End Transaction
            }//End se
        }
        # endregion


        #region Tags



        # endregion






        #region Delete

        public bool DeleteBoard(Domain.Myfashion.Domain.Boards board)
        {
            bool isUpdate = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update Boards set BoardName=:BoardName,FbProfileId=:fbPfofileId,GPlusProfileId=:gplusProfileId,Instagramprofileid=:instagramProfileId,Twitterprofileid=:twitterProfileId,CreateDate=:CreateDate,IsHidden=:IsHidden where BoardId = :BoardId")
                            .SetParameter("BoardName", board.BoardName)
                            .SetParameter("BoardId", board.BoardId)
                            .SetParameter("fbPfofileId", board.Fbprofileid)
                            .SetParameter("gplusProfileId", board.Gplusprofileid)
                            .SetParameter("instagramProfileId", board.Instagramprofileid)
                            .SetParameter("twitterProfileId", board.Twitterprofileid)
                             .SetParameter("CreateDate", board.CreateDate)
                              .SetParameter("IsHidden", true)
                            .ExecuteUpdate();
                        transaction.Commit();
                        isUpdate = true;
                        return isUpdate;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return isUpdate;
                    }
                }//End Transaction
            }//End session
        }


        public bool UndoDeleteBoard(Domain.Myfashion.Domain.Boards board)
        {
            bool isUpdate = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update Boards set BoardName=:BoardName,FbProfileId=:fbPfofileId,GPlusProfileId=:gplusProfileId,Instagramprofileid=:instagramProfileId,Twitterprofileid=:twitterProfileId,CreateDate=:CreateDate,IsHidden=:IsHidden where BoardId = :BoardId")
                            .SetParameter("BoardName", board.BoardName)
                            .SetParameter("BoardId", board.BoardId)
                            .SetParameter("fbPfofileId", board.Fbprofileid)
                            .SetParameter("gplusProfileId", board.Gplusprofileid)
                            .SetParameter("instagramProfileId", board.Instagramprofileid)
                            .SetParameter("twitterProfileId", board.Twitterprofileid)
                             .SetParameter("CreateDate", board.CreateDate)
                              .SetParameter("IsHidden", false)
                            .ExecuteUpdate();
                        transaction.Commit();
                        isUpdate = true;
                        return isUpdate;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return isUpdate;
                    }
                }//End Transaction
            }//End session
        }
        #endregion

    }
}