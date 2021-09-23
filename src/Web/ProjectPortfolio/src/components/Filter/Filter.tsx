import React, { useState, useEffect } from 'react';
import { Row, Col, Typography, Modal, Button, Collapse, Tag, Space, Badge, Drawer } from 'antd';
import { ControlOutlined, ClearOutlined, FilterOutlined } from '@ant-design/icons';

import { FilterDefinition, Props, Criteria, InnerCriteria } from './types';
import { renderFilterField } from './Fields';
import { isCreteriaWithoutGroup } from 'types';
import { useTranslation } from 'react-i18next';

const { Text } = Typography;
const { Panel } = Collapse;

function convertValue(criteria: Criteria): InnerCriteria {
  return criteria?.criteriaItems?.filter(isCreteriaWithoutGroup);
}

function renderChip(definition: FilterDefinition, criteria: InnerCriteria) {
  const { label, field, type } = definition;
  const crit = criteria?.filter((cr) => cr.field === field);
  if (crit?.length) {
    switch (type) {
      case 'dateRange': {
        const from = crit?.find((cr) => cr.operator === 'greaterEqual')?.label || '';
        const to = crit?.find((cr) => cr.operator === 'lessEqual')?.label || '';
        return { label, field, value: `${from} - ${to}` };
      }
      case 'select': {
        return { label, field, value: crit?.[0]?.listLabel?.join(', ') || '' };
      }
      default: {
        return { label, field, value: crit?.[0]?.label || '' };
      }
    }
  }
  return { label, field, value: '' };
}

export function Filter({ filterDefinition, onApply, value, loadSelect, isButton, renderAs = 'modal' }: Props) {
  const { t } = useTranslation();
  function tt(key: string) {
    return t(`components.Filter.${key}`);
  }

  const cvalue = convertValue(value);

  const [data, setData] = useState<InnerCriteria>(cvalue);
  const [isOpen, setOpen] = useState<boolean>(false);
  const [panelKey, setPanelKey] = useState<string | string[]>([]);

  useEffect(() => {
    !value?.criteriaItems?.length && setPanelKey([]);
  }, [value]);

  function clearData() {
    setData([]);
  }

  function onFilterApply() {
    setOpen(false);
    onApply?.({ criteriaItems: data || [] });
  }

  function onReset(event: React.MouseEvent<HTMLSpanElement, MouseEvent>) {
    event.stopPropagation();
    onApply?.({ criteriaItems: [] });
  }

  function onShow(event: React.MouseEvent<HTMLSpanElement, MouseEvent>) {
    event.stopPropagation();
    setData(cvalue);
    setOpen(true);
  }

  function onClose() {
    setOpen(false);
  }

  function onFieldChange(field: string, criteria: InnerCriteria = []) {
    const filteredData = data?.filter((cr) => cr.field !== field) || [];
    setData([...filteredData, ...criteria]);
  }

  function onRemoveChips(name?: string) {
    onApply?.({ criteriaItems: cvalue ? cvalue.filter((v) => v.field !== name) : [] });
  }

  function renderPanel() {
    const chips = filterDefinition?.map((fd) => renderChip(fd, cvalue)).filter((el) => el.value);

    return (
      <Collapse expandIconPosition="right" bordered={false} onChange={setPanelKey} activeKey={panelKey}>
        <Panel
          header={
            <Text strong style={{ textTransform: 'uppercase' }}>
              {tt('titlte')}
            </Text>
          }
          key={1}
          extra={
            <Space size="middle">
              {chips.length ? <ClearOutlined onClick={onReset} title={tt('reset')} /> : null}
              <ControlOutlined onClick={onShow} title={tt('configure')} />
            </Space>
          }
        >
          {chips.length
            ? chips.map((c) => <Tag closable onClose={() => onRemoveChips(c.field)}>{`${c.label}: ${c.value}`}</Tag>)
            : null}
        </Panel>
      </Collapse>
    );
  }

  function renderButton() {
    return (
      <Badge dot={!!cvalue?.length}>
        <Button type="link" icon={<FilterOutlined />} onClick={onShow}></Button>
      </Badge>
    );
  }

  const footer = [
    <Button key="reset" onClick={clearData}>
      {tt('reset')}
    </Button>,
    <Button key="submit" type="primary" onClick={onFilterApply}>
      {tt('apply')}
    </Button>,
  ];

  function renderModal() {
    return (
      <Modal visible={isOpen} title={tt('titlte')} footer={footer} onCancel={onClose} width={600}>
        <div>
          {filterDefinition.map((fd) => (
            <Row gutter={[8, 8]} key={fd.field}>
              <Col span={10}>
                <Text type="secondary">{fd.label}</Text>
              </Col>
              <Col span={14}>
                {renderFilterField(
                  fd,
                  data?.filter((cr) => cr.field === fd.field),
                  onFieldChange,
                  loadSelect,
                )}
              </Col>
            </Row>
          ))}
        </div>
      </Modal>
    );
  }

  function renderDrawer() {
    return (
      <Drawer
        width={300}
        visible={isOpen}
        onClose={onClose}
        title={tt('titlte')}
        footer={<Space key="foot">{footer}</Space>}
        closable={false}
      >
        <div>
          {filterDefinition.map((fd) => (
            <div key={fd.field}>
              <Text type="secondary">{fd.label}</Text>
              {renderFilterField(
                fd,
                data?.filter((cr) => cr.field === fd.field),
                onFieldChange,
                loadSelect,
              )}
            </div>
          ))}
        </div>
      </Drawer>
    );
  }

  return (
    <>
      {isButton ? renderButton() : renderPanel()}
      {renderAs === 'drawer' ? renderDrawer() : renderModal()}
    </>
  );
}
