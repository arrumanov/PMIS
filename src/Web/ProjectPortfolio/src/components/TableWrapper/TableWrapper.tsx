import React, { HTMLAttributes, ReactNode, ReactText } from 'react';
import { Table } from 'antd';
import { ColumnType, TableProps } from 'antd/lib/table';
import { SorterResult } from 'antd/lib/table/interface';

import { getColumns, LocalColumnType } from 'helpers/TableUtils';
import { useActionColumn } from './ActionColumn';
import { ResizableTitle } from './ResizableTitle';

type Props<T> = Omit<
  TableProps<T>,
  'columns' | 'rowSelection' | 'showSorterTooltip' | 'tableLayout' | 'components' | 'rowKey' | 'onRow'
> & {
  rowKey: keyof T & string;
  columns: LocalColumnType<T>[];
  sorter?: SorterResult<T> | SorterResult<T>[];
  selectedIds?: ReactText[];
  onSelectionChange?: (selectedRows: T[]) => void;
  renderRowActions?: (record: T, index: number) => ReactNode;
  onChangeColumns?: (columns: LocalColumnType<T>[]) => void;
  selectMode?: 'single' | 'multi';
};

export function TableWrapper<T extends Record<string, any> = {}>({
  columns,
  sorter,
  selectedIds = [],
  renderRowActions,
  selectMode,
  onSelectionChange,
  onChangeColumns,
  rowKey,
  dataSource,
  ...restTableProps
}: Props<T>) {
  function onRow(record: T) {
    return {
      onClick: () => {
        if (selectMode === 'single') {
          onSelectionChange?.([record]);
        } else {
          const rid = record[rowKey];
          const newSids =
            selectedIds.indexOf(rid) >= 0 ? selectedIds.filter((sid) => sid !== rid) : [rid, ...selectedIds];
          const newSelectedRows = dataSource?.filter((dsi) => newSids.includes(dsi[rowKey]));
          onSelectionChange?.(newSelectedRows || []);
        }
      },
    };
  }

  const handleResize = (index: number) => (e: any, { size: colSize }: any) => {
    e.stopImmediatePropagation();
    onChangeColumns?.(columns.map((c, i) => (i === index ? { ...c, width: colSize.width, render: c.render } : c)));
  };

  const actionCol: ColumnType<T> | undefined = useActionColumn<T>(renderRowActions);

  const baseColumns: ColumnType<T>[] = getColumns({ columns, sorter });

  const finalColumns = [
    ...baseColumns.map<ColumnType<T>>((col, idx) => ({
      ...col,
      onHeaderCell:
        onChangeColumns &&
        function (c: any) {
          return {
            width: c.width,
            onResize: handleResize(idx),
          } as HTMLAttributes<HTMLElement>;
        },
    })),
    { title: '' }, //fix for layout
    ...(actionCol ? [actionCol] : []),
  ];

  const components = onChangeColumns && {
    header: {
      cell: ResizableTitle,
    },
  };

  return (
    <Table
      components={components}
      tableLayout="fixed"
      rowKey={rowKey}
      dataSource={dataSource}
      rowSelection={
        selectMode && {
          type: selectMode === 'single' ? 'radio' : 'checkbox',
          selectedRowKeys: selectedIds,
          onChange: (selectedRowKeys: any, selectedRows: T[]) => onSelectionChange?.(selectedRows),
        }
      }
      columns={finalColumns}
      showSorterTooltip={false}
      onRow={onRow}
      {...restTableProps}
    />
  );
}
