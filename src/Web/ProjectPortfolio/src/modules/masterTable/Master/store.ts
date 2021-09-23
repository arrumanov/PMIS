import { reaction } from 'mobx';
import { SorterResult } from 'antd/lib/table/interface';
import { autowired } from 'first-di';

import { Filter } from 'types';
import { ARemoteTableStore } from 'helpers/ARemoteTableStore';
import { ClientDTO } from 'services/clients/modeles';
import { AClientsService } from 'services/clients';
import { userTableColumns } from './columns';

class Store extends ARemoteTableStore<ClientDTO> {
  @autowired() private clientsService!: AClientsService;

  defaultSorter: SorterResult<ClientDTO> = { field: 'id', order: 'ascend' };
  defaultColumns = userTableColumns;

  private rowSelectionReaction = reaction(
    () => this.dataPromise?.fulfilled,
    (fulfilled) => {
      if (fulfilled) {
        const users = this.dataPromise?.value?.content;
        if (Array.isArray(users) && users.length > 0) {
          const user = users.find(({ id }) => id === this.selectedId);
          if (!user) {
            this.setSelectedRows([users[0]]);
          }
        } else {
          this.setSelectedRows();
        }
      }
    },
  );

  get selectedId() {
    return this.selectedRows[0]?.id;
  }

  protected load(filter: Filter) {
    return this.clientsService.list(filter);
  }
}

export const store = new Store('id');
