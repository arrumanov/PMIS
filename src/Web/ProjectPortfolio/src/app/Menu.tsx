import { MenuComponent, MenuItem } from 'components/Menu';
import { observer } from 'mobx-react';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { IconCodeType } from 'types';
import { AppController } from './AppController';
import { Roles } from './Roles';
import { Routes } from './Routes';

type StructItem = {
  code: string;
  icon?: IconCodeType;
  route?: string;
  restriction?: string;
  children?: StructItem[];
};

const struct: StructItem[] = [
  {
    code: 'projects',
    icon: 'DashboardOutlined',
    route: Routes.PROJECTS,
  },
  {
    code: 'dashboard',
    icon: 'DashboardOutlined',
    route: Routes.DASHBOARD,
  },
  {
    code: 'main',
    icon: 'AppstoreOutlined',
    children: [
      {
        code: 'minimal',
        icon: 'TableOutlined',
        route: Routes.MINIMAL,
      },
      {
        code: 'simple',
        icon: 'TableOutlined',
        route: Routes.SIMPLE,
      },
      {
        code: 'workflow',
        icon: 'TableOutlined',
        route: Routes.WORKFLOW,
      },
      {
        code: 'multi',
        icon: 'TableOutlined',
        route: Routes.MULTI,
      },
      {
        code: 'master',
        icon: 'TableOutlined',
        route: Routes.MASTER,
      },
    ],
  },
  {
    code: 'admin',
    icon: 'SettingOutlined',
    restriction: Roles.ROLE_ADMIN,
  },
];

function exist(i?: MenuItem): i is MenuItem {
  return !!i;
}

export const Menu = observer(function Menu() {
  const { t } = useTranslation();
  const { hasRole } = AppController;

  function mapStruct(item: StructItem): MenuItem | undefined {
    const { code, icon, route, restriction, children } = item;

    return !restriction || hasRole(restriction)
      ? {
          code,
          name: t(`menu.${code}`),
          icon,
          route,
          children: children?.map(mapStruct).filter(exist),
        }
      : undefined;
  }

  return <MenuComponent items={struct.map(mapStruct).filter(exist)} />;
});
