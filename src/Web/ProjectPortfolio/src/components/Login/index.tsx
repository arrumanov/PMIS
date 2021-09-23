import React, { useState } from 'react';
import { Button, Input, Space, Typography } from 'antd';

import styles from './Login.module.less';

const { Text, Title } = Typography;

type Props = {
  onSubmit?: (username: string, password: string) => void;
};

export function Login({ onSubmit }: Props) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  function onUserNameChange(e: React.ChangeEvent<HTMLInputElement>) {
    setUsername(e.target.value);
  }

  function onPasswordChange(e: React.ChangeEvent<HTMLInputElement>) {
    setPassword(e.target.value);
  }

  function onLogin() {
    onSubmit?.(username, password);
  }

  function onKeyUp(e: React.KeyboardEvent<HTMLFormElement>) {
    if (e.key === 'Enter') {
      onLogin();
    }
  }

  return (
    <div className={styles.root}>
      <div className={styles.left}>
        <div></div>
      </div>

      <div className={styles.right}>
        <form onKeyUp={onKeyUp} className={styles.form}>
          <Space direction="vertical" style={{ width: '100%' }}>
            <Title className={styles.title}>Sign in</Title>
            <Text type="secondary">Enter your credentials</Text>

            <Input placeholder={'Login'} value={username} onChange={onUserNameChange} />
            <Input placeholder={'Password'} value={password} onChange={onPasswordChange} type="password" />

            <Button type="primary" style={{ width: '100%' }} onClick={onLogin}>
              Enter
            </Button>
          </Space>
        </form>

        <Text type="secondary">Copyright © 2020</Text>
        <br />
      </div>
    </div>
  );
}
