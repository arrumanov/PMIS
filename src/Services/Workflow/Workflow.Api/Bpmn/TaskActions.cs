using System.Collections.Generic;
using Camunda.Api.Client.UserTask;

namespace Workflow.Api.Bpmn
{
    public enum TaskActions
    {
        Prepare,
        Close
    }

    public static class UserTaskInfoExtension
    {
        public static List<TaskActions> AvailableActions(this UserTaskInfo userTask)
        {
            return userTask.TaskDefinitionKey switch
            {
                "Task_PrepareProject" => new List<TaskActions> { TaskActions.Prepare },
                "Task_CloseProject" => new List<TaskActions> { TaskActions.Close },
                _ => new List<TaskActions> { }
            };
        }
    }
}