using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Myfashionmarketer.Helper;
using Domain.Myfashion.Domain;

namespace Api.Myfashionmarketer.Models
{
    public class ReplyMessageRepository 
    {
        /// <AddPackage>
        /// Add Package
        /// </summary>
        /// <param name="package">Set Values in a Package Class Property and Pass the Object of Package Class.(Domein.Package)</param>
        public void AddReplyMessage(Domain.Myfashion.Domain.ReplyMessage ReplyMessage)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(ReplyMessage);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

    }
}