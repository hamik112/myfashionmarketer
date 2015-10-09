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
    public class WebSiteReviewDataRpository
    {
        public static void Add(websitereviewdata user)
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

        public bool IswebsitenameExist(string websitename)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from  websitereviewdata u where u.websitename = : websitename");
                        query.SetParameter("websitename", websitename);
                        var result = query.UniqueResult();
                        if (result == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }
            }
        }

        public websitereviewdata getUserInfoBywebsitename(string websitename)
        {
            List<websitereviewdata> lstUser = new List<websitereviewdata>();
            websitereviewdata user = new websitereviewdata();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
               {
                    try
                    {
                        lstUser = session.CreateQuery("from  websitereviewdata u where u.websitename = : websitename")
                        .SetParameter("websitename", websitename).List<websitereviewdata>().ToList<websitereviewdata>();
                        user = lstUser[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        user = null;
                    }
                }
            }

            return user;
        }


        public websitereviewdata getinfoforupdate(Guid Id, string Url)
        {
            websitereviewdata user = new websitereviewdata();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        List<websitereviewdata> lstdata = session.CreateQuery("from websitereviewdata u where u.websitename = : websitename and u.Id=:Id")
                                        .SetParameter("websitename", Url).SetParameter("Id", Id).List<websitereviewdata>().ToList<websitereviewdata>();
                        user = lstdata[0];
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        user = null;
                    }
                
                }
            
            }
            return user;
        
        
        }

        public int deleteolddata(string websitename)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from websitereviewdata u where u.websitename = : websitename ")
                                        .SetParameter("websitename", websitename);
                        int i = query.ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                
                }            
            }
        
        }

        public List<websitereviewdata> GetAllwebsite()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<websitereviewdata> lst = session.Query<websitereviewdata>().ToList<websitereviewdata>();
                        return lst;
                    }
                    catch (Exception ex)
                    {
                        return new List<websitereviewdata>();
                    }
                
                
                }
            
            
            }
        
        }

        public void UpdateWebSiteData(websitereviewdata objwebsitereviewdata)
        { 
         using (NHibernate.ISession session=SessionFactory.GetNewSession())
         {

             using (NHibernate.ITransaction transaction = session.BeginTransaction())
             {

                 NHibernate.IQuery query = session.CreateQuery("update websitereviewdata set websitename =:websitename,TrafficbyCountry=:TrafficbyCountry,imageurl=:imageurl,textname=:textname,websitedescription=:websitedescription,GlobalRank=:GlobalRank,CountryRank=:CountryRank,CategoryRank=:CategoryRank,VisitersOnSite=:VisitersOnSite,TimeOnSite=:TimeOnSite,WebSitePageViewers=:WebSitePageViewers,WebSiteBounceRate=:WebSiteBounceRate,DirrectTrafficOnSite=:DirrectTrafficOnSite,ReferralTrafficOnSite=:ReferralTrafficOnSite,SearchTrafficeOnSite=:SearchTrafficeOnSite,SocialTrafficeOnSite=:SocialTrafficeOnSite,MailTrafficeOnSite=:MailTrafficeOnSite,DisplayTrafficOnSite=:DisplayTrafficOnSite,toprefersitedata=:toprefersitedata,topdestiantionsites=:topdestiantionsites,PaidSearch=:PaidSearch,organickeyword=:organickeyword,paidkeyword=:paidkeyword,socialsites=:socialsites,sitesvalue=:sitesvalue,display=:display,interestvalue=:interestvalue,audienceinterest=:audienceinterest,visitedsites=:visitedsites,similarwebsite=:similarwebsite,googleinmagesource=:googleinmagesource,googleappname=:googleappname,inmagesourceapp=:inmagesourceapp,appnameapp=:appnameapp,relatedgoogleimageurl=:relatedgoogleimageurl,relatedappimageurl=:relatedappimageurl where Id =:Id and websitename =:websitename");
                 query.SetParameter("appnameapp", objwebsitereviewdata.appnameapp)
                     .SetParameter("audienceinterest", objwebsitereviewdata.audienceinterest)
                     .SetParameter("CategoryRank", objwebsitereviewdata.CategoryRank)
                     .SetParameter("CountryRank", objwebsitereviewdata.CountryRank)
                     .SetParameter("DirrectTrafficOnSite", objwebsitereviewdata.DirrectTrafficOnSite)
                     .SetParameter("display", objwebsitereviewdata.display)
                     .SetParameter("DisplayTrafficOnSite", objwebsitereviewdata.DisplayTrafficOnSite)
                     .SetParameter("GlobalRank", objwebsitereviewdata.GlobalRank)
                     .SetParameter("googleappname", objwebsitereviewdata.googleappname)
                     .SetParameter("googleinmagesource", objwebsitereviewdata.googleinmagesource)
                     .SetParameter("Id", objwebsitereviewdata.Id)
                     .SetParameter("imageurl", objwebsitereviewdata.imageurl)
                     .SetParameter("inmagesourceapp", objwebsitereviewdata.inmagesourceapp)
                     .SetParameter("interestvalue", objwebsitereviewdata.interestvalue)
                     .SetParameter("MailTrafficeOnSite", objwebsitereviewdata.MailTrafficeOnSite)
                     .SetParameter("organickeyword", objwebsitereviewdata.organickeyword)
                     .SetParameter("paidkeyword", objwebsitereviewdata.paidkeyword)
                     .SetParameter("PaidSearch", objwebsitereviewdata.PaidSearch)
                     .SetParameter("ReferralTrafficOnSite", objwebsitereviewdata.ReferralTrafficOnSite)
                     .SetParameter("relatedappimageurl", objwebsitereviewdata.relatedappimageurl)
                     .SetParameter("relatedgoogleimageurl", objwebsitereviewdata.relatedgoogleimageurl)
                     .SetParameter("SearchTrafficeOnSite", objwebsitereviewdata.SearchTrafficeOnSite)
                     .SetParameter("similarwebsite", objwebsitereviewdata.similarwebsite)
                     .SetParameter("sitesvalue", objwebsitereviewdata.sitesvalue)
                     .SetParameter("socialsites", objwebsitereviewdata.socialsites)
                     .SetParameter("SocialTrafficeOnSite", objwebsitereviewdata.SocialTrafficeOnSite)
                     .SetParameter("textname", objwebsitereviewdata.textname)
                     .SetParameter("TimeOnSite", objwebsitereviewdata.TimeOnSite)
                     .SetParameter("topdestiantionsites", objwebsitereviewdata.topdestiantionsites)
                     .SetParameter("toprefersitedata", objwebsitereviewdata.toprefersitedata)
                     .SetParameter("TrafficbyCountry", objwebsitereviewdata.TrafficbyCountry)
                     .SetParameter("visitedsites", objwebsitereviewdata.visitedsites)
                     .SetParameter("VisitersOnSite", objwebsitereviewdata.VisitersOnSite)
                     .SetParameter("WebSiteBounceRate", objwebsitereviewdata.WebSiteBounceRate)
                     .SetParameter("websitedescription", objwebsitereviewdata.websitedescription)
                     .SetParameter("websitename", objwebsitereviewdata.websitename)
                     .SetParameter("WebSitePageViewers", objwebsitereviewdata.WebSitePageViewers);
                     query.ExecuteUpdate();
                     transaction.Commit();
             
             }


         }
        
        
        
        }
    }
}