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
    public class Customersrepository
    {

        public Customers  Add(Customers cust)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(cust);
                    transaction.Commit();
                    int id = cust.Customerid;
                    return cust;
                }
            }
        }
        public int lastRecord() {
            
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<Customers>().OrderByDescending(x => x.Customerid).Take(1);
                List<Customers> _Customers = query.ToList();
                return _Customers[0].Customerid;
            }
        }
    }
}