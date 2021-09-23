import { Profile } from './types';

export abstract class AProfileService {
  abstract load(): Promise<Profile>;
}
