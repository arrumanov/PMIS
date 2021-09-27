import { Layout, Skeleton, Spin } from 'antd';
import { AppHeader } from 'components/Header';
import { observer } from 'mobx-react';
import React, { lazy, Suspense, useEffect } from 'react';
import { Redirect, Route, Switch } from 'react-router';
import { AppController } from './AppController';
import { AuthController } from './AuthController';
import { Menu } from './Menu';
import { Routes } from './Routes';

const DashboardModule = lazy(() => import('modules/dashboard'));
const SimpleTableModule = lazy(() => import('modules/simpleTable'));
const MasterTableModule = lazy(() => import('modules/masterTable'));
const WFTableModule = lazy(() => import('modules/wfTable'));
const MultiTableModule = lazy(() => import('modules/multiTable'));
const MinTableModule = lazy(() => import('modules/minimalTable'));
const ProjectsModule = lazy(() => import('modules/projects'));

export const AppBody = observer(() => {
  useEffect(() => {
    AppController.init();
  }, []);

  const { loading } = AppController;

  return loading ? (
    <Skeleton />
  ) : (
    <Layout>
      <Menu />
      <Layout>
        <AppHeader onSignout={AuthController.logout} />
        <div id="content">
          <Suspense fallback={<Spin className="spinner" style={{ margin: 'auto' }} />}>
            <Switch>
              <Route exact path={`/${Routes.PROJECTS}`} component={ProjectsModule} />
              <Route exact path={`/${Routes.DASHBOARD}`} component={DashboardModule} />
              <Route exact path={`/${Routes.SIMPLE}`} component={SimpleTableModule} />
              <Route exact path={`/${Routes.MINIMAL}`} component={MinTableModule} />
              <Route exact path={`/${Routes.MASTER}`} component={MasterTableModule} />
              <Route exact path={`/${Routes.WORKFLOW}`} component={WFTableModule} />
              <Route exact path={`/${Routes.MULTI}`} component={MultiTableModule} />
              <Redirect to={`/${Routes.PROJECTS}`} />
            </Switch>
          </Suspense>
        </div>
      </Layout>
    </Layout>
  );
});
