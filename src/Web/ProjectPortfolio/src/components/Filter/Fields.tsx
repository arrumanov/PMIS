import React from 'react';
import { DatePicker, Input, InputNumber, Select } from 'antd';
import moment, { Moment } from 'moment';
import { Dictionary } from 'types';
import {
  StringFilterDefinition,
  DateFilterDefinition,
  DateRangeFilterDefinition,
  FilterDefinition,
  NumberFilterDefinition,
  SelectFilterDefinition,
  InnerCriteria,
  BooleanFilterDefinition,
} from './types';
import { LoadableSelect } from './LoadableSelect';

const { RangePicker } = DatePicker;

function renderBooleanField(
  definition: BooleanFilterDefinition,
  criteria?: InnerCriteria,
  onFieldChange?: (name: string, value: InnerCriteria) => void,
) {
  const { field, op = 'eq' } = definition;

  function onChange(value?: any) {
    onFieldChange?.(
      field,
      typeof value === 'number' ? [{ field, value: !!value, operator: op, label: String(!!value) }] : [],
    );
  }

  function getValue() {
    const value = criteria?.[0]?.value;
    return value === true ? 1 : value === false ? 0 : undefined;
  }

  return (
    <Select style={{ width: '100%' }} allowClear value={getValue()} onChange={onChange}>
      <Select.Option value={1}>Да</Select.Option>
      <Select.Option value={0}>Нет</Select.Option>
    </Select>
  );
}

function renderInputField(
  definition: StringFilterDefinition,
  criteria?: InnerCriteria,
  onFieldChange?: (name: string, value: InnerCriteria) => void,
) {
  const { field, op = 'eq' } = definition;

  function onInputChange(event: React.ChangeEvent<HTMLInputElement>) {
    const { value } = event.target;
    onFieldChange?.(field, [{ field, value, operator: op, label: value }]);
  }

  function getInputValue() {
    const value = criteria?.[0]?.value;
    return typeof value === 'string' ? value : undefined;
  }

  return <Input value={getInputValue()} style={{ width: '100%' }} onChange={onInputChange} />;
}

function renderNumberField(
  definition: NumberFilterDefinition,
  criteria?: InnerCriteria,
  onFieldChange?: (name: string, value: InnerCriteria) => void,
) {
  const { field, op = 'eq' } = definition;

  function onNumberChange(value?: string | number) {
    if (typeof value === 'number' || value === '') {
      const label = String(value ?? '');
      onFieldChange?.(field, [{ field, value, operator: op, label }]);
    }
  }

  function getNumberValue() {
    const value = criteria?.[0]?.value;
    return typeof value === 'number' ? value : undefined;
  }

  return (
    <InputNumber value={getNumberValue()} style={{ width: '100%' }} onChange={onNumberChange} step={definition.step} />
  );
}

function renderSelectField(
  definition: SelectFilterDefinition,
  criteria?: InnerCriteria,
  onFieldChange?: (name: string, value: InnerCriteria) => void,
  onLoadSelect?: (name: string) => Promise<Dictionary> | undefined,
) {
  const { field, mode } = definition;

  function onSelectChange(listValue?: string[], items?: Dictionary) {
    onFieldChange?.(
      field,
      listValue?.length
        ? [
            {
              field,
              listValue,
              operator: 'in',
              listLabel: listValue.map((v) => items?.find((it) => it.itemCode === v)?.name || ''),
            },
          ]
        : [],
    );
  }

  function getSelectValue() {
    return criteria?.[0]?.listValue;
  }

  function onLoad() {
    return onLoadSelect?.(field);
  }

  return <LoadableSelect value={getSelectValue()} onChange={onSelectChange} onLoad={onLoad} mode={mode} />;
}

function renderDateField(
  definition: DateFilterDefinition,
  criteria?: InnerCriteria,
  onFieldChange?: (name: string, value: InnerCriteria) => void,
) {
  const { field, showTime, disabledDate, op = 'eq' } = definition;

  function onDateChange(date: Moment | null) {
    if (date) {
      const value = date.toISOString();
      const label = showTime ? `${date.format('L')} ${date.format('LTS')}` : date.format('L');
      onFieldChange?.(field, [{ field, operator: op, value, label }]);
    } else {
      onFieldChange?.(field, []);
    }
  }

  function getDateValue(): Moment | null {
    const value = criteria?.[0]?.value;
    return value && typeof value === 'string' ? moment(value, moment.ISO_8601) : null;
  }

  return (
    <DatePicker
      value={getDateValue()}
      onChange={onDateChange}
      style={{ width: '100%' }}
      showTime={showTime}
      disabledDate={disabledDate}
    />
  );
}

function renderDateRangeField(
  definition: DateRangeFilterDefinition,
  criteria?: InnerCriteria,
  onFieldChange?: (name: string, value: InnerCriteria) => void,
) {
  const { field, showTime, disabledDate } = definition;

  function onRangeChange(dates: [any, any] | null) {
    if (dates) {
      const [first, second] = dates as [Moment | null, Moment | null];
      const res: InnerCriteria = [];
      if (first) {
        const value = first.toISOString();
        const label = showTime ? `${first.format('L')} ${first.format('LTS')}` : first.format('L');
        res.push({ field, operator: 'greaterEqual', value, label });
      }
      if (second) {
        const value = second.toISOString();
        const label = showTime ? `${second.format('L')} ${second.format('LTS')}` : second.format('L');
        res.push({ field, operator: 'lessEqual', value, label });
      }
      onFieldChange?.(field, res);
    } else {
      onFieldChange?.(field, []);
    }
  }

  function getRangeValue(): [Moment | null, Moment | null] {
    const first = criteria?.find((cr) => cr.operator === 'greaterEqual')?.value;
    const second = criteria?.find((cr) => cr.operator === 'lessEqual')?.value;
    return [
      first && typeof first === 'string' ? moment(first, moment.ISO_8601) : null,
      second && typeof second === 'string' ? moment(second, moment.ISO_8601) : null,
    ];
  }

  return (
    <RangePicker
      value={getRangeValue()}
      style={{ width: '100%' }}
      allowEmpty={[true, true]}
      onChange={onRangeChange}
      showTime={showTime}
      disabledDate={disabledDate}
    />
  );
}

export function renderFilterField(
  definition: FilterDefinition,
  criteria?: InnerCriteria,
  onFieldChange?: (name: string, value: InnerCriteria) => void,
  onLoadSelect?: (name: string) => Promise<Dictionary> | undefined,
) {
  const { type = 'string' } = definition;

  switch (type) {
    case 'date': {
      return renderDateField(definition as DateFilterDefinition, criteria, onFieldChange);
    }
    case 'dateRange': {
      return renderDateRangeField(definition as DateRangeFilterDefinition, criteria, onFieldChange);
    }
    case 'number': {
      return renderNumberField(definition as NumberFilterDefinition, criteria, onFieldChange);
    }
    case 'string': {
      return renderInputField(definition as StringFilterDefinition, criteria, onFieldChange);
    }
    case 'select': {
      return renderSelectField(definition as SelectFilterDefinition, criteria, onFieldChange, onLoadSelect);
    }
    case 'boolean': {
      return renderBooleanField(definition as BooleanFilterDefinition, criteria, onFieldChange);
    }
  }
}
