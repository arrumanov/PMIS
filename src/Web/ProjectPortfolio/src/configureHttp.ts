import { AHttpService } from '@st/unified-core';

export function initHttp() {
  AHttpService.checkResponseRetry = async ({ ok, status }: Response): Promise<boolean> => {
    if (ok || status === 500) {
      return false;
    }
    if (status === 401) {
      //TODO: refresh token
      return true;
    }
    return true; //other (network) errors
  };

  AHttpService.processError = async ({ status, text }: Response): Promise<string> => {
    switch (status) {
      case 404: {
        return 'Service is unavailable';
      }
      default: {
        return text();
      }
    }
  };
}
