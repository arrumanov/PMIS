import { reflection } from 'first-di';
import delay from 'lodash/delay';
import { AWFService } from './AWFService';
import { WorkflowActionDTO } from './types';

const mockData: WorkflowActionDTO[] = [
  {
    actionCode: 'actionConfirm',
    actionId: 'actionConfirm',
    actionName: 'Confirm',
    contextId: '',
  },
  {
    actionCode: 'actionReject',
    actionId: 'actionReject',
    actionName: 'Reject',
    contextId: '',
  },
  {
    actionCode: 'actionDownloadPdf',
    actionId: 'actionDownloadPdf',
    actionName: 'Download PDF',
    contextId: '',
  },
];

@reflection
export class MockWFService extends AWFService {
  getActionList(contextId: string): Promise<WorkflowActionDTO[]> {
    const data = mockData.map((md) => ({ ...md, contextId }));
    return new Promise<WorkflowActionDTO[]>((res) => delay(() => res(data), 1000));
  }

  executeWorkflowAction(actionId: string, contextId: string): Promise<any> {
    throw new Error('Method not implemented.');
  }
}
