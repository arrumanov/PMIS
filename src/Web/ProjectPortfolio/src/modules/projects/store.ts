import { SorterResult } from 'antd/lib/table/interface';
import { autowired } from 'first-di';
import { action, makeObservable, observable } from 'mobx';

import { Filter } from 'types';
import { ARemoteTableStore } from 'helpers/ARemoteTableStore';
import { AProjectService, Project } from 'services/project';
import { PROJECT_TYPES } from './types';
import { userTableColumns } from './columns';
import { AWizzardService } from 'services/wizzard';
import { TableGid } from 'types';

const defaultSorter: SorterResult<Project> = { field: 'id', order: 'ascend' };

class Store extends ARemoteTableStore<Project> {
  @autowired() private readonly projectService!: AProjectService;
  @autowired() private readonly wizzService!: AWizzardService;

  configUUID = TableGid.Simple;
  defaultSorter = defaultSorter;
  defaultColumns = userTableColumns;

  activeTab: PROJECT_TYPES = PROJECT_TYPES.ALL;

  constructor(rowKey: keyof Project) {
    super(rowKey);

    makeObservable(this, {
      activeTab: observable,
      setActiveTab: action,
    });
  }

  setActiveTab(activeTab: PROJECT_TYPES) {
    this.activeTab = activeTab;
    this.setCriteria();
  }

  runWizz(data: string) {
    return this.wizzService.getForm(data);
  }

  protected load(filter: Filter) {
    return this.projectService.list(filter);
  }

  setData(dataSource: Project[]){
    this.projectService.data = dataSource;
  }
}

export const store = new Store('id');
