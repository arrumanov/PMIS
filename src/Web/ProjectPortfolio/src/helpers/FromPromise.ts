import { makeAutoObservable } from 'mobx';

type PromiseState = 'pending' | 'fulfilled' | 'rejected';

export class PromiseObserver<T> {
  state: PromiseState = 'pending';
  value?: T = undefined;
  error?: Error = undefined;

  constructor(target: Promise<T>, oldValue?: T) {
    this.value = oldValue;

    makeAutoObservable(this);

    target.then(this.onResolve).catch(this.onReject);
  }

  private onReject = (error: Error) => {
    this.error = error;
    this.state = 'rejected';
  };

  private onResolve = (value: T) => {
    this.value = value;
    this.state = 'fulfilled';
  };

  get pending() {
    return this.state === 'pending';
  }

  get rejected() {
    return this.state === 'rejected';
  }

  get fulfilled() {
    return this.state === 'fulfilled';
  }
}

export function fromPromise<T>(origPromise: Promise<T>, options?: { oldData?: T }): PromiseObserver<T> {
  return new PromiseObserver<T>(origPromise, options?.oldData);
}
