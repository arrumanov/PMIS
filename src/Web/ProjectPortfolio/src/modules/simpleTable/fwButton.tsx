import { Form } from '@st/fpi';
import { Button, message, Modal } from 'antd';
import { FileSearchOutlined } from '@ant-design/icons';
import React, { useState } from 'react';
import { v4 } from 'uuid';

import { store } from './store';

const FORMS_ID = '0';
let reqUid = v4();

export function FWButton() {
  const [open, setOpen] = useState<boolean>(false);

  function onOpen() {
    reqUid = v4();
    setOpen(true);
  }

  function onClose() {
    setOpen(false);
  }

  function onError(e: Error) {
    message.error(e.message);
    setOpen(false);
  }

  async function onLoadForm(body: string) {
    return store.runWizz(body);
  }

  return (
    <>
      <Button type="link" icon={<FileSearchOutlined />} onClick={onOpen} />
      <Modal
        title="Процесс просмотра клиента"
        visible={open}
        onCancel={onClose}
        width={800}
        footer={null}
        bodyStyle={{ padding: '0' }}
      >
        <Form RequestHash={reqUid} FormSetID={FORMS_ID} onFinish={onClose} onError={onError} load={onLoadForm} />
      </Modal>
    </>
  );
}
