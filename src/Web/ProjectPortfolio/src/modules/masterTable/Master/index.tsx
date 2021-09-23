import React, { useEffect } from 'react';
import { Button, Space } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { observer } from 'mobx-react';

import { Container } from 'components/Container';
import { TableWrapper } from 'components/TableWrapper';
import { ClientDTO } from 'services/clients/modeles';
import { store } from './store';

export { store };

function renderRowActions(record: ClientDTO) {
  return (
    <Space>
      <Button icon={<EditOutlined />} type="link" onClick={() => alert(JSON.stringify(record))} />
      <Button danger icon={<DeleteOutlined />} type="primary">
        {`Delete ${parseInt(record.id) - 5}`}
      </Button>
    </Space>
  );
}

export const Master = observer(() => {
  const { sorter, pagination, loading, data, columns } = store;

  useEffect(() => {
    store.loadData();
  }, []);

  return (
    <Container>
      <TableWrapper<ClientDTO>
        columns={columns}
        onChangeColumns={store.setColumns}
        selectMode="single"
        rowKey="id"
        sorter={sorter}
        pagination={pagination}
        onChange={store.loadData}
        loading={loading}
        dataSource={data}
        onSelectionChange={store.setSelectedRows}
        selectedIds={store.selectedIds}
        renderRowActions={renderRowActions}
      />
    </Container>
  );
});
