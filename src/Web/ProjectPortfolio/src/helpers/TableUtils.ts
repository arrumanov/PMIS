import React from 'react';
import { SorterResult, ColumnType, ColumnsType } from 'antd/lib/table/interface';
import { CheckOutlined } from '@ant-design/icons';
import { Filter, SORT_ORDER } from 'types';
import { formatDateTime, formatDate, formatNumber } from './Formatter';

export type LocalColumnType<RowType = any> = {
  title?: string;
  dataIndex: keyof RowType & string;
  width: number;
  ellipsis?: boolean;
  render?: ColumnType<RowType>['render'];
  disableSorting?: boolean;
  align?: ColumnType<RowType>['align'];
  hidden?: boolean;
  index?: number;
};

type SortProps<RowType> = {
  sorter?: SorterResult<RowType> | SorterResult<RowType>[];
  isLocal?: boolean;
  disableSorting?: boolean;
};

const colSort = (a: any, b: any): number => {
  if (!a || !b) {
    return 0;
  }
  if (typeof a === 'string' && typeof b === 'string') {
    return a.localeCompare(b);
  }
  if (typeof a === 'number' && typeof b === 'number') {
    return a - b;
  }
  return 0;
};

const columnDef = <RowType>(col: LocalColumnType<RowType>, props: SortProps<RowType>): ColumnType<RowType> => {
  const { title = '', dataIndex, width, ellipsis, render, disableSorting, align, hidden } = col;
  const { isLocal, sorter } = props;
  const sortOrder = sorter
    ? (Array.isArray(sorter) ? sorter : [sorter]).find((s) => s.field === dataIndex)?.order
    : undefined;
  return hidden
    ? { title: '', width: 0 }
    : {
        title,
        dataIndex: dataIndex,
        key: dataIndex,
        ...(width ? { width } : {}),
        ...(ellipsis ? { ellipsis } : {}),
        sorter: !disableSorting ? (isLocal ? (a: any, b: any) => colSort(a[dataIndex], b[dataIndex]) : true) : false,
        ...(sortOrder ? { sortOrder } : {}),
        render,
        align,
      };
};

export const getColumns = <RowType>(
  props: { columns: LocalColumnType<RowType>[] } & SortProps<RowType>,
): ColumnType<RowType>[] => {
  const { columns, ...rest } = props;
  return columns.map((col) => columnDef(col, rest));
};

export function calcColumnsWidth(columns: ColumnsType<any>): number {
  return columns.reduce((prev, { width }) => prev + (width ? parseInt(String(width)) : 0), 0);
}

export function getSort(sorter?: SorterResult<any> | SorterResult<any>[]): Filter['sort'] | undefined {
  if (sorter) {
    return (Array.isArray(sorter) ? sorter : [sorter])
      .filter((s) => s.order)
      .map(({ field, order }) => ({
        field: String(field),
        order: order === 'ascend' ? SORT_ORDER.ASC : SORT_ORDER.DESC,
      }));
  } else {
    return undefined;
  }
}

export function renderDateTimeColumn(value: string) {
  return formatDateTime(value);
}

export function renderDateColumn(value: string) {
  return formatDate(value);
}

export function renderBooleanColumn(value: boolean) {
  return value ? React.createElement(CheckOutlined) : null;
}

export function renderNumberColumn(value: string | number) {
  return formatNumber(value);
}

export function getTableShown(total: number, range: [number, number]) {
  return `${range[0]}-${range[1]} / ${total}`;
}
