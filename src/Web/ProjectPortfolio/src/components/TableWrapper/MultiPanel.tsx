import React, { ReactNode } from 'react';
import { Button, Space, Typography } from 'antd';
import { ClearOutlined } from '@ant-design/icons';
import classnames from 'classnames';

import { useSize } from 'helpers/useSize';
import styles from './MultiPanel.module.less';
import { useTranslation } from 'react-i18next';
import { StringMap, TOptions } from 'i18next';

const { Text } = Typography;

type Props = {
  count?: number;
  actions?: ReactNode;
  onClean?: () => void;
};

export function MultiPanel({ count = 0, actions, onClean }: Props) {
  const { t } = useTranslation();
  function tt(key: string, options?: string | TOptions<StringMap> | undefined) {
    return t(`components.MultiPanel.${key}`, options);
  }
  const size = useSize();

  function onClick() {
    onClean?.();
  }

  return (
    <div
      className={classnames(styles['panel'], {
        [styles['large']]: size === 'large',
        [styles['small']]: size === 'small',
      })}
    >
      <Text className={styles['text']}>{tt('info', { count })}</Text>
      {count ? <Button type="link" size="small" icon={<ClearOutlined />} onClick={onClick}></Button> : null}
      <Space style={{ marginLeft: 'auto' }}>{actions}</Space>
    </div>
  );
}
