import * as Icons from '@ant-design/icons';
import { MenuFoldOutlined, MenuUnfoldOutlined } from '@ant-design/icons';
import { render } from '@st/fpi';
import { Layout, Menu } from 'antd';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Link, useLocation } from 'react-router-dom';
import { IconCodeType } from 'types';
import classes from './Menu.module.less';

const { Sider } = Layout;
const { SubMenu } = Menu;

export type MenuItem = {
  code: string;
  name: string;
  icon?: IconCodeType;
  route?: string;
  children?: MenuItem[];
};

type Props = {
  items: MenuItem[];
};

function findSelected(
  items: MenuItem[],
  path: string,
  parents: string[] = [],
): { id: string; path: string[] } | undefined {
  return items
    .map((item) => {
      if (item.route === path) {
        return { id: item.code, path: parents };
      } else {
        return item.children ? findSelected(item.children, path, [...parents, item.code]) : undefined;
      }
    })
    .filter((itm) => !!itm)[0];
}

export function MenuComponent({ items }: Props) {
  const { t } = useTranslation();
  function tt(key: string) {
    return t(`components.Menu.${key}`);
  }

  const currentUrl = useLocation().pathname.replace(/[/]/g, '');
  const { id: selectedKey, path: selectedPath } = findSelected(items, currentUrl) || {};

  const [collapsed, setCollapsed] = useState(false);

  function toggleCollapse() {
    setCollapsed(!collapsed);
  }

  function renderMenuItems(mi: MenuItem[]) {
    return mi.map((item) => {
      const Icon = item.icon && Icons[item.icon];
      if (item.children) {
        return (
          <SubMenu key={item.code} icon={Icon ? <Icon /> : null} title={item.name}>
            {renderMenuItems(item.children)}
          </SubMenu>
        );
      } else {
        return (
          <Menu.Item key={item.code} icon={Icon ? <Icon /> : null} title={item.name}>
            <Link to={item.route || '/'}>{item.name}</Link>
          </Menu.Item>
        );
      }
    });
  }

  return items.length ? (
    <Sider collapsed={collapsed}>
      <div className={classes.container}>
        <Menu
          className={classes.menu}
          selectedKeys={selectedKey ? [selectedKey] : undefined}
          defaultOpenKeys={collapsed ? undefined : selectedPath}
          mode="inline"
          theme="dark"
        >
          {renderMenuItems(items)}
        </Menu>
        <Menu className={classes.collapse} mode="inline" theme="dark" selectable={false}>
          <Menu.Item icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />} onClick={toggleCollapse}>
            {collapsed ? tt('expand') : tt('collapse')}
          </Menu.Item>
        </Menu>
      </div>
    </Sider>
  ) : null;
}
