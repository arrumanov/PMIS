import { TablePaginationConfig } from 'antd/lib/table';
import { SorterResult } from 'antd/lib/table/interface';
import { ATableStore } from './ATableStore';

export abstract class ALocalTableStore<T> extends ATableStore<T, T[]> {
  get data(): T[] {
    return this.dataPromise?.value || [];
  }

  protected async getData() {
    const data = await this.load(this.getFilter());
    this.setPagination({ total: data.length });
    return data;
  }

  changePage = (pagination?: TablePaginationConfig, _filters?: any, sorter?: SorterResult<T> | SorterResult<T>[]) => {
    if (pagination) {
      this.setPagination(pagination);
    }
    if (sorter) {
      this.currentSorter = sorter;
    }
  };
}
