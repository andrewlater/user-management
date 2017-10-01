using Paramore.Darker;
using UserManagement.Core.Queries;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Core.Data;
using UserManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Core.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandlerAsync<GetUserQuery, GetUserQuery.Result>
    {
        private readonly IDataSession _dataSession;

        public GetUserQueryHandler(IDataSession dataSession)
        {
            _dataSession = dataSession;
        }

        public override async Task<GetUserQuery.Result> ExecuteAsync(
            GetUserQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _dataSession.Set<User>()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstAsync(u => u.UserId == query.UserId);

            var roles = await _dataSession.Set<Role>()
                .ToListAsync();

            return new GetUserQuery.Result(user, roles);
        }
    }
}
