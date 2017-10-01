using System;
using System.Collections.Generic;

namespace UserManagement.Core.Models
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }
        DateTime LastUpdateDate { get; set; }
    }

    public class User : IAuditable
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Hash { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        private ICollection<UserRole> _userRoles;
        public ICollection<UserRole> UserRoles
        {
            get
            {
                return _userRoles ?? (_userRoles = new List<UserRole>());
            }
        }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }

    public class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
