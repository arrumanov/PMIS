import delay from 'lodash/delay';
import { reflection } from 'first-di';
import { Filter, Page } from 'types'

import { Project } from './modeles';
import { AProjectService } from './AProjectService';


@reflection
export class ProjectService implements AProjectService {

    private dataSource!: Project[];

    get data(): Project[]{
        return this.dataSource || [];
    }

    set data(dataSource: Project[]){
        this.dataSource = dataSource || [];
    }

  list(filter?: Filter) {
    const { page = 0, size = 10 } = filter || {};
    const data: Page<Project> = {
      number: 1,
      totalElements: this.dataSource.length,
      content: this.dataSource.slice(page * size, (page + 1) * size),
    };
    //return Promise.resolve(data);
    return new Promise<Page<Project>>((res) => delay(() => res(data), 1000));
  }

  create() {}
}
