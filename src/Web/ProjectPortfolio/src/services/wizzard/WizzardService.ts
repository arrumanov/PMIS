import { AHttpService } from '@st/unified-core';
import { reflection } from 'first-di';
import { AWizzardService } from './AWizzardService';

const WIZZ_PATH = process.env.WIZZARD_PATH_PREFIX;

@reflection
export class WizzardService extends AWizzardService {
  constructor(private readonly http: AHttpService) {
    super();
  }

  getForm(data: string): Promise<string> {
    return this.http.post(`${WIZZ_PATH}/api/request/process`, {
      headers: {
        'Content-Type': 'application/xml',
        'Accept-Language': 'ru-RU',
      },
      data,
    });
  }
}
