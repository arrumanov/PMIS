import React from 'react';
import { Checkbox, List } from 'antd';
import { MenuOutlined } from '@ant-design/icons';
import update from 'immutability-helper';

import { LocalColumnType } from 'helpers/TableUtils';
import { DragList } from 'components/DragList';

type Props = {
  columns: LocalColumnType[];
  onChange?: (columns: LocalColumnType[]) => void;
};

export function ColumnsList({ columns, onChange }: Props) {
  function renderItem(itm: LocalColumnType, idx: number) {
    function onVisibilityChange() {
      onChange?.(update(columns, { [idx]: { $set: { ...columns[idx], hidden: !itm.hidden } } }));
    }

    return (
      <List.Item extra={<Checkbox checked={!itm.hidden} onChange={onVisibilityChange}></Checkbox>}>
        <List.Item.Meta title={itm.title} avatar={<MenuOutlined />} />
      </List.Item>
    );
  }

  function onDrag(items: LocalColumnType[]) {
    onChange?.(items.map((i, index) => ({ ...i, index })));
  }

  return (
    <List size={'small'}>
      <DragList<LocalColumnType> items={columns} renderItem={renderItem} onChange={onDrag} />
    </List>
  );
}
