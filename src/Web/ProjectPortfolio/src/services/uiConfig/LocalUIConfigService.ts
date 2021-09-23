import { AUIConfigService } from './AUIConfigService';
import { TableConfig } from './types';

export class LocalUIConfigService extends AUIConfigService {
  async get(key: string): Promise<TableConfig> {
    return JSON.parse(localStorage.getItem(key) || '{}');
  }

  async set(key: string, value: TableConfig): Promise<void> {
    localStorage.setItem(key, JSON.stringify(value));
  }

  async delete(key: string): Promise<void> {
    localStorage.removeItem(key);
  }
}
