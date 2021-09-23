import React, { useEffect } from 'react';
import { Button, Space } from 'antd';
import { observer } from 'mobx-react';

import { Container } from 'components/Container';
import { TableWrapper } from 'components/TableWrapper';
import { DetailDTO } from 'services/clients/modeles';
import { store } from './store';

export { store };

function renderRowActions(record: DetailDTO) {
  return (
    <Space>
      <Button type="link">{`View ${record.id}`}</Button>
    </Space>
  );
}

export const Details = observer(() => {
  const { sorter, pagination, loading, data, columns } = store;

  useEffect(() => {
    store.loadData();
  }, []);

  return (
    <Container>
      <TableWrapper<DetailDTO>
        columns={columns}
        onChangeColumns={store.setColumns}
        rowKey="id"
        sorter={sorter}
        pagination={pagination}
        onChange={store.changePage}
        loading={loading}
        dataSource={data}
        renderRowActions={renderRowActions}
      />
    </Container>
  );
});
