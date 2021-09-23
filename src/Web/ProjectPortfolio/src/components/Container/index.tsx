import React, { PropsWithChildren, CSSProperties } from 'react';
import classnames from 'classnames';
import styles from './styles.module.less';
import { SizeType } from 'antd/es/config-provider/SizeContext';
import { useSize } from 'helpers/useSize';

type Props = {
  margin?: SizeType;
  flex?: CSSProperties['flex'];
};

export function Container(props: PropsWithChildren<Props>) {
  const size = useSize();

  const { children, margin = size, flex } = props;

  const classes = classnames(styles['container'], {
    [styles['margin-sm']]: margin === 'small',
    [styles['margin-md']]: margin === 'middle',
    [styles['margin-lg']]: margin === 'large',
  });

  return (
    <div className={classes} style={{ flex }}>
      {children}
    </div>
  );
}
