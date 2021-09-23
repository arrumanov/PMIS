import React, { PropsWithChildren, ReactElement } from 'react';
import { observer } from 'mobx-react';

type Props = {
  permission: string;
  ReplacementComponent?: ReactElement | null;
};

export const WithUIPermission = observer(function WithUIPermission({
  children,
  permission,
  ReplacementComponent = null,
}: PropsWithChildren<Props>) {
  return null; // ResourcesManager.hasUIPermission(permission) ? <>{children}</> : ReplacementComponent;
});
