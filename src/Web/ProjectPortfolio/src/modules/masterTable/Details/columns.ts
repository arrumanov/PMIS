import { LocalColumnType } from 'helpers/TableUtils';
import { DetailDTO } from 'services/clients/modeles';

export const userTableColumns: LocalColumnType<DetailDTO>[] = [
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
    title: 'Division Code',
    dataIndex: 'divisionCode',
    width: 400,
  },
  {
    title: 'Mail',
    dataIndex: 'email',
    width: 250,
  },
  {
    title: 'Manager',
    dataIndex: 'manager',
    width: 400,
  },
];
