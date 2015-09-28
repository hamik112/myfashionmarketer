﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDataServices
{
    class clsSocialSiteDataFeedsFactory
    {
        ISocialSiteData objSocialSiteDataFeeds;
       
        public clsSocialSiteDataFeedsFactory
            (string networkType)
        {
            switch (networkType)
            {
                case "twitter":
                    objSocialSiteDataFeeds = new TwitterData();
                    break;
                case "linkedin":
                    objSocialSiteDataFeeds = new LinkedInData();
                    break;
                case "googleanalytics":
                    //objSocialSiteDataFeeds = new GoogleAnalyticsData();
                    break;
                case "googleplus":
                    //objSocialSiteDataFeeds = new Google();
                    break;
                case "facebook":
                    objSocialSiteDataFeeds = new FacebookData();
                    break;
                case "instagram":
                    objSocialSiteDataFeeds = new InstagramData();
                    break;
                case "tumblr":
                    objSocialSiteDataFeeds = new TumblrData();
                    break;
                case "youtube":
                    objSocialSiteDataFeeds = new YouTubeData();
                    break;
                case "facebook_page":
                    objSocialSiteDataFeeds = new FacebookFanPageData();
                    break;
                case "facebook page":
                    objSocialSiteDataFeeds = new FacebookPageData();
                    break;
                default:
                    break;
            }
        }

        public ISocialSiteData CreateSocialSiteDataFeedsInstance()
        {
            return objSocialSiteDataFeeds;
        }
    }
}
