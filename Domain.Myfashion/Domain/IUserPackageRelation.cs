using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Myfashion.Domain
{
    public interface IUserPackageRelation
    {
        List<UserPackageRelation> getAllUserPackageRelation();
        void AddUserPackageRelation(UserPackageRelation userPackageRelation);
        int DeleteUserPackageRelation(UserPackageRelation userPackageRelation);
        int UpdateUserPackageRelation(User userPackageRelation);
    }
}
