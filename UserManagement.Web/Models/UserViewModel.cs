using System.Collections.Generic;
using UserManagement.Core.Models;

namespace UserManagement.Web.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IList<Role> Roles { get; set; }
    }
}
