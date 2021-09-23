import { ConfigProvider, Modal } from 'antd';
import ru_RU from 'antd/es/locale/ru_RU';
import { Login } from 'components/Login';
import { reaction } from 'mobx';
import { observer } from 'mobx-react';
import React, { useEffect } from 'react';
import { AppBody } from './AppBody';
import { AppController } from './AppController';
import { AuthController } from './AuthController';
import { ConfigButton } from './ConfigButton';

//modify default ant behaviour
Modal.defaultProps = { ...Modal.defaultProps, maskClosable: false };

function App() {
  const { isLoggedIn, loginPromise } = AuthController;
  const { size, setSize } = AppController;

  useEffect(() => {
    AuthController.init();

    return reaction(
      () => loginPromise?.rejected,
      (rejected) => {
        if (rejected) {
          alert(loginPromise?.error?.message);
        }
      },
    );
  }, [loginPromise]);

  return (
    <ConfigProvider componentSize={size} locale={ru_RU}>
      {isLoggedIn ? (
        <>
          <AppBody />
          <ConfigButton size={size} onChangeSize={setSize} />
        </>
      ) : (
        <Login onSubmit={AuthController.login} />
      )}
    </ConfigProvider>
  );
}

export default observer(App);
