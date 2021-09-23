export type WorkflowActionDTO = {
  actionId: string;
  actionCode: string;
  actionName: string;
  contextId: string;
  iconType?: string;
};

export type ActionParams<T> = { contextId: string } & T;
