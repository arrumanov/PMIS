import { reaction, makeAutoObservable } from 'mobx';
import { autowired } from 'first-di';

import { PromiseObserver, fromPromise } from 'helpers/FromPromise';
import { AWFService, WorkflowActionDTO, ActionParams } from 'services/workflow';

export class ActionStore<T = {}> {
  @autowired() private readonly wfService!: AWFService;

  isOpen: boolean = false;
  actionsPromise?: PromiseObserver<WorkflowActionDTO[]> = undefined;
  executeWFActionPromise?: PromiseObserver<unknown> = undefined;
  actionParams?: ActionParams<T> = undefined;

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.actionsPromise?.fulfilled,
      (fulfilled) => fulfilled && this.setIsOpen(true),
    );
  }

  private load(contextId: string) {
    this.actionsPromise = fromPromise(this.wfService.getActionList(contextId), { oldData: this.actionsPromise?.value });
  }

  private setIsOpen(isOpen: boolean) {
    this.isOpen = isOpen;
  }

  closeActionList = () => {
    this.setIsOpen(false);
  };

  loadActions = (params: ActionParams<T>) => {
    if (JSON.stringify(this.actionParams) === JSON.stringify(params)) {
      this.setIsOpen(true);
    } else {
      this.actionParams = params;
      this.load(this.actionParams.contextId);
    }
    return this.actionsPromise;
  };

  get isLoading() {
    return !!this.actionsPromise?.pending;
  }

  get dynamicActions() {
    return this.actionsPromise?.value || [];
  }

  runDynamicAction = (actionId: string, contextId: string) => {
    this.executeWFActionPromise = fromPromise(this.wfService.executeWorkflowAction(actionId, contextId));
  };

  get isExecuteActionLoading() {
    return !!this.executeWFActionPromise?.pending;
  }
}
