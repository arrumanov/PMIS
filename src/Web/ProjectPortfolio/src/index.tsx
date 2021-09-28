// Render the top-level React component
import 'core-js/stable';
import {gql, ApolloClient, NormalizedCacheObject, ApolloProvider, InMemoryCache} from '@apollo/client';
// import {cache} from './graphql/chache';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import 'reflect-metadata';
import App from './app';

// инициализация ApolloClient
const client: ApolloClient<NormalizedCacheObject> = new ApolloClient({
  uri: 'https://localhost:44344/graphql/', //указывает URL-адрес нашего сервера GraphQL
  cache: new InMemoryCache(), //Apollo Client использует для кэширования результатов запроса после их получения
});

//ApolloProvider обертывает приложение React и помещает Apollo Client в контекст, 
//что позволяет получить к нему доступ из любого места в дереве компонентов
ReactDOM.render(
  <BrowserRouter>
    <ApolloProvider client={client}>
      <App />
    </ApolloProvider>
  </BrowserRouter>,
  document.getElementById('root'),
);
