import { SizeType } from 'antd/es/config-provider/SizeContext';
import { autowired } from 'first-di';
import { fromPromise, PromiseObserver } from 'helpers/FromPromise';
import { makeAutoObservable } from 'mobx';
import { AProfileService, Profile } from 'services/profile';

class AppState {
  @autowired() private readonly profileService!: AProfileService;

  size: SizeType = 'middle';
  collapsedMenu: boolean = false;
  profilePromise?: PromiseObserver<Profile> = undefined;

  constructor() {
    makeAutoObservable(this);
  }

  setSize = (size: SizeType) => {
    this.size = size;
  };

  private loadProfile() {
    this.profilePromise = fromPromise(this.profileService.load());
  }

  get loading() {
    return this.profilePromise?.pending;
  }

  init = () => {
    this.loadProfile();
  };

  get profile() {
    return this.profilePromise?.value;
  }

  get roles() {
    return this.profile?.roles;
  }

  hasRole = (roleName: string): boolean => {
    return !!this.roles?.includes(roleName);
  };
}

export const AppController = new AppState();
