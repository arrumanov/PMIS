import React, { PropsWithChildren } from 'react';
import styles from './styles.module.less';

export function Widget(props: PropsWithChildren<{}>) {
  const { children } = props;

  return <div className={styles['container']}>{children}</div>;
}