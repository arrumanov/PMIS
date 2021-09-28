import moment, { Moment } from 'moment';
import { FilterDefinition } from 'components/Filter';

export const filterDefinition: FilterDefinition[] = [
  { label: 'Project ID', field: 'id', op: 'like' },
  { label: 'Name', field: 'name', op: 'like' },
  { label: 'Description', field: 'description', op: 'like' },
];