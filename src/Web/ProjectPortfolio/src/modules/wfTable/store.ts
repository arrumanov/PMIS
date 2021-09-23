import { SorterResult } from 'antd/lib/table/interface';
import { autowired } from 'first-di';
import { action, makeObservable, observable } from 'mobx';

import { Filter } from 'types';
import { ARemoteTableStore } from 'helpers/ARemoteTableStore';
import { AClientsService, ClientDTO } from 'services/clients';
import { ActionStore } from 'helpers/ActionStore';
import { userTableColumns } from './columns';

const defaultSorter: SorterResult<ClientDTO> = { field: 'id', order: 'ascend' };

class Store extends ARemoteTableStore<ClientDTO> {
  @autowired() private readonly clientsService!: AClientsService;

  defaultSorter = defaultSorter;
  defaultColumns = userTableColumns;

  actionsRow?: ClientDTO = undefined;

  constructor(rowKey: keyof ClientDTO) {
    super(rowKey);

    makeObservable(this, {
      actionsRow: observable,
      setActionRow: action,
    });
  }

  setActionRow(actionsRow?: ClientDTO) {
    this.actionsRow = actionsRow;
  }

  protected load(filter: Filter) {
    return this.clientsService.list(filter);
  }
}

export const store = new Store('id');

export const actionStore = new ActionStore();
