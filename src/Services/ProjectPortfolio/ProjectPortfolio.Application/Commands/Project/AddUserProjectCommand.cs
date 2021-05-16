﻿using System;

namespace ProjectPortfolio.Application.Commands.Project
{
    public class AddUserProjectCommand : CommandBase<bool>
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
    }
}