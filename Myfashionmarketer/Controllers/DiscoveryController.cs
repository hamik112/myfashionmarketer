using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Api.Myfashionmarketer.Models;

namespace Myfashionmarketer.Controllers
{
    
    public class DiscoveryController :Controller
    {
        //
        // GET: /Discovery/

        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
                
        }
        public ActionResult LoadDiscovery()
        {
            // Edited by Antima

            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<string> lstSearchHistory = new List<string>();
            lstSearchHistory = (List<string>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.getAllSearchKeywords(objUser.Id.ToString()), typeof(List<string>)));

            return PartialView("_DiscoveryPartial", lstSearchHistory);
        }

        public ActionResult SearchFacebook(string keyword)
        {
            keyword = Uri.EscapeDataString(keyword);
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Myfashion.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Myfashion.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Myfashion.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.DiscoverySearchFacebook(objUser.Id.ToString(), keyword), typeof(List<Domain.Myfashion.Domain.DiscoverySearch>)));
            return PartialView("_SearchFacebookPartial", lstDiscoverySearch);
        }
        public ActionResult SearchTwitter(string keyword)
        {
            keyword = Uri.EscapeDataString(keyword);
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Myfashion.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Myfashion.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Myfashion.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.DiscoverySearchTwitter(objUser.Id.ToString(), keyword), typeof(List<Domain.Myfashion.Domain.DiscoverySearch>)));
            return PartialView("_SearchTwitterPartial", lstDiscoverySearch);
        }

        public ActionResult SearchGplus(string keyword)
        {
            keyword = Uri.EscapeDataString(keyword);
            Domain.Myfashion.Domain.User objUser = (Domain.Myfashion.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Myfashion.Domain.DiscoverySearch> GplusDiscoverySearch = new List<Domain.Myfashion.Domain.DiscoverySearch>();
            GplusDiscoverySearch = (List<Domain.Myfashion.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.DiscoverySearchGplus(objUser.Id.ToString(), keyword), typeof(List<Domain.Myfashion.Domain.DiscoverySearch>)));
            return PartialView("_SearchGplusPartial", GplusDiscoverySearch);
        }

        public ActionResult GetUrls(string keywords)
        {
            Api.DiscoverySearch.DiscoverySearch apiLinkBuilder = new Api.DiscoverySearch.DiscoverySearch();
            List<string> _lstUrl = new List<string>();
            string[] keys = Regex.Split(keywords, ",");
            foreach (string item in keys)
            {
                try
                {
                    List<string> _lsttwt = (List<string>)new JavaScriptSerializer().Deserialize(apiLinkBuilder.TwitterLinkBuilder(item), typeof(List<string>));
                    if (_lsttwt.Count > 0)
                    {
                        _lstUrl.AddRange(_lsttwt);
                    }
                }
                catch { }
                try
                {
                    List<string> _lstgplus = (List<string>)new JavaScriptSerializer().Deserialize(apiLinkBuilder.GPlusLinkBuilder(item), typeof(List<string>));
                    if (_lstgplus.Count > 0)
                    {
                        _lstUrl.AddRange(_lstgplus);
                    }
                }
                catch { }
            }
            ExportUrlsToCSV(_lstUrl);
            return View();

        }
        public void ExportUrlsToCSV(List<string> _lst)
        {
            try
            {
                var details = new System.Data.DataTable("Urls");

                details.Columns.Add("Urls", typeof(string));
                foreach (string item_url in _lst)
                {
                    details.Rows.Add(item_url);
                }
                var grid = new GridView();
                grid.DataSource = details;
                grid.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Urls_" + (DateTime.Now.Ticks).ToString() + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }

      
    }
}
