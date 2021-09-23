import { Skeleton } from 'antd';
import { initContainer } from 'configureContainer';
import { initHttp } from 'configureHttp';
import { initI18n } from 'configureI18n';
import { configure } from 'mobx';
import React, { lazy, Suspense, useEffect, useState } from 'react';
import { withRouter } from 'react-router';
import 'theme/index.less';

const App = lazy(() => import('./App')); //needs to be async to initialyze container

function RoutedApp() {
  const [loading, setLoading] = useState(true);

  async function init() {
    configure({ enforceActions: 'observed' });
    await initI18n('ru');
    initContainer();
    initHttp();
    setLoading(false);
  }

  useEffect(() => {
    init();
  }, []);

  return loading ? (
    <Skeleton loading />
  ) : (
    <Suspense fallback={<Skeleton loading />}>
      <App />
    </Suspense>
  );
}

export default withRouter(RoutedApp);
