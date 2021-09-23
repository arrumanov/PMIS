import { action, makeObservable, observable, reaction } from 'mobx';
import { SorterResult } from 'antd/lib/table/interface';
import { autowired } from 'first-di';

import { Filter } from 'types';
import { ClientDTO, DetailDTO } from 'services/clients/modeles';
import { AClientsService } from 'services/clients';
import { ALocalTableStore } from 'helpers/ALocalTableStore';
import { userTableColumns } from './columns';

class Store extends ALocalTableStore<DetailDTO> {
  @autowired() private clientsService!: AClientsService;

  defaultSorter: SorterResult<DetailDTO> = { field: 'id', order: 'ascend' };
  defaultColumns = userTableColumns;

  clientId?: ClientDTO['id'] = undefined;

  constructor(rowKey: keyof DetailDTO) {
    super(rowKey);

    makeObservable(this, {
      clientId: observable,
      setClientId: action,
    });

    reaction(
      () => this.clientId,
      () => this.loadData({ ...this.pagination, current: 1 }),
    );
  }

  setClientId = (clientId?: ClientDTO['id']) => {
    this.clientId = clientId;
  };

  protected load(filter: Filter) {
    return this.clientId ? this.clientsService.details(this.clientId) : Promise.resolve([]);
  }
}

export const store = new Store('id');
