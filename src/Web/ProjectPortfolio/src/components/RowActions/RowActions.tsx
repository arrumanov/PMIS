import React, { useState } from 'react';
import { Dropdown, Menu, Button, Empty } from 'antd';
import { MenuProps } from 'antd/lib/menu';
import { MoreOutlined } from '@ant-design/icons';

import { RowAction } from './types';

const { Item } = Menu;

type MenuClickEventHandler = MenuProps['onClick'];

export type Props = {
  isOpen?: boolean;
  actions?: RowAction[];
  loading?: boolean;
  onVisibleChange?: (isOpen: boolean) => void;
};

export const RowActions = ({ actions = [], loading, onVisibleChange: toggleOpen, isOpen: isDropdownOpen }: Props) => {
  const [isOpen, setIsOpen] = useState(false);

  const isVisible = isDropdownOpen ?? isOpen;

  function onVisibleChange(visible: boolean) {
    toggleOpen ? toggleOpen(visible) : setIsOpen(visible);
  }

  const onMenuClick: MenuClickEventHandler = function ({ key }) {
    const action = actions.find(({ actionCode }) => actionCode === key);
    onVisibleChange(false);
    action?.onClick();
  };

  function renderOverlay() {
    if (!actions.length) {
      return (
        <Menu>
          <Item>
            <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} />
          </Item>
        </Menu>
      );
    }
    return (
      <Menu onClick={onMenuClick}>
        {actions.map(({ actionCode, actionName, icon, disabled }) => {
          return (
            <Item key={actionCode} icon={icon} disabled={disabled}>
              <span>{actionName}</span>
            </Item>
          );
        })}
      </Menu>
    );
  }

  return (
    <Dropdown
      visible={isVisible}
      onVisibleChange={onVisibleChange}
      overlay={renderOverlay}
      placement="bottomLeft"
      trigger={['click']}
    >
      <Button type="link" icon={<MoreOutlined />} loading={loading} />
    </Dropdown>
  );
};
