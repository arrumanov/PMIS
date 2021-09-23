import moment from 'moment';

type NumberFormat = 'number' | 'percent';

export function formatDate(ISODate?: string) {
  return ISODate ? moment(ISODate, moment.ISO_8601).format('L') : undefined;
}

export function formatDateTime(ISODate?: string) {
  return ISODate ? moment(ISODate, moment.ISO_8601).format('L LTS') : undefined;
}

export function formatDateTimeShort(ISODate?: string) {
  return ISODate ? moment(ISODate, moment.ISO_8601).format('L LT') : undefined;
}

export function formatNumber(value?: string | number, type: NumberFormat = 'number'): string {
  const res = parseFloat(`${value}`);
  if (!Number.isFinite(res)) {
    return '';
  }
  switch (type) {
    case 'number':
      return Intl.NumberFormat('en-GB', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
        minimumIntegerDigits: 1,
      }).format(res);
    case 'percent':
      return Intl.NumberFormat('en-GB', { minimumFractionDigits: 1 }).format(res * 100);
  }
}
