import { notification } from 'antd';
import { ArgsProps } from 'antd/lib/notification';

export function showError({ placement = 'bottomRight', ...args }: ArgsProps) {
    notification.error({ placement, ...args });
}

export function showSuccess({ placement = 'bottomRight', ...args }: ArgsProps) {
    notification.success({ placement, ...args });
}

export function showWarn({ placement = 'bottomRight', ...args }: ArgsProps) {
    notification.warn({ placement, ...args });
}

export function showInfo({ placement = 'bottomRight', ...args }: ArgsProps) {
    notification.info({ placement, ...args });
}
