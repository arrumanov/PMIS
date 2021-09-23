import * as Icons from '@ant-design/icons';

export * from './Dictionary';
export * from './Filter';
export * from './Page';
export * from './Uuids';

export type IconCodeType = keyof Omit<
  typeof Icons,
  'getTwoToneColor' | 'setTwoToneColor' | 'createFromIconfontCN' | 'default' | 'TwoToneColor'
> &
  string;

export interface Mapper<I, O> {
  map(param: I): O;
  unmap(param: O): I;
}
