import delay from 'lodash/delay';
import { reflection } from 'first-di';
import { Filter, Page } from 'types';

import { ClientDTO, DetailDTO } from './modeles';
import { AClientsService } from './AClientsService';

const clientMock: ClientDTO = {
  id: '0',
  contextId: '0',
  active: true,
  activeChangeDate: '',
  activeChangeUser: 'none',
  change_date: '',
  change_user: 'none',
  citizenshipCode: '220000',
  citizenshipName: 'BLR',
  contacts: [],
  create_date: '',
  create_user: 'empty',
  divisionCode: '',
  email: 'user@mail.com',
  identityDocuments: [],
  keyInfo: 'fsdfdsfds',
  keyInfoTypeCode: 'code',
  keyInfoTypeName: 'name',
  manager: 'manager',
  name: 'Name',
  resident: true,
  shortName: 'Short Name',
  srcCode: '',
  srcCodeName: '',
  srcId: '',
  typeCode: '',
  typeName: '',
};

const detailMock: DetailDTO = {
  id: '0',
  divisionCode: '54354354',
  email: 'mail@tut.by',
  manager: 'empty',
  name: 'Name',
};

const mockList: ClientDTO[] = new Array(100).fill(clientMock).map((el, i) => ({
  ...el,
  id: i + '',
  contacts: new Array(i % 3).fill('TAG'),
  citizenshipCode: '2' + Math.round(Math.random() * 100000),
}));

const detailList: DetailDTO[] = new Array(100).fill(detailMock).map((el, i) => ({ ...el, id: i + '' }));

@reflection
export class ClientsService implements AClientsService {
  list(filter?: Filter) {
    const { page = 0, size = 10 } = filter || {};
    const data: Page<ClientDTO> = {
      number: 1,
      totalElements: mockList.length,
      content: mockList.slice(page * size, (page + 1) * size),
    };
    //return Promise.resolve(data);
    return new Promise<Page<ClientDTO>>((res) => delay(() => res(data), 1000));
  }

  details(clientId: string) {
    return clientId
      ? new Promise<DetailDTO[]>((res) => delay(() => res(detailList), 1000))
      : Promise.reject('Client id is null');
  }

  create() {}
}
