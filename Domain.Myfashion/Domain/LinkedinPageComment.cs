using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Myfashion.Domain
{
    public class LinkdeinPageComment
    {
        public string Comment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CommentTime { get; set; }
        public string PictureUrl { get; set; }
    }
}
