using System.Linq;
using UserManagement.Core.Models;
using UserManagement.Core.Security;

namespace UserManagement.Core.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UserManagementContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                var userRole = new Role { Name = "User" };
                var administratorRole = new Role { Name = "Administrator" };
                var systemOwnerRole = new Role { Name = "Owner" };

                context.Roles.Add(userRole);
                context.Roles.Add(administratorRole);
                context.Roles.Add(systemOwnerRole);

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var systemOwnerRole = context.Roles.First(role => role.Name == "Owner");
                var systemOwnerUser = new User { Username = "SystemOwner", Hash = PasswordStorage.CreateHash("Password") };
                systemOwnerUser.UserRoles.Add(new UserRole { User = systemOwnerUser, Role = systemOwnerRole });

                context.Users.Add(systemOwnerUser);

                context.SaveChanges();
            }
        }
    }
}
