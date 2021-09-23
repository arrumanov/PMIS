import React, { useCallback, ReactNode, CSSProperties } from 'react';
import update from 'immutability-helper';

import { Card } from './Card';

export type Props<T> = {
  items: T[];
  renderItem: (item: T, index: number) => ReactNode;
  onChange?: (items: T[]) => void;
  style?: CSSProperties;
};

export function Container<T>({ items, onChange, renderItem, style }: Props<T>) {
  const moveCard = useCallback(
    (dragIndex: number, hoverIndex: number) => {
      const dragCard = items[dragIndex];
      onChange?.(
        update(items, {
          $splice: [
            [dragIndex, 1],
            [hoverIndex, 0, dragCard],
          ],
        })
      );
    },
    [items]
  );

  const renderCard = (card: T, index: number) => {
    return (
      <Card key={JSON.stringify(card)} index={index} moveCard={moveCard}>
        {renderItem(card, index)}
      </Card>
    );
  };

  return <div style={style}>{items.map((card, i) => renderCard(card, i))}</div>;
}
