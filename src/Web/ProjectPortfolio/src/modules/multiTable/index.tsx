import React, { useEffect } from 'react';
import { Button, Space } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { observer } from 'mobx-react';

import { Container } from 'components/Container';
import { TableWrapper, MultiPanel } from 'components/TableWrapper';
import { ClientDTO } from 'services/clients/modeles';

import { userTableColumns } from './columns';
import { store } from './store';

function renderRowActions(record: ClientDTO) {
  return (
    <Space>
      <Button icon={<EditOutlined />} type="link" />
      <Button danger icon={<DeleteOutlined />} type="link"></Button>
    </Space>
  );
}

export default observer(() => {
  const { sorter, pagination, selectedRows, selectedIds, data, loading } = store;

  useEffect(() => {
    store.loadData();
  }, []);

  const count = selectedRows.length;

  const multiActions = count
    ? [
        <Button key="sign" disabled={!count} type="primary">
          {count ? `Sign (${count})` : 'Sign'}
        </Button>,
        <Button key="appr" disabled={!count}>
          Approve
        </Button>,
        <Button key="del" danger icon={<DeleteOutlined />} disabled={!count}>
          Dlete
        </Button>,
      ]
    : [];

  return (
    <Container>
      <MultiPanel count={count} actions={multiActions} onClean={store.setSelectedRows} />
      <TableWrapper<ClientDTO>
        columns={userTableColumns}
        selectMode="multi"
        rowKey="id"
        sorter={sorter}
        pagination={pagination}
        onChange={store.loadData}
        loading={loading}
        dataSource={data}
        onSelectionChange={store.setPageSelectedRows}
        selectedIds={selectedIds}
        renderRowActions={renderRowActions}
      />
    </Container>
  );
});
