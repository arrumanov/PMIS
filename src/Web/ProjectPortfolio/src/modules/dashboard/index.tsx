import React from 'react';
import { PageHeader, Row, Col, Card, Button } from 'antd';
import { MoreOutlined } from '@ant-design/icons';

import { default as Chart1 } from './LineBarAreaComposedChart';
import { default as Chart2 } from './CustomContentTreemap';
import { default as Chart3 } from './CustomActiveShapePieChart';
import { default as Chart4 } from './TwoLevelPieChart';
import { Container } from 'components/Container';

export const Dashboard = () => {
  return (
    <>
      <PageHeader
        title="Dashboard"
        subTitle="All information in real time"
        extra={[
          <Button key="0">Export</Button>,
          <Button key="1" type="primary">
            Configure
          </Button>,
        ]}
      />
      <Container>
        <Row gutter={[16, 16]}>
          <Col span={12}>
            <Card title="Simulation" extra={<Button type="link" icon={<MoreOutlined />} />}>
              <Chart1 />
            </Card>
          </Col>
          <Col span={12}>
            <Card title="Map" extra={<Button type="link" icon={<MoreOutlined />} />}>
              <Chart2 />
            </Card>
          </Col>
        </Row>
        <Row gutter={[16, 16]}>
          <Col span={8}>
            <Card title="Ring">
              <Chart3 />
            </Card>
          </Col>
          <Col span={8}>
            <Card title="Other data">
              <Chart4 />
            </Card>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Dashboard;
