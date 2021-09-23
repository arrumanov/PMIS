import { AAuthService, AStorageService, LoginResponse, STORAGE_KEYS } from '@st/unified-core';
import { autowired } from 'first-di';
import { fromPromise, PromiseObserver } from 'helpers/FromPromise';
import { makeAutoObservable, reaction } from 'mobx';

class AuthState {
  @autowired() private readonly authService!: AAuthService;
  @autowired() private readonly persistentStorage!: AStorageService;

  loginPromise?: PromiseObserver<LoginResponse> = undefined;
  isLoggedIn = false;

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.loginPromise?.fulfilled,
      (fulfilled) => {
        if (fulfilled) {
          this.setIsLoggedIn(true);
          this.persistentStorage.set(STORAGE_KEYS.AUTH.TOKEN, JSON.stringify(this.loginPromise?.value));
        }
      },
    );
  }

  private setIsLoggedIn = (isLoggedIn: boolean) => {
    this.isLoggedIn = isLoggedIn;
  };

  get loading() {
    return this.loginPromise?.pending;
  }

  init = () => {
    let token = this.persistentStorage.get(STORAGE_KEYS.AUTH.TOKEN);
    if (token) {
      this.setIsLoggedIn(true);
    } else {
      const searchParams: URLSearchParams = new URLSearchParams(window.location.hash);
      token = searchParams.get('#access_token');
      if (token) {
        this.setIsLoggedIn(true);
        this.persistentStorage.set(STORAGE_KEYS.AUTH.TOKEN, token);
      }
    }
  };

  login = (username: string, password: string) => {
    this.loginPromise = fromPromise(this.authService.login({ username, password }));
  };

  logout = async () => {
    await this.authService.logout();
    this.persistentStorage.remove(STORAGE_KEYS.AUTH.TOKEN);
    this.setIsLoggedIn(false);
  };
}

export const AuthController = new AuthState();
