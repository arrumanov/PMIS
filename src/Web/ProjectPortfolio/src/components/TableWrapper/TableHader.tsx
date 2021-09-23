import React, { ReactNode } from 'react';
import { Space, Typography } from 'antd';
import classnames from 'classnames';

import { useSize } from 'helpers/useSize';
import styles from './TableHader.module.less';

const { Title } = Typography;

type Props = {
  title?: string;
  extra?: ReactNode;
  actions?: ReactNode;
};

export function TableHader(props: Props) {
  const size = useSize();
  const { extra, actions, title = '' } = props;

  return (
    <div
      className={classnames(styles['title'], {
        [styles['large']]: size === 'large',
        [styles['small']]: size === 'small',
      })}
    >
      <div className={styles['titleBlock']}>
        <Space>
          <Title level={size === 'small' ? 5 : size === 'large' ? 3 : 4} className={styles['text']}>
            {title}
          </Title>
          {extra}
        </Space>
      </div>
      <Space>{actions}</Space>
    </div>
  );
}
