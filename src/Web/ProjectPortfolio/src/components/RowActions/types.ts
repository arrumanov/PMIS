import { ReactNode } from 'react';

export type RowAction = {
  disabled?: boolean;
  icon?: ReactNode;
  actionCode: string;
  actionName: string;
  onClick: () => void;
};
