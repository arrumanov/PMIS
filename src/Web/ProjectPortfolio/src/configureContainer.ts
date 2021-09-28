import {
  AAuthService,
  AHttpService,
  AStorageService,
  LocalStorageService,
  MockAuthService,
  PureHttpService,
} from '@st/unified-core';
import { override } from 'first-di';
import { AClientsService, ClientsService } from 'services/clients';
import { AProjectService, ProjectService } from 'services/project';
import { AProfileService, MockProfileService } from 'services/profile';
import { AUIConfigService, LocalUIConfigService } from 'services/uiConfig';
import { AWizzardService, MockWizzardService } from 'services/wizzard';
import { AWFService, MockWFService } from 'services/workflow';

export function initContainer() {
  override(AAuthService, MockAuthService);
  override(AStorageService, LocalStorageService);
  override(AHttpService, PureHttpService);

  override(AWFService, MockWFService);
  override(AProfileService, MockProfileService);
  override(AClientsService, ClientsService);
  override(AProjectService, ProjectService);
  override(AWizzardService, MockWizzardService);
  override(AUIConfigService, LocalUIConfigService);

  if (process.env.NODE_ENV === 'test') {
    //TODO: overrides for test
  }
}
