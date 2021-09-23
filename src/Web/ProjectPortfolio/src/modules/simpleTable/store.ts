import { SorterResult } from 'antd/lib/table/interface';
import { autowired } from 'first-di';
import { action, makeObservable, observable } from 'mobx';

import { Filter } from 'types';
import { ARemoteTableStore } from 'helpers/ARemoteTableStore';
import { AClientsService, ClientDTO } from 'services/clients';
import { CLIENT_TYPES } from './types';
import { userTableColumns } from './columns';
import { AWizzardService } from 'services/wizzard';
import { TableGid } from 'types';

const defaultSorter: SorterResult<ClientDTO> = { field: 'id', order: 'ascend' };

class Store extends ARemoteTableStore<ClientDTO> {
  @autowired() private readonly clientsService!: AClientsService;
  @autowired() private readonly wizzService!: AWizzardService;

  configUUID = TableGid.Simple;
  defaultSorter = defaultSorter;
  defaultColumns = userTableColumns;

  activeTab: CLIENT_TYPES = CLIENT_TYPES.ALL;

  constructor(rowKey: keyof ClientDTO) {
    super(rowKey);

    makeObservable(this, {
      activeTab: observable,
      setActiveTab: action,
    });
  }

  setActiveTab(activeTab: CLIENT_TYPES) {
    this.activeTab = activeTab;
    this.setCriteria();
  }

  runWizz(data: string) {
    return this.wizzService.getForm(data);
  }

  protected load(filter: Filter) {
    return this.clientsService.list(filter);
  }
}

export const store = new Store('id');
