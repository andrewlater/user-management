using Paramore.Darker;
using System.Collections.Generic;
using UserManagement.Core.Models;

namespace UserManagement.Core.Queries
{
    public class GetUsersQuery : IQuery<GetUsersQuery.Result>
    {
        public sealed class Result
        {
            public IEnumerable<User> Users { get; }

            public Result(IEnumerable<User> users)
            {
                Users = users;
            }
        }
    }
}
