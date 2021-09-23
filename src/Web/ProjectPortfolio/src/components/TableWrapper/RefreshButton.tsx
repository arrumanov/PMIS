import { Button } from 'antd';
import React from 'react';
import { ReloadOutlined } from '@ant-design/icons';

type Props = {
  onClick?: () => void;
};

export function RefreshButton({ onClick }: Props) {
  return <Button icon={<ReloadOutlined />} type="link" onClick={onClick}></Button>;
}
