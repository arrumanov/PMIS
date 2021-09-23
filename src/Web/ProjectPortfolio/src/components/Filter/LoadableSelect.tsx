import React, { useState, useEffect, useCallback } from 'react';
import { Select } from 'antd';
import { showError } from 'components/Notification';
import { Dictionary } from 'types';

const { Option } = Select;

type Props = {
  value?: string[];
  onChange: (value?: string[], items?: Dictionary) => void;
  onLoad: () => Promise<Dictionary> | undefined;
  mode?: 'multiple';
};

export function LoadableSelect({ value, mode, onLoad, onChange }: Props) {
  const [source, setSource] = useState<Dictionary>();
  const [loading, setLoading] = useState<boolean>(false);

  const load = useCallback(async () => {
    setLoading(true);
    try {
      const items = await onLoad();
      setSource(items);
    } catch ({ message }) {
      showError({ message });
    }
    setLoading(false);
  }, [onLoad]);

  const isCorrectSource = useCallback(() => {
    if (value?.length && !source?.length) {
      return false;
    }
    return true;
  }, [source, value]);

  useEffect(() => {
    !isCorrectSource() && load();
  }, [isCorrectSource, load]);

  function getValue() {
    return mode === 'multiple' ? value : value?.[0];
  }

  function onDropdownVisibleChange(open: boolean) {
    open && load();
  }

  function onValueChange(newValue: string | string[] = [], opt?: any) {
    onChange(Array.isArray(newValue) ? newValue : [newValue], source);
  }

  return (
    <Select
      allowClear
      loading={loading}
      value={getValue()}
      style={{ width: '100%' }}
      onChange={onValueChange}
      onDropdownVisibleChange={onDropdownVisibleChange}
      mode={mode}
    >
      {source?.map((itm) => (
        <Option value={itm.itemCode} key={itm.itemCode}>
          {itm.name}
        </Option>
      ))}
    </Select>
  );
}
