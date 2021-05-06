﻿using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Model;
using Thread = System.Threading.Tasks;

namespace Dogovor.Infrastructure.Database.Command.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(GraphContext context) : base(context)
        {
        }

        public async Thread.Task AddUser(UserProject userProject)
        {
            await _Context.UserProjects.AddAsync(userProject);
        }

        public Thread.Task RemoveUser(UserProject userProject)
        {
            ClearChangeTrack<UserProject>();

            _Context.UserProjects.Remove(userProject);

            return Thread.Task.CompletedTask;
        }
    }
}