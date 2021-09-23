import { reflection } from 'first-di';
import { AHttpService } from '@st/unified-core';
import { action, observable, runInAction } from 'mobx';

type NLS = {
  description: string | null;
  language: string;
  name: string;
};

type DictionaryEntity = {
  active: boolean;
  changeDate: string;
  changeUser: {
    name: string;
    displayName: string;
  };
  code: string;
  createDate: string;
  createUser: {
    name: string;
    displayName: string;
  };
  directory: string;
  id: string;
  nls: NLS[];
};

type DictionaryItem = {
  code: string;
  name: string;
  description: string;
  additionalParams?: Record<string, any>;
};

// разделить на системные и модульные, пример модульных currency и security
export type DictionaryTypes =
  | 'country'
  | 'gender'
  | 'security_category'
  | 'currency'
  | 'security_type'
  | 'issuer'
  | 'security_quote_type'
  | 'bond_type'
  | 'coupon_type'
  | 'coupon_payment_interval'
  | 'coupon_day_count_convention'
  | 'property_type'
  | 'security_category'
  | 'trust_management_type'
  | 'trust_product_type'
  | 'deal_channel'
  | 'client_category'
  | 'deal_client_activity'
  | 'deal_client_limit_type'
  | 'deal_client_comment_type'
  | 'deal_client_spot_check_type'
  | 'limit_currency_type'
  | 'position'
  | 'individual_group'
  | 'sign_type'
  | 'account_characterization'
  | 'counterpart_type'
  | 'account_open_mode'
  | 'day_type'
  | 'account_characterization';

type DictionaryMap = Partial<Record<DictionaryTypes, DictionaryItem[]>>;

@reflection
export class DictionariesService {
  constructor(private readonly httpService: AHttpService) {}

  @observable
  public loading: boolean = false;

  @observable
  public dictionaries: DictionaryMap = {};

  @observable
  public customDictionaries: Record<string, DictionaryItem[]> = {};

  convert(item: DictionaryEntity): DictionaryItem {
    const { id, nls, ...other } = item;
    return {
      code: id,
      name: nls?.find((i: NLS) => i.language === 'ru')?.name || '',
      description: nls?.find((i: NLS) => i.language === 'ru')?.description || '',
      additionalParams: other,
    };
  }

  @action
  public async loadDictionary(type: DictionaryTypes): Promise<void> {
    if (!this.dictionaries[type]) {
      this.loading = true;
      const response = await this.httpService.get(
        `/directory-service/directory-entries?query=directory.code==$${type}&size=20000`,
      );
      runInAction(() => {
        this.loading = false;
        this.dictionaries[type] = response.data.content.map(this.convert);
      });
    }
  }

  @action
  public async loadCustomDictionary(url: string, params?: Record<string, any>): Promise<void> {
    this.loading = true;
    let paramsString = 'size=20000';
    if (params) {
      paramsString +=
        '&' +
        Object.entries(params)
          .map(([key, value]) => `${key}=${value}`)
          .join('&');
    }
    const reqUrl = `${url}?${paramsString}`;
    const response = await this.httpService.get(reqUrl);
    runInAction(() => {
      this.loading = false;
      this.customDictionaries[url] = response.data.content.map(this.convert);
    });
  }
}
