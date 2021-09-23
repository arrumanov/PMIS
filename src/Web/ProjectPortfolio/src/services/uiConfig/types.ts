import { LocalColumnType } from 'helpers/TableUtils';
import { SizeType } from 'helpers/useSize';

export type ColumnConfig = Pick<LocalColumnType, 'dataIndex' | 'width' | 'hidden' | 'index'>;
export type TableConfig = { size: SizeType; columns: ColumnConfig[] };
