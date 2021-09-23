import { WorkflowActionDTO } from './types';

export abstract class AWFService {
  abstract getActionList(contextId: string): Promise<WorkflowActionDTO[]>;
  abstract executeWorkflowAction(actionId: string, contextId: string): Promise<any>;
}
