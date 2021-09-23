import { Moment } from 'moment';
import { Filter, Dictionary, FilterCriteriaWithoutGroup } from 'types';

export type StringFilterDefinition = {
  label: string;
  field: string;
  type?: 'string';
  op?: 'eq' | 'like';
};

export type BooleanFilterDefinition = {
  label: string;
  field: string;
  type?: 'boolean';
  op?: 'eq';
};

export type NumberFilterDefinition = {
  label: string;
  field: string;
  type: 'number';
  op?: 'eq';
  step?: number;
};

export type DateFilterDefinition = {
  label: string;
  field: string;
  type: 'date';
  op?: 'eq' | 'greaterEqual' | 'lessEqual';
  showTime?: boolean;
  disabledDate?: (date: Moment) => boolean;
};

export type DateRangeFilterDefinition = {
  label: string;
  field: string;
  type: 'dateRange';
  showTime?: boolean;
  disabledDate?: (date: Moment) => boolean;
};

export type SelectFilterDefinition = {
  label: string;
  field: string;
  type: 'select';
  mode?: 'multiple';
};

export type FilterDefinition =
  | StringFilterDefinition
  | BooleanFilterDefinition
  | NumberFilterDefinition
  | DateFilterDefinition
  | DateRangeFilterDefinition
  | SelectFilterDefinition;

export type Criteria = Filter['criteria'];

export type InnerCriteria = Array<FilterCriteriaWithoutGroup> | undefined;

export type RenderAsType = 'modal' | 'drawer';

export type Props = {
  filterDefinition: FilterDefinition[];
  value?: Criteria;
  onApply?: (criteria: Criteria) => void;
  loadSelect?: (field: string, params?: Record<string, string>) => Promise<Dictionary> | undefined;
  isButton?: boolean;
  renderAs?: RenderAsType;
};
