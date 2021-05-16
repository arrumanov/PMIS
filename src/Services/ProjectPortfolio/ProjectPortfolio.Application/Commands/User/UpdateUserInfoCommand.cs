using System;

namespace ProjectPortfolio.Application.Commands.User
{
    public class UpdateUserInfoCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}