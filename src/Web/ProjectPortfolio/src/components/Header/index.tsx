import React from 'react';
import { Avatar, Badge, Button, Dropdown, Menu, Space } from 'antd';
import {
  UserOutlined,
  BellOutlined,
  AppstoreOutlined,
  DownOutlined,
  SettingOutlined,
  LogoutOutlined,
} from '@ant-design/icons';

import classnames from 'classnames';
import { useSize } from 'helpers/useSize';

import styles from './Header.module.less';
import { useTranslation } from 'react-i18next';

type Props = {
  onSignout?: () => void;
};

export const AppHeader: React.FC<Props> = ({ onSignout }) => {
  const size = useSize();
  const { t } = useTranslation();
  function tt(key: string) {
    return t(`components.Header.${key}`);
  }

  const menu = (
    <Menu>
      <Menu.Item icon={<UserOutlined />}>{tt('profile')}</Menu.Item>
      <Menu.Item icon={<SettingOutlined />}>{tt('settings')}</Menu.Item>
      <Menu.Item danger icon={<LogoutOutlined />} onClick={onSignout}>
        {tt('out')}
      </Menu.Item>
    </Menu>
  );

  return (
    <div
      className={classnames(styles.header, {
        [styles.large]: size === 'large',
        [styles.small]: size === 'small',
      })}
    >
      <div className={styles.path}>
        <Space>
          <Button type="link" icon={<AppstoreOutlined />} />
          <span className={styles.title}>{tt('title')}</span>
        </Space>
      </div>
      <div className={styles.actions}>
        <Space>
          <Badge count={5}>
            <Button type="link" icon={<BellOutlined />} />
          </Badge>

          <Dropdown overlay={menu}>
            <Space style={{ cursor: 'pointer' }}>
              <Avatar icon={<UserOutlined />} style={{ marginLeft: size === 'large' ? '8px' : '4px' }} />
              <span>Admin Admin</span>
              <DownOutlined />
            </Space>
          </Dropdown>
        </Space>
      </div>
    </div>
  );
};
