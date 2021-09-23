import { LocalColumnType } from 'helpers/TableUtils';
import { ClientDTO } from 'services/clients/modeles';

export const userTableColumns: LocalColumnType<ClientDTO>[] = [
  {
    title: 'User ID',
    dataIndex: 'id',
    width: 180,
  },
  {
    title: 'Name',
    dataIndex: 'name',
    width: 250,
  },
  {
    title: 'Short name',
    dataIndex: 'shortName',
    width: 250,
  },
  {
    title: 'E-mail',
    dataIndex: 'email',
    width: 250,
  },
  {
    title: 'Country',
    dataIndex: 'citizenshipName',
    width: 100,
  },
  {
    title: 'ZIP code',
    dataIndex: 'citizenshipCode',
    width: 100,
  },
];
