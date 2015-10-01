using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BaseLib;
using System.Text.RegularExpressions;
using Domain.Myfashion.Domain;
using Api.Myfashionmarketer.Models;
using System.Web.Script.Serialization;

namespace Api.Myfashionmarketer.Services
{
    /// <summary>
    /// Summary description for WebSiteReview
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebSiteReview : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public string WebSiteData(string url)
        {
            WebSiteReviewDataRpository objWebSiteReviewDataRpository = new WebSiteReviewDataRpository();
            websitereviewdata _WebSiteReviewData = new websitereviewdata();
            if (!objWebSiteReviewDataRpository.IswebsitenameExist(url))
            {
                
                string imageurl = "";
                string textname = "";
                string websitedescription = "";
                string GlobalRank = "";
                string CountryRank = "";
                string CategoryRank = "";
                string VisitersOnSite = "";
                string TimeOnSite = "";
                string WebSitePageViewers = "";
                string WebSiteBounceRate = "";
                string DirrectTrafficOnSite = "";
                string ReferralTrafficOnSite = "";
                string SearchTrafficeOnSite = "";
                string SocialTrafficeOnSite = "";
                string MailTrafficeOnSite = "";
                string DisplayTrafficOnSite = "";
                string toprefersitedata = "";
                string topdestiantionsites = "";
                string PaidSearch = "";
                string organickeyword = "";
                string paidkeyword = "";
                string socialsites = "";
                string sitesvalue = "";
                string display = "";
                string interestvalue = "";
                string audienceinterest = "";
                string visitedsites = "";
                string similarwebsite = "";
                string inmagesource = "";
                string appname = "";
                string inmagesourceapp = "";
                string appnameapp = "";
                string pagesource = "";
                string TrafficbyCountry = "";
                string Url = "http://www.similarweb.com/website/" + url;
                ChilkatHttpHelpr objChilkatHttpHelpr = new ChilkatHttpHelpr();
                GlobusHttpHelper _GlobusHttpHelper = new GlobusHttpHelper();
                 pagesource = _GlobusHttpHelper.getHtmlfromUrl(new Uri(Url));
                 if (string.IsNullOrEmpty(pagesource))
                 {
                     pagesource = _GlobusHttpHelper.getHtmlfromUrlProxy(new Uri(Url), "104.156.208.77", 80, "proxy123", "ashley1212");
                 }

                if (pagesource.Contains("stickyHeader-nameItem"))
                {
                    string requiredpagesource = Utils.getBetween(pagesource, "stickyHeader-nameItem", "class=\"icon-new-window stickyHeader-iconNew\"");
                    imageurl = Utils.getBetween(requiredpagesource, "src=\"", "\" />");
                    textname = Utils.getBetween(requiredpagesource, "\"stickyHeader-nameText\">", "</span>");
                }
                if (pagesource.Contains("analysis-descriptionText"))
                {
                    string descriptionsource = Utils.getBetween(pagesource, "analysis-descriptionText", "stickyHeader-relatedAppsSection");
                    websitedescription = Utils.getBetween(descriptionsource, "\">", "</div>");

                }
                if (pagesource.Contains("rankingSection"))
                {
                    string rankepagesource = Utils.getBetween(pagesource, "rankingSection", "rankingItem-embed");
                    string globalrank = Utils.getBetween(rankepagesource, "data-value=\"", "/span>");
                    GlobalRank = Utils.getBetween(globalrank, "\">", "<");

                }
                if (pagesource.Contains("Category Rank"))
                {
                    string countryrankpagesource = Utils.getBetween(pagesource, "Country Rank", "Category Rank");
                    countryrankpagesource = Utils.getBetween(countryrankpagesource, "data-value=\"", "/span>");
                    CountryRank = Utils.getBetween(countryrankpagesource, "\">", "<");

                    string categorypagesource = Utils.getBetween(pagesource, "Category Rank", "Traffic Overview");
                    categorypagesource = Utils.getBetween(categorypagesource, "data-value=\"", "/span>");
                    CategoryRank = Utils.getBetween(categorypagesource, "\">", "<");
                }

                if (pagesource.Contains("Estimated Monthly Visits"))
                {
                    string EstimatedMonthlyVisits = Utils.getBetween(pagesource, "Engagement", "Traffic by countries");
                    string visits = Utils.getBetween(EstimatedMonthlyVisits, "visits", "Time on site");
                    visits = Utils.getBetween(visits, "engagementInfo-value engagementInfo-value--large u-text-ellipsis", "span>");
                    VisitersOnSite = Utils.getBetween(visits, "\">", "<");

                    string timeonsite = Utils.getBetween(EstimatedMonthlyVisits, "Time on site", "Page views");
                    timeonsite = Utils.getBetween(timeonsite, "engagementInfo-value u-text-ellipsis", "/span>");

                    TimeOnSite = Utils.getBetween(timeonsite, "\">", "<");


                    string pageview = Utils.getBetween(EstimatedMonthlyVisits, "Page views", "Bounce rate");
                    pageview = Utils.getBetween(pageview, "engagementInfo-value u-text-ellipsis", "/span>");

                    WebSitePageViewers = Utils.getBetween(pageview, "\">", "<");


                    string bouncerate = Utils.getBetween(EstimatedMonthlyVisits, "Bounce rate", "geo");
                    bouncerate = Utils.getBetween(bouncerate, "engagementInfo-value u-text-ellipsis", "/span>");
                    WebSiteBounceRate = Utils.getBetween(bouncerate, "\">", "<");
                }

                if (pagesource.Contains("WebsitePageModule.Views.MapView"))
                {
                    string trafficbycountry = Utils.getBetween(pagesource, "WebsitePageModule.Views.MapView", "websitePage-contentNarrow websitePage-contentRight geo-accordion");
                    TrafficbyCountry = Utils.getBetween(trafficbycountry, "&quot;Country&quot;, &quot;Share&quot;],", "]}'></div>").Replace("&quot;", "");
                }
                if (pagesource.Contains("trafficSourcesSection"))
                {

                    string trafficsource = Utils.getBetween(pagesource, "trafficSourcesChart-list", "analysisPage-section websitePage-referrals hideInCompared");
                    string trafficsourcedirrect = Utils.getBetween(trafficsource, "trafficSourcesChart-item direct", "icon-direct trafficSourcesChart-icon");
                    DirrectTrafficOnSite = Utils.getBetween(trafficsourcedirrect, "<div class=\"trafficSourcesChart-value\">", "</div>");

                    string refferalsource = Utils.getBetween(trafficsource, "trafficSourcesChart-item referrals", "icon-referrals trafficSourcesChart-icon");
                    ReferralTrafficOnSite = Utils.getBetween(refferalsource, "<div class=\"trafficSourcesChart-value\">", "</div>");

                    string searchtrafficsource = Utils.getBetween(trafficsource, "trafficSourcesChart-item search", "icon-search trafficSourcesChart-icon");
                    SearchTrafficeOnSite = Utils.getBetween(searchtrafficsource, "<div class=\"trafficSourcesChart-value\">", "</div>");

                    string socialtrafficesource = Utils.getBetween(trafficsource, "trafficSourcesChart-item social", "icon-social trafficSourcesChart-icon");
                    SocialTrafficeOnSite = Utils.getBetween(socialtrafficesource, "<div class=\"trafficSourcesChart-value\">", "</div>");

                    string mailtrafficesource = Utils.getBetween(trafficsource, "trafficSourcesChart-item mail", "icon-mail trafficSourcesChart-icon");
                    MailTrafficeOnSite = Utils.getBetween(mailtrafficesource, "<div class=\"trafficSourcesChart-value\">", "</div>");


                    string displaytrafficesource = Utils.getBetween(trafficsource, "trafficSourcesChart-item display", "icon-display trafficSourcesChart-icon");
                    DisplayTrafficOnSite = Utils.getBetween(displaytrafficesource, "<div class=\"trafficSourcesChart-value\">", "</div>");
                }
                if (pagesource.Contains("analysisPage-section websitePage-referrals hideInCompared"))
                {
                    string topsitereferrral = Utils.getBetween(pagesource, "Top Referring Sites", "searchSection analysisPage-section websitePage-search hideInCompared");
                    string toprefersite = Utils.getBetween(topsitereferrral, "Top Referring Sites", "Top Destination Sites:");
                    string[] reqdataarr = Regex.Split(toprefersite, "class=\"websitePage-listItemLink");

                    foreach (var item in reqdataarr)
                    {


                        if (item.Contains("\"Internal Link\""))
                        {

                            string topsitename = Utils.getBetween(item, "\">", "</a>");
                            toprefersitedata = topsitename + "," + toprefersitedata;
                        }
                    }

                    string topdestinationsite = Utils.getBetween(topsitereferrral, "Top Destination Sites", "websitePage-blueButton websitePage-sectionButton referrals-hookButton js-proPopup");

                    string[] topdestiantion = Regex.Split(topdestinationsite, "websitePage-listItemLink js-tooltipTarget");


                    foreach (var item_site in topdestiantion)
                    {

                        if (item_site.Contains("\"Internal Link\""))
                        {
                            string topdetsianton = Utils.getBetween(item_site, "\">", "</a>");
                            topdestiantionsites = topdetsianton + "," + topdestiantionsites;
                        }
                    }

                }
                if (pagesource.Contains("searchPie-text searchPie-text--left"))
                {
                    string serchvalue = Utils.getBetween(pagesource, "searchPie-text searchPie-text--left", "searchKeywords-text searchKeywords-text--left websitePage-mobileFramed");
                    string organicsearch = Utils.getBetween(serchvalue, "<span class=\"searchPie-number\">", "</span>");
                    string paidserach = Utils.getBetween(serchvalue, "<div class=\"searchPie-text searchPie-text--right  \">", "/span>");
                    PaidSearch = Utils.getBetween(paidserach, "\">", "<");
                }

                if (pagesource.Contains("Organic Keywords"))
                {
                    string keyword = Utils.getBetween(pagesource, "Organic Keywords", "socialSection analysisPage-section websitePage-social hideInCompared");
                    string keywordvalue = Utils.getBetween(keyword, "searchKeywords-list", "websitePage-sectionButton u-button--wide js-proPopup");
                    string[] onganickeywordarry = Regex.Split(keywordvalue, "class='searchKeywords-words'");


                    foreach (var item in onganickeywordarry)
                    {
                        if (item.Contains("title=\""))
                        {
                            string valuekeyword = Utils.getBetween(item, "\">", "</span>");
                            organickeyword = valuekeyword + "," + organickeyword;
                        }
                    }

                    string paidvalue = Utils.getBetween(keyword, "websitePage-sectionButton u-button--wide js-proPopup", "social");
                    paidkeyword = Utils.getBetween(paidvalue, "search-noDataMessage\">", "</div>").Replace("\n", "");

                }


                if (pagesource.Contains("socialSection analysisPage-section websitePage-social hideInCompared"))
                {
                    string socialvalue = Utils.getBetween(pagesource, "socialSection analysisPage-section websitePage-social hideInCompared", "Display Advertising");
                    string[] socialsitesvalue = Regex.Split(socialvalue, "class=\"socialItem\"");

                    foreach (var item in socialsitesvalue)
                    {
                        if (item.Contains("Internal Link"))
                        {
                            string data = Utils.getBetween(item, "Internal Link\"", "/a>");
                            data = Utils.getBetween(data, "'>", "<");
                            socialsites = data + "," + socialsites;

                            string value = Utils.getBetween(item, "socialItem-value\">", "</div>");
                            sitesvalue = value + "," + sitesvalue;
                        }
                    }

                }

                if (pagesource.Contains("Display Advertising"))
                {
                    string displayvalue = Utils.getBetween(pagesource, "Display Advertising", "Audience Interests");
                    display = Utils.getBetween(displayvalue, "class=\"noData-title display\">", "</h2>");
                }
                if (pagesource.Contains("Audience Interests"))
                {
                    string audiencesvalue = Utils.getBetween(pagesource, "Audience Interests", "Also visited websites");
                    string[] audiencedata = Regex.Split(audiencesvalue, "audienceCategories-item fadeInLeft");

                    foreach (var item in audiencedata)
                    {
                        if (item.Contains("audienceCategories-chartContainer"))
                        {
                            string values = Utils.getBetween(item, "\"fillValue\":", "\"innerSize\"").Replace(",", "");
                            interestvalue = values + "," + interestvalue;

                            string intreset = Utils.getBetween(item, "audienceCategories-itemLink", "/a>");
                            intreset = Utils.getBetween(intreset, "\">", "<");
                            audienceinterest = intreset + "," + audienceinterest;
                        }
                    }
                }
                if (pagesource.Contains("Also visited websites"))
                {
                    string alsovisitedvalue = Utils.getBetween(pagesource, "Also visited websites", "Similar Sites");
                    string[] data = Regex.Split(alsovisitedvalue, "websitePage-listItemContainer");

                    foreach (var item in data)
                    {
                        if (item.Contains("Internal Link"))
                        {
                            string value = Utils.getBetween(item, "Internal Link", "/a>");
                            value = Utils.getBetween(value, "\">", "<");
                            visitedsites = value + "," + visitedsites;
                        }
                    }
                }
                if (pagesource.Contains("Similar Sites"))
                {
                    string SimilaSitesdata = Utils.getBetween(pagesource, "Similar Sites", "Related Mobile Apps");
                    string[] data = Regex.Split(SimilaSitesdata, "compareModal-row-cell-title");

                    foreach (var item in data)
                    {
                        if (item.Contains("data-url="))
                        {
                            string value = Utils.getBetween(item, "\">", "</span>");
                            similarwebsite = value + "," + similarwebsite;
                        }
                    }
                }
                if (pagesource.Contains("analysisPage-section websitePage-websiteMobileApps hideInCompared"))
                {
                    string RelatedMobilAppsdata = Utils.getBetween(pagesource, "analysisPage-section websitePage-websiteMobileApps hideInCompared", "Get More with SimilarWeb PRO");
                    string data = Utils.getBetween(RelatedMobilAppsdata, "Google Play Store", "App Store");
                    string[] dataarry = Regex.Split(data, "data-analytics-category=\"Internal Link\"");

                    foreach (var item in dataarry)
                    {
                        if (item.Contains("data-analytics-label="))
                        {
                            string valueimgaesrc = Utils.getBetween(item, "data-original=\"", "alt=\"").Replace("\"", "");
                            inmagesource = valueimgaesrc + "," + inmagesource;
                            string valueappname = Utils.getBetween(item, "itemprop=\"name\"", "/span>");
                            valueappname = Utils.getBetween(valueappname, ">", "<").Replace("\n", "").Trim();
                            appname = valueappname + "," + appname;

                        }
                    }

                    string appdatavlue = Utils.getBetween(RelatedMobilAppsdata, "App Store", "u-button--middle websitePage-pinkStrongButton websitePage-sectionButton js-proPopup");
                    string[] datavalue = Regex.Split(appdatavlue, "mobileApps-appList");

                    foreach (var item in datavalue)
                    {
                        if (item.Contains("data-options="))
                        {
                            string valueimgaesrc = Utils.getBetween(item, "data-original=\"", "alt=\"").Replace("\"", "");
                            inmagesourceapp = valueimgaesrc + "," + inmagesourceapp;
                            string valueappname = Utils.getBetween(item, "itemprop=\"name\"", "/span>");
                            valueappname = Utils.getBetween(valueappname, ">", "<").Replace("\n", "").Trim();
                            appnameapp = valueappname + "," + appnameapp;
                        }

                    }
                }
                _WebSiteReviewData.Id = Guid.NewGuid();
                _WebSiteReviewData.imageurl = imageurl;
                _WebSiteReviewData.googleinmagesource = inmagesource;
                _WebSiteReviewData.inmagesourceapp = inmagesourceapp;
                _WebSiteReviewData.interestvalue = interestvalue;
                _WebSiteReviewData.MailTrafficeOnSite = MailTrafficeOnSite;
                _WebSiteReviewData.organickeyword = organickeyword;
                _WebSiteReviewData.paidkeyword = paidkeyword;
                _WebSiteReviewData.PaidSearch = PaidSearch;
                _WebSiteReviewData.ReferralTrafficOnSite = ReferralTrafficOnSite;
                _WebSiteReviewData.SearchTrafficeOnSite = SearchTrafficeOnSite;
                _WebSiteReviewData.similarwebsite = similarwebsite;
                _WebSiteReviewData.sitesvalue = sitesvalue;
                _WebSiteReviewData.socialsites = socialsites;
                _WebSiteReviewData.SocialTrafficeOnSite = socialsites;
                _WebSiteReviewData.textname = textname;
                _WebSiteReviewData.TimeOnSite = TimeOnSite;
                _WebSiteReviewData.topdestiantionsites = topdestiantionsites;
                _WebSiteReviewData.toprefersitedata = toprefersitedata;
                _WebSiteReviewData.visitedsites = visitedsites;
                _WebSiteReviewData.VisitersOnSite = VisitersOnSite;
                _WebSiteReviewData.WebSiteBounceRate = WebSiteBounceRate;
                _WebSiteReviewData.websitedescription = websitedescription;
                _WebSiteReviewData.websitename = url;
                _WebSiteReviewData.WebSitePageViewers = WebSitePageViewers;
                _WebSiteReviewData.googleappname = appname;
                _WebSiteReviewData.appnameapp = appnameapp;
                _WebSiteReviewData.audienceinterest = audienceinterest;
                _WebSiteReviewData.CategoryRank = CategoryRank;
                _WebSiteReviewData.CountryRank = CountryRank;
                _WebSiteReviewData.DirrectTrafficOnSite = DisplayTrafficOnSite;
                _WebSiteReviewData.display = display;
                _WebSiteReviewData.DisplayTrafficOnSite = DisplayTrafficOnSite;
                _WebSiteReviewData.GlobalRank = GlobalRank;
                _WebSiteReviewData.TrafficbyCountry = TrafficbyCountry;
                WebSiteReviewDataRpository.Add(_WebSiteReviewData);
            }
            else {
                _WebSiteReviewData = objWebSiteReviewDataRpository.getUserInfoBywebsitename(url);
            
            }


            return new JavaScriptSerializer().Serialize(_WebSiteReviewData);
        }
    }
}
