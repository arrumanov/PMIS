import { SorterResult } from 'antd/lib/table/interface';
import { autowired } from 'first-di';

import { Filter, TableGid } from 'types';
import { AClientsService, ClientDTO } from 'services/clients';
import { userTableColumns } from './columns';
import { ARemoteTableStore } from 'helpers/ARemoteTableStore';

const defaultSorter: SorterResult<ClientDTO> = { field: 'id', order: 'ascend' };

class Store extends ARemoteTableStore<ClientDTO> {
  @autowired() private readonly clientsService!: AClientsService;

  configUUID = TableGid.Minimal;
  defaultSorter = defaultSorter;
  defaultColumns = userTableColumns;

  protected load(filter: Filter) {
    return this.clientsService.list(filter);
  }
}

export const store = new Store('id');
