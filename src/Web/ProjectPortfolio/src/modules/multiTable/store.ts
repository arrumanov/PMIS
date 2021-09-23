import { SorterResult } from 'antd/lib/table/interface';
import { autowired } from 'first-di';

import { Filter } from 'types';
import { ARemoteTableStore } from 'helpers/ARemoteTableStore';
import { ClientDTO } from 'services/clients/modeles';
import { AClientsService } from 'services/clients';

class Store extends ARemoteTableStore<ClientDTO> {
  @autowired() private clientsService!: AClientsService;

  defaultSorter: SorterResult<ClientDTO> = { field: 'id', order: 'ascend' };

  protected load(filter: Filter) {
    return this.clientsService.list(filter);
  }
}

export const store = new Store('id');
