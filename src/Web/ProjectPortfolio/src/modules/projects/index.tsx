import React, { useEffect } from 'react';
import { Spin, Button, Dropdown, Menu, Radio, Space } from 'antd';
import { EllipsisOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { observer } from 'mobx-react';
import { RadioChangeEvent } from 'antd/lib/radio';

import {gql, useMutation, useQuery} from '@apollo/client';

import {GET_PROJECTS} from 'graphql/queries/GetProjects';
import {GetProjects_projects, GetProjects} from 'graphql/queries/__generated__/GetProjects';

import { Container } from 'components/Container';
// import { Project, Projects } from 'services/project/modeles';
import { store } from './store';
import { RowActions } from 'components/RowActions';
import { PROJECT_TYPES } from './types';
import { SimpleTable } from 'components/SimpleTable';
import { filterDefinition } from './filter';
// import { FWButton } from './fwButton';

function renderRowActions(record: GetProjects_projects, index: number) {
  return (
    <Space>
      {/* <FWButton /> */}
      <RowActions
        actions={[
          {
            actionCode: 'edit',
            actionName: 'Edit',
            onClick: () => alert(`Edit ${record.id}`),
            icon: <EditOutlined />,
          },
          {
            actionCode: 'delete',
            actionName: 'Delete',
            onClick: () => alert(`Delete ${record.id}`),
            icon: <DeleteOutlined />,
            disabled: index % 2 === 0,
          },
        ]}
      />
    </Space>
  );
}

function onTabChange(e: RadioChangeEvent) {
  store.setActiveTab(e.target.value);
}

export default observer(() => {
  const{data, loading, error} = useQuery<GetProjects>(GET_PROJECTS);
  if(loading) return <Spin size="large" className="spinner" style={{margin: 'auto'}}/>;
  if(error) return <p>ERROR</p>;
  if(!data) return <p>Not found</p>;

  store.setData(data.projects);
  store.setDataGql(data.projects);

  const { activeTab } = store;

  // useEffect(() => {
  //   store.loadData();
  //   store.loadUIConfig();
  // }, []);
  store.loadData();

  const headerExtra = [
    <Radio.Group key="group" buttonStyle="solid" value={activeTab} onChange={onTabChange}>
      <Radio.Button value={PROJECT_TYPES.ALL}>All</Radio.Button>
      <Radio.Button value={PROJECT_TYPES.NAT}>Natural</Radio.Button>
      <Radio.Button value={PROJECT_TYPES.LEG}>Legal</Radio.Button>
    </Radio.Group>,
  ];

  const headerActions = [
    <Button key="1" type="primary">
      Create
    </Button>,
    <Dropdown
      overlay={
        <Menu>
          <Menu.Item>PDF</Menu.Item>
          <Menu.Item>DOCX</Menu.Item>
          <Menu.Item>XLSX</Menu.Item>
        </Menu>
      }
    >
      <Button key="0">Export</Button>
    </Dropdown>,
    <Dropdown
      overlay={
        <Menu>
          <Menu.Item>Other</Menu.Item>
          <Menu.Item>Actions</Menu.Item>
          <Menu.Item>List</Menu.Item>
        </Menu>
      }
    >
      <Button key="2" icon={<EllipsisOutlined />} type="link" title="More"></Button>
    </Dropdown>,
  ];

  return (
    <Container>
      <SimpleTable
        store={store}
        title="Проекты"
        filterDefinition={filterDefinition}
        actions={headerActions}
        extra={headerExtra}
        renderRowActions={renderRowActions}
      />
    </Container>
  );
});
