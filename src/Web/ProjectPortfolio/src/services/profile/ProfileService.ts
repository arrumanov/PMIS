import { reflection } from 'first-di';
import { AHttpService } from '@st/unified-core';

import { Profile } from './types';
import { AProfileService } from './AProfileService';

const mock: Profile = {
  login: 'root',
  fullName: 'Chemko E.S.',
  language: 'ru',
  roles: [
    'ROLE_MENU_DEALS',
    'ROLE_MENU_CURRENCIES',
    'ROLE_SECURITY_MARKET_QUOTE_D',
    'ROLE_SECURITY_MARKET_QUOTE_W',
    'ROLE_SECURITY_COUPON_ACCRUED_INTEREST_D',
    'ROLE_SECURITY_COUPON_ACCRUED_INTEREST_W',
    'ROLE_SECURITY_COUPON_W',
    'ROLE_SECURITY_COUPON_D',
    'ROLE_SECURITY_IMPORT_W',
    'ROLE_SECURITY_IMPORT_D',
    'ROLE_DEAL_CLIENT_EXT_W',
    'ROLE_IP_CONTRACT_TYPE_W',
    'ROLE_IP_CONTRACT_TYPE_H',
    'ROLE_IP_CONTRACT_TYPE_D',
    'ROLE_IP_CONTRACT_W',
    'ROLE_IP_CONTRACT_APPROVE',
    'ROLE_IP_CONTRACT_REGISTER',
    'ROLE_IP_CONTRACT_CANCEL',
    'ROLE_IP_CONTRACT_CLOSE',
    'ROLE_IP_CONTRACT_H',
    'ROLE_IP_CONTRACT_D',
    'ROLE_ACCOUNT_W',
    'ROLE_ACCOUNT_D',
  ],
};

@reflection
export class ProfileService extends AProfileService {
  constructor(private readonly httpService: AHttpService) {
    super();
  }

  load() {
    return this.httpService.get('/administration-service/users/profile');
  }
}
