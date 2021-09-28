using System.Collections.Generic;
using Camunda.Api.Client.UserTask;
using Workflow.Api.Bpmn;

namespace Workflow.Api.Graph.Project.Types
{
    public class TaskPayload
    {
        public string TaskId { get; set; }

        public string TaskType { get; set; }

        public string TaskName { get; set; }

        public string Assignee { get; set; }
        
        public string ObjectWfId { get; set; }
        public string ObjectId { get; set; }
        public string ObjectName { get; set; }

        public string RequestedSuperpower { get; set; }

        public string ObjectStatus { get; set; }

        public List<TaskActions> Actions { get; set; }

        public static TaskPayload FromEntity(UserTaskInfo task, Domain.ProjectWf project)
        {
            return new TaskPayload
            {
                TaskId = task.Id,
                TaskType = task.TaskDefinitionKey,
                TaskName = task.Name,
                Assignee = task.Assignee,
                ObjectWfId = project?.Id.ToString(),
                ObjectId = project?.ObjectId.ToString(),
                ObjectName = project?.ObjectName,
                ObjectStatus = project?.Status.ToString(),
                Actions = task.AvailableActions()
            };
        }
    }
}