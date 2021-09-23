import { Tag, Typography } from 'antd';
import { LocalColumnType } from 'helpers/TableUtils';
import React from 'react';
import { ClientDTO } from 'services/clients/modeles';

const { Text } = Typography;

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
    render: (value: any, record: ClientDTO, index: number) => (
      <Text type={index % 2 ? 'success' : 'danger'}>{value}</Text>
    ),
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
  {
    title: 'Contacts',
    dataIndex: 'contacts',
    width: 200,
    render: (value: any) => (value as string[]).map((c, i) => <Tag key={c + i}>{c}</Tag>),
  },
  {
    title: 'Change user',
    dataIndex: 'change_user',
    width: 150,
  },
];
