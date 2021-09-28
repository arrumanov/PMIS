import { Tag, Typography } from 'antd';
import { LocalColumnType } from 'helpers/TableUtils';
import React from 'react';
import { Project } from 'services/project/modeles';

const { Text } = Typography;

export const userTableColumns: LocalColumnType<Project>[] = [
  {
    title: 'ID проекта',
    dataIndex: 'id',
    width: 180,
  },
  {
    title: 'Название',
    dataIndex: 'name',
    width: 250,
    render: (value: any, record: Project, index: number) => (
      <Text type={index % 2 ? 'success' : 'danger'}>{value}</Text>
    ),
  },
  {
    title: 'Описание',
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
