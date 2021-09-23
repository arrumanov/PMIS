import { PromiseObserver } from 'helpers/FromPromise';
import { showSuccess, showError } from 'components/Notification';
import { autorun } from 'mobx';

export function observeError(promise?: PromiseObserver<any>, message?: string) {
  return (
    promise &&
    autorun(() => {
      promise.rejected && showError({ message: message || promise.error?.message });
    })
  );
}

export function observeSuccess(promise?: PromiseObserver<any>, message?: string) {
  return (
    promise &&
    message &&
    autorun(() => {
      promise.fulfilled && showSuccess({ message });
    })
  );
}
