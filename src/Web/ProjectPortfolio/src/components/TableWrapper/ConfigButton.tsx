import { Button, Divider, Drawer, Select } from 'antd';
import React, { useState } from 'react';
import { SettingOutlined } from '@ant-design/icons';

import { ColumnsList } from './ColumnsList';
import { LocalColumnType } from 'helpers/TableUtils';
import { SizeType } from 'helpers/useSize';
import { useTranslation } from 'react-i18next';

type Props = {
  size?: SizeType;
  columns?: LocalColumnType[];
  onChangeColumns?: (columns: LocalColumnType[]) => void;
  onChangeSize?: (size: SizeType) => void;
};

function ConfigButton({ columns = [], size, onChangeColumns, onChangeSize }: Props) {
  const { t } = useTranslation();
  function tt(key: string) {
    return t(`components.${key}`);
  }
  const [open, setOpen] = useState<boolean>(false);

  function onOpen() {
    setOpen(true);
  }

  function onClose() {
    setOpen(false);
  }

  return (
    <>
      <Button icon={ConfigButton.icon} type="link" onClick={onOpen} />
      <Drawer visible={open} title={tt('TableConfig.title')} onClose={onClose}>
        <Select value={size} onSelect={onChangeSize} style={{ width: '100%' }}>
          <Select.Option value="small">{tt('Size.small')}</Select.Option>
          <Select.Option value="middle">{tt('Size.middle')}</Select.Option>
          <Select.Option value="large">{tt('Size.large')}</Select.Option>
        </Select>
        <Divider />
        <ColumnsList columns={columns} onChange={onChangeColumns} />
      </Drawer>
    </>
  );
}

ConfigButton.icon = <SettingOutlined />;

export { ConfigButton };
