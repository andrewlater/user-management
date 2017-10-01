using System;
using Paramore.Brighter;
using UserManagement.Core.Models;

namespace UserManagement.Core.Commands
{
    public class SaveUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public User User { get; }

        public SaveUserCommand(User user)
        {
            Id = Guid.NewGuid();
            User = user;
        }
    }
}
