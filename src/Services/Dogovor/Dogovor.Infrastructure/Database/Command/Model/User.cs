﻿using System;
using System.Collections.Generic;
using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Command.Model
{
    public class User : IModel
    {
        public User()
        {
            UserProjects = new HashSet<UserProject>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }
    }
}