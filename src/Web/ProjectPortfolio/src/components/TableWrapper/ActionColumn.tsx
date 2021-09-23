import React, { ReactNode, useState, useRef, useEffect, useCallback } from 'react';
import { ColumnType } from 'antd/lib/table';
//import throttle from 'lodash/throttle';

type renderFn<T> = (record: T, index: number) => ReactNode;

function stop(e: React.MouseEvent<HTMLElement, MouseEvent>) {
  e.stopPropagation();
}

function ColRender({ index, record, onActionResize, render }: any) {
  const ref = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const { current } = ref;
    if (current) {
      const newWidth = current.scrollWidth + current.offsetLeft * 2;
      onActionResize(index, newWidth);
      // ResizeObserver is needed when it's necessary to react on size from Context
      // WARN: using ResizeObserver causes performance issues
      // const ro = new ResizeObserver(
      //   throttle(() => {
      //     const newWidth = current.scrollWidth + current.offsetLeft * 2;
      //     onActionResize(index, newWidth);
      //   }, 300)
      // );
      //ro.observe(current);
      return () => {
        // ro.disconnect();
        onActionResize(index, 0);
      };
    }
  }, [index, onActionResize]);

  return (
    <div ref={ref} style={{ display: 'inline-flex', overflow: 'hidden' }} onClick={stop}>
      {render?.(record, index)}
    </div>
  );
}

export function useActionColumn<T>(render?: renderFn<T>) {
  const [actionWidths, setActionWidth] = useState<Record<number, number>>({});

  const onActionResize = useCallback(
    (index: number, colWidth?: number) => {
      if (colWidth) {
        setActionWidth((acts) => ({ ...acts, [index]: colWidth }));
      } else {
        setActionWidth(({ [index]: del, ...restWidth }) => restWidth);
      }
    },
    [setActionWidth],
  );

  const widths = Object.values(actionWidths);

  const width = Math.max(...widths, 0);

  const actionCol: ColumnType<T> | undefined = render
    ? {
        title: '',
        width: width,
        fixed: 'right',
        align: 'left',
        render: (value: any, record: T, index: number) => (
          <ColRender index={index} record={record} render={render} onActionResize={onActionResize} />
        ),
      }
    : undefined;

  return actionCol;
}
