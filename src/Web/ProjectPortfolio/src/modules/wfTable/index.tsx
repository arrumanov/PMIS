import React, { ReactNode, useEffect } from 'react';
import { CheckOutlined, CloseOutlined, DownloadOutlined } from '@ant-design/icons';
import { observer } from 'mobx-react';

import { Container } from 'components/Container';
import { TableWrapper, TableHader, ConfigButton } from 'components/TableWrapper';
import { ClientDTO } from 'services/clients/modeles';
import { store, actionStore } from './store';
import { RowAction, RowActions } from 'components/RowActions';
import { WorkflowActionDTO } from 'services/workflow';
import { ACTION_TYPES } from './types';
import { Widget } from 'components/Widget';
import { Filter } from 'components/Filter';
import { filterDefinition } from './filter';

const actionIconMap: Record<ACTION_TYPES, ReactNode> = {
  [ACTION_TYPES.CONFIRM]: <CheckOutlined />,
  [ACTION_TYPES.REJECT]: <CloseOutlined />,
  [ACTION_TYPES.DOWNLOAD_PDF]: <DownloadOutlined />,
};

function onActionClick(record: ClientDTO, actionCode: string) {
  const { id } = record;
  switch (actionCode) {
    case ACTION_TYPES.CONFIRM:
      alert(`Confirm ${id}`);
      break;
    case ACTION_TYPES.REJECT:
      alert(`Reject ${id}`);
      break;
    case ACTION_TYPES.DOWNLOAD_PDF:
      alert(`Download ${id}`);
      break;
  }
}

function mapAction({ actionCode, actionName, actionId }: WorkflowActionDTO, record: ClientDTO): RowAction {
  return {
    actionCode,
    actionName,
    icon: actionIconMap[actionCode as ACTION_TYPES],
    onClick: () => onActionClick(record, actionCode),
  };
}

export default observer(() => {
  const { sorter, pagination, actionsRow, columns, size, loading, data, criteria } = store;
  const { isLoading: isActionsLoading, isOpen: isActionsOpen, dynamicActions } = actionStore;

  useEffect(() => {
    store.loadData();
  }, []);

  function renderRowActions(record: ClientDTO) {
    function onVisibleChange(isOpen: boolean) {
      if (isOpen) {
        store.setActionRow(record);
        actionStore.loadActions(record);
      } else {
        actionStore.closeActionList();
      }
    }

    const isCurrent = actionsRow?.id === record.id;

    return (
      <RowActions
        onVisibleChange={onVisibleChange}
        isOpen={isCurrent && isActionsOpen}
        loading={isCurrent && isActionsLoading}
        actions={dynamicActions.map((a) => mapAction(a, record))}
      />
    );
  }

  const configButton = (
    <ConfigButton columns={columns} onChangeColumns={store.setColumns} size={size} onChangeSize={store.setSize} />
  );

  return (
    <Container>
      <Widget>
        <TableHader title="Workflow actions" actions={configButton} />
        <Filter
          key="filter"
          filterDefinition={filterDefinition}
          value={criteria}
          onApply={store.setCriteria}
          renderAs="drawer"
        />
        <TableWrapper<ClientDTO>
          size={size}
          columns={columns}
          onChangeColumns={store.setColumns}
          rowKey="id"
          sorter={sorter}
          pagination={pagination}
          onChange={store.loadData}
          loading={loading}
          dataSource={data}
          renderRowActions={renderRowActions}
        />
      </Widget>
    </Container>
  );
});
