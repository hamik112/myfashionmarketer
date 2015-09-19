using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Myfashion.Domain
{
    public class GroupScheduleMessage
    {
        public Guid Id { get; set; }
        public Guid ScheduleMessageId { get; set; }
        public string GroupId { get; set; }
    }
}