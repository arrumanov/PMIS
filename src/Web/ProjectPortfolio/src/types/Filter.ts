export enum SORT_ORDER {
  ASC = 'ASC',
  DESC = 'DESC',
}

export type FilterCriteriaItem = {
  field: string;
  operator: 'like' | 'equals' | 'eq' | 'greaterEqual' | 'lessEqual' | 'unequal';
  value: string | number | boolean;
  label?: string;
  listValue?: undefined;
  listLabel?: undefined;
};

export type FilterCriteriaListItem = {
  field: string;
  operator: 'in' | 'notIn';
  listValue: string[];
  listLabel?: string[];
  value?: undefined;
  label?: undefined;
};

export type FilterCriteriaGroup = {
  group: Array<FilterCriteria>;
  insideGroupOperator: 'or' | 'and';
};

export type FilterCriteria = FilterCriteriaItem | FilterCriteriaListItem | FilterCriteriaGroup;

export type FilterCriteriaWithoutGroup = Exclude<FilterCriteria, FilterCriteriaGroup>;

export function isCreteriaWithoutGroup(value: any): value is FilterCriteriaWithoutGroup {
  return (
    typeof value.field === 'string' &&
    typeof value.operator === 'string' &&
    (typeof value.value === 'string' ||
      typeof value.value === 'number' ||
      typeof value.value === 'boolean' ||
      Array.isArray(value.listValue))
  );
}

export type Filter = {
  criteria?: {
    criteriaItems: FilterCriteria[];
  };
  fetch?: {
    field: string;
  }[];
  page?: number;
  size?: number;
  sort?: {
    field: string;
    order: SORT_ORDER;
  }[];
};
