using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NHibernate.Linq;
using Domain.Myfashion.Domain;
using Api.Myfashionmarketer.Helper;

namespace Api.Myfashionmarketer.Models
{
    public class UsersRepository
    {
        public static void addUsers(Users user)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }
    }
}