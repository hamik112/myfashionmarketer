using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Myfashion.Domain
{
    public class FBTwitterFeeds
    {
        public FacebookFeed FacebookFeed { get; set; }
        public TwitterFeed TwitterFeed { get; set; }
        public int TicketNo { get; set; }
        public string AssignedUser { get; set; }
    }
}
