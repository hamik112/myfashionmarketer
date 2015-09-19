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
    public class EmployeeRepository
    {
        public static ICollection<Employee> GetAllUsers()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                return session.CreateCriteria(typeof(Employee)).List<Employee>();

            }


        }
        public static void Add(Employee Employee)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(Employee);
                    transaction.Commit();
                }
            }
        }

        public List<Employee> Employees(Guid c_id)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Employee> result = session.Query<Employee>().Where(e => e.Company_id == c_id).ToList();
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

        public Employee contact(Guid empId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Employee e where e.Employee_id = : Employee_id");
                        query.SetParameter("Employee_id", empId);
                        Employee result = (Employee)query.UniqueResult();
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