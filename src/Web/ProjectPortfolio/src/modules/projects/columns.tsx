import { Tag, Typography } from 'antd';
import { LocalColumnType } from 'helpers/TableUtils';
import React from 'react';
import { Project } from 'services/project/modeles';

const { Text } = Typography;

export const userTableColumns: LocalColumnType<Project>[] = [
  {
    title: 'Project ID',
    dataIndex: 'id',
    width: 180,
  },
  {
    title: 'Name',
    dataIndex: 'name',
    width: 250,
    render: (value: any, record: Project, index: number) => (
      <Text type={index % 2 ? 'success' : 'danger'}>{value}</Text>
    ),
  },
  {
    title: 'Description',
    dataIndex: 'description',
    width: 250,
  },
//   {
//     title: 'Project departments',
//     dataIndex: 'projectDepartments',
//     width: 200,
//     render: (value: any) => (value as string[]).map((c, i) => <Tag key={c + i}>{c}</Tag>),
//   },
];
