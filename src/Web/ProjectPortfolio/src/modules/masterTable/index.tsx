import React from 'react';
import { reaction } from 'mobx';

import { Master, store as masterStore } from './Master';
import { Details, store as detailStore } from './Details';

reaction(() => masterStore.selectedId, detailStore.setClientId);

export default function Example() {
  return (
    <>
      <Master />
      <Details />
    </>
  );
}
