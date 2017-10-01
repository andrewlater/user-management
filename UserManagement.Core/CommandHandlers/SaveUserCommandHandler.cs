using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;
using UserManagement.Core.Commands;
using UserManagement.Core.Data;
using System.Linq;
using UserManagement.Core.Models;
using Paramore.Darker;
using UserManagement.Core.Queries;

namespace UserManagement.Core.CommandHandlers
{
    public class SaveUserCommandHandler : RequestHandlerAsync<SaveUserCommand>
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IDataSession _dataSession;

        public SaveUserCommandHandler(IQueryProcessor queryProcessor, IDataSession dataSession)
        {
            _queryProcessor = queryProcessor;
            _dataSession = dataSession;
        }

        public override async Task<SaveUserCommand> HandleAsync(
            SaveUserCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var updatedUser = command.User;
            var getUserQueryResult = await _queryProcessor.ExecuteAsync(new GetUserQuery(updatedUser.UserId));
            var dbUser = getUserQueryResult.User;

            dbUser.Username = updatedUser.Username;
            dbUser.FirstName = updatedUser.FirstName;
            dbUser.LastName = updatedUser.LastName;

            dbUser.UserRoles.ToList()
                .ForEach(userRole =>
                {
                    if (!updatedUser.UserRoles.Any(ur => ur.RoleId == userRole.RoleId))
                    {
                        dbUser.UserRoles.Remove(userRole);
                    }
                });

            var rolesToAdd = from userRole in updatedUser.UserRoles
                             where userRole.RoleId > 0 && !dbUser.UserRoles.Any(ur => ur.RoleId == userRole.RoleId)
                             join role in getUserQueryResult.Roles
                             on userRole.RoleId equals role.RoleId
                             select role;

            foreach (var role in rolesToAdd)
            {
                dbUser.UserRoles.Add(new UserRole { User = dbUser, Role = role });
            }

            _dataSession.SaveChanges();

            return command;
        }
    }
}
