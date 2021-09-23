import { TableConfig } from './types';

export abstract class AUIConfigService {
  abstract get(key: string): Promise<TableConfig>;
  abstract set(key: string, value: TableConfig): Promise<void>;
  abstract delete(key: string): Promise<void>;
}
