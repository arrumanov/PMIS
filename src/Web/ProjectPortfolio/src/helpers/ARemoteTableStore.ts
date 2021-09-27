import { Page } from 'types';
import { ATableStore } from './ATableStore';

export abstract class ARemoteTableStore<T> extends ATableStore<T, Page<T>> {
  protected dataGql?: T[];

  setDataGql(data: T[]){
    this.dataGql = data;
  }

  get data(): T[] {
    return this.dataGql || [];
  }

  protected async getData() {
    const data = await this.load(this.getFilter());
    this.setPagination({ total: data.totalElements });
    return data;
  }

  /** Sets selection on current page. Saves selection on other pages */
  setPageSelectedRows = (selectedRows: T[] = []) => {
    const otherRows = this.selectedRows.filter((sr) => !this.data.find((di) => di[this.rowKey] === sr[this.rowKey]));
    this.setSelectedRows([...selectedRows, ...otherRows]);
  };
}
