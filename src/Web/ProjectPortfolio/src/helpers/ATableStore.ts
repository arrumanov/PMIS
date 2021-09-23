import { TablePaginationConfig } from 'antd/lib/table';
import { SorterResult } from 'antd/lib/table/interface';

import { getSort, getTableShown, LocalColumnType } from './TableUtils';
import { Page, Filter } from 'types';
import { fromPromise, PromiseObserver } from './FromPromise';
import { SizeType } from 'helpers/useSize';
import { action, extendObservable, makeObservable, observable, reaction } from 'mobx';
import { autowired } from 'first-di';
import { AUIConfigService, TableConfig } from 'services/uiConfig';

type DataType<T> = T[] | Page<T>;

export abstract class ATableStore<T extends Record<string, any>, P extends DataType<T>> {
  @autowired() private readonly UIConfigureService!: AUIConfigService;

  protected configUUID?: string;
  protected defaultSize?: SizeType;
  protected defaultColumns?: LocalColumnType<T>[];
  protected defaultSorter?: SorterResult<T> | SorterResult<T>[];
  protected defaultPagination: TablePaginationConfig = {
    current: 1,
    total: 0,
    pageSize: 10,
    showSizeChanger: true,
    showTotal: getTableShown,
  };

  protected currentSize?: SizeType;
  protected currentColumns?: LocalColumnType<T>[];
  protected currentSorter?: SorterResult<T> | SorterResult<T>[];
  protected currentPagination?: TablePaginationConfig;

  protected dataPromise?: PromiseObserver<P>;
  protected uiPromise?: PromiseObserver<TableConfig>;

  selectedRows: T[] = [];
  criteria?: Filter['criteria'] = undefined;

  constructor(readonly rowKey: keyof T & string) {
    makeObservable(this, {
      selectedRows: observable,
      criteria: observable,
      loadData: action,
      setColumns: action,
      setCriteria: action,
      setSelectedRows: action,
      setSize: action,
      setPagination: action,
    });

    extendObservable(this, {
      dataPromise: undefined,
      uiPromise: undefined,
      currentSize: undefined,
      currentColumns: undefined,
      currentSorter: undefined,
      currentPagination: undefined,
    });

    reaction(
      () => ({ cols: this.columns, size: this.size }),
      ({ cols, size }) => {
        this.configUUID &&
          this.UIConfigureService.set(this.configUUID, {
            size,
            columns: cols.map(({ dataIndex, width, hidden, index }) => ({ dataIndex, width, hidden, index })),
          });
      },
    );

    reaction(
      () => this.uiPromise?.fulfilled,
      (fulfilled) => {
        if (fulfilled) {
          const colsConf = this.uiPromise?.value?.columns || [];
          const keys: string[] = colsConf.map((c) => c.dataIndex);
          const newCols: LocalColumnType<T>[] = this.columns
            .map((c) =>
              keys.includes(c.dataIndex) ? { ...c, ...colsConf.find((cc) => cc.dataIndex === c.dataIndex) } : c,
            )
            .sort((a, b) => (a.index || 0) - (b.index || 0));
          this.setColumns(newCols);

          if (this.uiPromise?.value?.size) {
            this.setSize(this.uiPromise.value.size);
          }
        }
      },
    );
  }

  abstract get data(): T[];

  protected abstract load(filter: Filter): Promise<P>;

  protected abstract getData(): Promise<P>;

  get selectedIds() {
    return this.selectedRows.map<string | number>((row) => row[this.rowKey]);
  }

  get loading() {
    return this.dataPromise?.pending;
  }

  get error() {
    return this.dataPromise?.error?.message;
  }

  get size() {
    return this.currentSize || this.defaultSize;
  }

  get sorter() {
    return this.currentSorter || this.defaultSorter;
  }

  get pagination() {
    return this.currentPagination || this.defaultPagination;
  }

  get columns() {
    return this.currentColumns || this.defaultColumns || [];
  }

  setSize = (size?: SizeType) => {
    this.currentSize = size;
  };

  setColumns = (columns?: LocalColumnType<T>[]) => {
    this.currentColumns = columns;
  };

  setSelectedRows = (selectedRows: T[] = []) => {
    this.selectedRows = selectedRows;
  };

  setPagination(pagination: TablePaginationConfig) {
    this.currentPagination = { ...this.pagination, ...pagination };
  }

  setCriteria = (criteria?: Filter['criteria']) => {
    this.criteria = criteria;
    this.currentPagination = { ...this.pagination, current: 1 };
    this.loadData();
  };

  protected getFilter(): Filter {
    const { current = 0, pageSize } = this.pagination;
    const filter: Filter = {
      page: current - 1,
      size: pageSize,
      criteria: this.criteria,
      fetch: [],
      sort: getSort(this.sorter),
    };
    return filter;
  }

  loadUIConfig = () => {
    if (this.configUUID) {
      this.uiPromise = fromPromise(this.UIConfigureService.get(this.configUUID), { oldData: this.uiPromise?.value });
    } else {
      throw new Error(`Can't load UI config for empty configUUID`);
    }
  };

  loadData = (pagination?: TablePaginationConfig, _filters?: any, sorter?: SorterResult<T> | SorterResult<T>[]) => {
    if (pagination) {
      this.currentPagination = { ...this.pagination, ...pagination };
    }
    if (sorter) {
      this.currentSorter = sorter;
    }
    this.dataPromise = fromPromise(this.getData(), { oldData: this.dataPromise?.value });
  };

  clear = () => {
    this.dataPromise = undefined;
    this.criteria = undefined;
    this.currentSorter = undefined;
    this.currentPagination = undefined;
    this.currentColumns = undefined;
    this.currentSize = undefined;
  };
}
