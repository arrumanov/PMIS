namespace ProjectPortfolio.Application.Commands.Project
{
    public class AddProjectCommand : CommandBase<Infrastructure.Database.Query.Model.Project.Project>
    {
        public string Description { get; set; }
        public string LongDescription { get; set; }
    }
}