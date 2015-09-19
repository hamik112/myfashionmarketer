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
    public class AccountRepository
    {
        public static ICollection<Account> GetAllUsers()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                return session.CreateCriteria(typeof(Account)).List<Account>();

            }


        }
        public static  void Add(Account account)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(account);
                    transaction.Commit();
                  
                }
            }
        }
        public List<Account> getAccountDetails(string EmailId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Account> result = session.Query<Account>().Where(u => u.EmailId == EmailId).ToList();
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

        public Account getuserAccountDetails(Guid comp_id)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("from  Account ac where ac.Company_id = : comp_id")
                            .SetParameter("comp_id", comp_id);
                            Account _account = (Account)query.UniqueResult();

                            return _account;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return null;
                        }



                    }
                }
            }
            catch
            {
                return null;

            }

        }

        public string updateAccount(string Accid, Account account)
        {
            try
            {
                int i = 0;
                //     Account account = new Account();
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            i = session.CreateQuery("Update Account set Business_name=:Business_name,Company_url=:Company_url,Tags=:Tags,Indusrties=:Indusrties,Default_no=:Default_no,Country_name=:Country_name,City=:City,Zip=:Zip  where Company_id = :Company_id")
                                    .SetParameter("Business_name", account.Business_name)

                                    .SetParameter("Company_url", account.Company_url)
                                    .SetParameter("Tags", account.Tags)
                                    .SetParameter("Indusrties", account.Indusrties)
                                    .SetParameter("Default_no", account.Default_no)
                                    .SetParameter("Country_name", account.Country_name)
                                    .SetParameter("City", account.City)
                                    .SetParameter("Zip", account.Zip)
                                    .SetParameter("Company_id", Accid)
                                    .ExecuteUpdate();
                            transaction.Commit();

                            return "updated";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return null;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

    }
}