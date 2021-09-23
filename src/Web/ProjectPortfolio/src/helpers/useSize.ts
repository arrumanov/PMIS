import { useContext } from 'react';
import SizeContext, { SizeType } from 'antd/es/config-provider/SizeContext';

export { SizeType };

export function useSize() {
  return useContext(SizeContext);
}
