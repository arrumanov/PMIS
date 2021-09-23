using System;
using System.Collections.Generic;
using Camunda.Api.Client.UserTask;
using HotChocolate;
using HotChocolate.Types;
using Workflow.Api.Bpmn;

namespace Workflow.Api.Graph.Project.Types
{
    public class TaskPayload
    {
        public string TaskId { get; set; }

        public string TaskType { get; set; }

        public string TaskName { get; set; }

        public string Assignee { get; set; }
        
        public string ProjectId { get; set; }

        public string RequestedSuperpower { get; set; }

        public string ProjectStatus { get; set; }

        public List<TaskActions> Actions { get; set; }

        public static TaskPayload FromEntity(UserTaskInfo task, Domain.Project project)
        {
            return new TaskPayload
            {
                TaskId = task.Id,
                TaskType = task.TaskDefinitionKey,
                TaskName = task.Name,
                Assignee = task.Assignee,
                ProjectId = project?.Id.ToString(),
                ProjectStatus = project?.Status.ToString(),
                Actions = task.AvailableActions()
            };
        }
    }
}