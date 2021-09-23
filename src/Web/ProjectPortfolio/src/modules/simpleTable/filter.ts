import moment, { Moment } from 'moment';
import { FilterDefinition } from 'components/Filter';

export const filterDefinition: FilterDefinition[] = [
  {
    label: 'Date',
    field: 'created',
    type: 'dateRange',
    showTime: true,
    disabledDate: function(date: Moment) {
      return date.diff(moment.now()) >= 0;
    },
  },
  { label: 'Client ID', field: 'clientID', op: 'like' },
  { label: 'Channel ID', field: 'channelId', type: 'number' },
  { label: 'User ID', field: 'userId', type: 'number' },
  { label: 'User login', field: 'userLogin', op: 'like' },
  { label: 'Status', field: 'httpStatus', type: 'number' },
  { label: 'Token', field: 'token', op: 'like' },
  { label: 'Method', field: 'method', op: 'like' },
  { label: 'Path', field: 'path', op: 'like' },
  { label: 'IP', field: 'address', op: 'like' },
  { label: 'Link', field: 'referer', op: 'like' },
];
