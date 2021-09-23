import React, { useEffect } from 'react';
import { observer } from 'mobx-react';

import { TableWrapper, TableHader, ConfigButton, RefreshButton } from 'components/TableWrapper';
import { Filter, FilterDefinition } from 'components/Filter';
import { Widget } from 'components/Widget';
import { autorun } from 'mobx';
import { notification } from 'antd';
import { ARemoteTableStore } from 'helpers/ARemoteTableStore';

type Props<T> = {
  store: ARemoteTableStore<T>;
  title?: string;
  filterDefinition?: FilterDefinition[];
  actions?: React.ReactNode;
  extra?: React.ReactNode;
  renderRowActions?: (record: T, index: number) => React.ReactNode;
};

export const SimpleTable = observer(function <T>({
  store,
  title,
  actions,
  extra,
  filterDefinition,
  renderRowActions,
}: Props<T>) {
  const { sorter, pagination, criteria, columns, size, rowKey, data, loading } = store;

  useEffect(() => {
    return autorun(() => store.error && notification.error({ message: store.error }));
  }, [store]);

  const filterButton = filterDefinition ? (
    <Filter
      key="filter"
      isButton
      filterDefinition={filterDefinition}
      value={criteria}
      onApply={store.setCriteria}
      renderAs="drawer"
    />
  ) : null;

  const headerExtra = [<RefreshButton key="r" onClick={store.loadData} />, extra];

  const configButton = (
    <ConfigButton
      key="config"
      columns={columns}
      onChangeColumns={store.setColumns}
      size={size}
      onChangeSize={store.setSize}
    />
  );

  const headerActions = [actions, filterButton, configButton];

  return (
    <Widget>
      <TableHader title={title} actions={headerActions} extra={headerExtra} />
      <TableWrapper<T>
        size={size}
        columns={columns}
        rowKey={rowKey}
        sorter={sorter}
        pagination={pagination}
        onChange={store.loadData}
        loading={loading}
        dataSource={data}
        renderRowActions={renderRowActions}
        onChangeColumns={store.setColumns}
      />
    </Widget>
  );
});
