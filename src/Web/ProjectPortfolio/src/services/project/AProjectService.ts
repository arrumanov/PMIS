import { Filter, Page } from 'types';
import { Project } from './modeles';

export abstract class AProjectService {
  abstract get data(): Project[];
  abstract set data(data: Project[]);
  abstract list: (filter?: Filter) => Promise<Page<Project>>;
  abstract create: () => void;
}
