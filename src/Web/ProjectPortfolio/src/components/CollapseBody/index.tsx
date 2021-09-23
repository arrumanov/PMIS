import React, { ReactNode } from 'react';

import classes from './styles.module.less';

type Props = {
    children: ReactNode;
};

export function CollapseBody({ children }: Props) {
    return <div className={classes.collapseBody}>{children}</div>;
}
