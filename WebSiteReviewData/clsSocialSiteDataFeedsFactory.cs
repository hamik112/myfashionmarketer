using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSiteReviewData
{
    class clsSocialSiteDataFeedsFactory
    {
        ISocialSiteData objSocialSiteDataFeeds;
       
        public clsSocialSiteDataFeedsFactory
            (string networkType)
        {
            switch (networkType)
            {
                case "websitereview":
                    objSocialSiteDataFeeds = new WebSiteDataData();
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
