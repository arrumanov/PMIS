import { reflection } from 'first-di';
import { AWFService } from './AWFService';
import { WorkflowActionDTO } from './types';

@reflection
export class WFService extends AWFService {
  getActionList(contextId: string): Promise<WorkflowActionDTO[]> {
    throw new Error('Method not implemented.');
  }
  executeWorkflowAction(actionId: string, contextId: string): Promise<any> {
    throw new Error('Method not implemented.');
  }
}
