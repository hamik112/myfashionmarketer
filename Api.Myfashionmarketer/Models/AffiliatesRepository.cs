using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Myfashionmarketer.Helper;

namespace Api.Myfashionmarketer.Models
{
    public class AffiliatesRepository
    {
        public void Add(Domain.Myfashion.Domain.Affiliates affiliate)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(affiliate);
                    transaction.Commit();
                }
            }
        }

        public List<Domain.Myfashion.Domain.Affiliates> GetAffiliateDataByUserId(Guid UserId, Guid FriendsUserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Myfashion.Domain.Affiliates> lst = session.CreateQuery("from Affiliates where UserId=: userid and FriendUserId=:friendsuserid ORDER BY AffiliateDate DESC")
                                       .SetParameter("userid", UserId)
                                       .SetParameter("friendsuserid", FriendsUserId).List<Domain.Myfashion.Domain.Affiliates>().ToList<Domain.Myfashion.Domain.Affiliates>();
                        return lst;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


        public List<Domain.Myfashion.Domain.Affiliates> GetAffiliateDataByUserId(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Myfashion.Domain.Affiliates> lst = session.CreateQuery("from Affiliates where UserId=: userid ORDER BY AffiliateDate DESC")
                                       .SetParameter("userid", UserId)
                                       .List<Domain.Myfashion.Domain.Affiliates>().ToList<Domain.Myfashion.Domain.Affiliates>();
                        return lst;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

    }
}