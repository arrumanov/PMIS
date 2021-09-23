import React from 'react';
import { Resizable } from 'react-resizable';

import styles from './ResizableTitle.module.less';

function stop(event: React.MouseEvent<HTMLSpanElement, MouseEvent>) {
  event.stopPropagation();
}

export const ResizableTitle = (props: any) => {
  const { onResize, width, ...restProps } = props;

  if (!width) {
    return <th {...restProps} />;
  }

  return (
    <Resizable
      width={width}
      height={0}
      handle={<span className={styles['react-resizable-handle']} onClick={stop} />}
      onResize={onResize}
      draggableOpts={{ enableUserSelectHack: false }}
    >
      <th {...restProps} />
    </Resizable>
  );
};
