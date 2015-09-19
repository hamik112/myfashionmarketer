using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Myfashionmarketer.Helper;
using Domain.Myfashion.Domain;
namespace Api.Myfashionmarketer.Models
{
    public class GroupScheduleMessageRepository
    {

        public void AddGroupScheduleMessage(Domain.Myfashion.Domain.GroupScheduleMessage _GroupScheduleMessage)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_GroupScheduleMessage);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public Domain.Myfashion.Domain.GroupScheduleMessage GetScheduleMessageId(Guid ScheduleMessageId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GroupScheduleMessage  where ScheduleMessageId = : schedulemessageid");
                        query.SetParameter("schedulemessageid", ScheduleMessageId);

                        Domain.Myfashion.Domain.GroupScheduleMessage result = (Domain.Myfashion.Domain.GroupScheduleMessage)query.UniqueResult();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }


    }
}