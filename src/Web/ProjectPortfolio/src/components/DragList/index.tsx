import React, { ReactNode } from 'react';
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';
import { Container } from './Container';

export type Props<T> = {
  items: T[];
  renderItem: (item: T, index: number) => ReactNode;
  onChange?: (items: T[]) => void;
};

export function DragList<T = any>({ items, renderItem, onChange }: Props<T>) {
  return (
    <DndProvider backend={HTML5Backend}>
      <Container<T> items={items} renderItem={renderItem} onChange={onChange} />
    </DndProvider>
  );
}
