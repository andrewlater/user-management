using Paramore.Darker;
using System.Collections.Generic;
using UserManagement.Core.Models;

namespace UserManagement.Core.Queries
{
    public class GetUserQuery : IQuery<GetUserQuery.Result>
    {
        public int UserId { get; }

        public GetUserQuery(int userId)
        {
            UserId = userId;
        }

        public sealed class Result
        {
            public User User { get; }

            public IList<Role> Roles { get; }

            public Result(User user, IList<Role> roles)
            {
                User = user;
                Roles = roles;
            }
        }
    }
}
