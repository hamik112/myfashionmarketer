using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Myfashion.Domain;

namespace Api.Socioboard.Model
{
    public interface ISocialSiteMessageRepository
    {
        void AddMessage(Domain.Myfashion.Domain.ISocialSiteMessage objSocialSiteMessage);

        void DeleteMessage(Domain.Myfashion.Domain.ISocialSiteMessage objSocialSiteMessage);

        void UpdateMessage(Domain.Myfashion.Domain.ISocialSiteMessage objSocialSiteMessage);

        List<Domain.Myfashion.Domain.ISocialSiteMessage> GetAllMessagesOfUser(Guid UserId, string profileId);

        bool CheckMessageExists(string Id, Guid Userid);

        void deleteAllMessagesOfUser(string fbuserid, Guid userid);

        List<Domain.Myfashion.Domain.FacebookMessage> getAllMessageOfProfile(string profileid);
    }
}
