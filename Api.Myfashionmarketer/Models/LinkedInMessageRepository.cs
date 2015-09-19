using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using Api.Myfashionmarketer.Helper;

namespace Api.Myfashionmarketer.Models
{
    public class LinkedInMessageRepository
    {
        public void addLinkedInMessage(Domain.Myfashion.Domain.LinkedInMessage limsg)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(limsg);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public bool checkLinkedInMessageExists(string ProfileId,string FeedId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check if FacebookUser is Exist in database or not by UserId and FbuserId.
                        // And Set the reuired paremeters to find the specific values.
                        List<Domain.Myfashion.Domain.LinkedInMessage> alst = session.CreateQuery("from LinkedInMessage where UserId = :userid and ProfileId = :fbuserid and FeedId =: feedid")
                        .SetParameter("userid", Userid)
                        .SetParameter("fbuserid", ProfileId)
                        .SetParameter("feedid",FeedId)
                        .List<Domain.Myfashion.Domain.LinkedInMessage>()
                       .ToList<Domain.Myfashion.Domain.LinkedInMessage>();
                        if (alst.Count == 0 || alst == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End session
        }

        public List<Domain.Myfashion.Domain.LinkedInMessage> getLinkedInMessageDetail(string profileid, string noOfDataToSkip, Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Myfashion.Domain.LinkedInMessage> lstmsg = session.Query<Domain.Myfashion.Domain.LinkedInMessage>().Where(u => u.UserId == UserId && u.ProfileId == profileid).OrderByDescending(x => x.CreatedDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(10).ToList<Domain.Myfashion.Domain.LinkedInMessage>();
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Trasaction
            }//End session
        }

    }
}