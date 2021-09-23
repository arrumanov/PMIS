import React, { useEffect } from 'react';
import { Button } from 'antd';
import { FileSearchOutlined } from '@ant-design/icons';

import { Container } from 'components/Container';
import { store } from './store';
import { SimpleTable } from 'components/SimpleTable';
import { filterDefinition } from './filter';
import { ClientDTO } from 'services/clients';

function renderRowActions(record: ClientDTO, index: number) {
  return <Button icon={<FileSearchOutlined />} type="link" />;
}

export default function () {
  useEffect(() => {
    store.loadData();
    store.loadUIConfig();
  }, []);

  return (
    <Container>
      <SimpleTable<ClientDTO>
        store={store}
        title="Clients"
        filterDefinition={filterDefinition}
        renderRowActions={renderRowActions}
      />
    </Container>
  );
}
