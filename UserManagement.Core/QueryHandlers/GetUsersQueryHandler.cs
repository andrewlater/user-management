using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Paramore.Darker;
using UserManagement.Core.Queries;
using UserManagement.Core.Data;
using UserManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Core.QueryHandlers
{
    public sealed class GetUsersQueryHandler : QueryHandlerAsync<GetUsersQuery, GetUsersQuery.Result>
    {
        private readonly IDataSession _dataSession;

        public GetUsersQueryHandler(IDataSession dataSession)
        {
            _dataSession = dataSession;
        }

        public override async Task<GetUsersQuery.Result> ExecuteAsync(
            GetUsersQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var users = await _dataSession.Set<User>()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return new GetUsersQuery.Result(users);
        }
    }
}
