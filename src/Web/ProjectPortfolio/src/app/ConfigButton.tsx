import { SettingOutlined } from '@ant-design/icons';
import { Button, Drawer, Select, Typography } from 'antd';
import { SizeType } from 'antd/es/config-provider/SizeContext';
import React, { useState } from 'react';

const { Text } = Typography;

type Props = {
  size: SizeType;
  onChangeSize?(size: SizeType): void;
};

export function ConfigButton({ size, onChangeSize }: Props) {
  const [open, setOpen] = useState(false);

  function onClose() {
    setOpen(false);
  }

  function onOpen() {
    setOpen(true);
  }

  return (
    <>
      <Button
        shape="circle"
        type="primary"
        icon={<SettingOutlined />}
        style={{ position: 'fixed', bottom: '80px', right: '40px', boxShadow: '0 0 5px 5px #00000025' }}
        onClick={onOpen}
      />
      <Drawer title="Settings" placement="right" closable={false} onClose={onClose} visible={open}>
        <Text>UI size</Text>
        <Select value={size} onSelect={onChangeSize} style={{ width: '100%' }}>
          <Select.Option value="small">Small</Select.Option>
          <Select.Option value="middle">Middle</Select.Option>
          <Select.Option value="large">Large</Select.Option>
        </Select>
      </Drawer>
    </>
  );
}
