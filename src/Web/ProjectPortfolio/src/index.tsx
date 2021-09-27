// Render the top-level React component
import 'core-js/stable';
import {gql, ApolloClient, NormalizedCacheObject, ApolloProvider, InMemoryCache} from '@apollo/client';
// import {cache} from './graphql/chache';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import 'reflect-metadata';
import App from './app';

const client: ApolloClient<NormalizedCacheObject> = new ApolloClient({
  uri: 'https://localhost:44344/graphql/',
  cache: new InMemoryCache(),  
});

ReactDOM.render(
  <BrowserRouter>
    <ApolloProvider client={client}>
      <App />
    </ApolloProvider>
  </BrowserRouter>,
  document.getElementById('root'),
);
