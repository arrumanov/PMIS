import { Filter, Page } from 'types';
import { ClientDTO, DetailDTO } from './modeles';

export abstract class AClientsService {
  abstract list: (filter?: Filter) => Promise<Page<ClientDTO>>;
  abstract details: (clientId: ClientDTO['id']) => Promise<DetailDTO[]>;
  abstract create: () => void;
}
