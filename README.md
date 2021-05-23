# PMIS

=======================================================================================================================================================

docker network create --driver bridge elasticsearchnetwork

docker run -p 9200:9200 -p 9300:9300 --network elasticsearchnetwork -e "discovery.type=single-node" --name elasticsearch elasticsearch:7.6.2

docker run --network elasticsearchnetwork --name kibana -p 5601:5601 kibana:7.6.2

docker container run -d --name some-rabbit -p 4369:4369 -p 5671:5671 -p 5672:5672 -p 25672:25672 -p 15671:15671 -p 8080:15672 rabbitmq:3-management

=======================================================================================================================================================




1) Пример проекта (брал за основу) - https://github.com/gbauso/GraphAPI
2) Use MassTransit + RabbitMQ with .Net Core 3.1 - https://feyyazacet.medium.com/use-masstransit-rabbitmq-with-net-core-3-1-33c679fcb2d
3) A simple Pub/Sub Scenario with MassTransit 6.2 + RabbitMQ +.NET Core 3.1 + Elasticsearch + MSSQL - https://alikzlda.medium.com/a-simple-pub-sub-scenario-with-masstransit-6-2-rabbitmq-net-core-3-1-elasticsearch-mssql-5a65c993b2fd
4) DOCKER ДЛЯ РАЗРАБОТЧИКОВ .NET - https://www.stevejgordon.co.uk/docker-dotnet-developers-part-1
5) Развертывание приложения .NET Core в локальном кластере Kubernetes - https://levelup.gitconnected.com/deploying-net-core-application-on-local-kubernetes-cluster-d4a0473d1543
6) Серия статей о микросервисах - от нуля до героя (https://github.com/WolfgangOfner/MicroserviceDemo) - https://www.programmingwithwolfgang.com/microservice-series-from-zero-to-hero
7) Микрослужбы .NET: Архитектура контейнерных приложений .NET (Microsoft) - https://docs.microsoft.com/ru-ru/dotnet/architecture/microservices/
8) Hot Chocolate - https://chillicream.com/docs/hotchocolate/v10
  Примеры - https://github.com/ChilliCream/hotchocolate-examples
  Пример сшивания схем (Stitching) - https://github.com/ChilliCream/hotchocolate-examples/tree/master/misc/Stitching
  Сшивание с помощью Redis (Federation with Redis) - https://github.com/ChilliCream/hotchocolate/blob/main/website/src/docs/hotchocolate/distributed-schema/schema-federations.md
  Обучение по Hot Chocolate - https://github.com/ChilliCream/graphql-workshop
  
9) Hot Chocolate GraphQL Аутентификация JWT (JSON Web Token) - Создание токена доступа пользователя для входа в систему - https://www.learmoreseekmore.com/2021/02/part-1-hotchocolate-graphql-jwt-authentiation-generate-user-access-token.html     https://www.learmoreseekmore.com/2021/03/part1-hotchocolate-graphql-custom-authentication-series-using-pure-code-first-technique-user-registration.html
10) Apollo документация - https://www.apollographql.com/docs/
11) Создайте микросервис с использованием Asp Net Core 5 и Docker - https://dotnetdetail.net/build-a-microservice-using-asp-net-core-5-and-docker/
12) Установка и базовая настройка PostgreSQL в Windows 10 - https://winitpro.ru/index.php/2019/10/25/ustanovka-nastrojka-postgresql-v-windows/
13) Веб-API ASP.Net Core с Docker Compose, PostgreSQL и EF Core - https://medium.com/front-end-weekly/net-core-web-api-with-docker-compose-postgresql-and-ef-core-21f47351224f
14) ЗАПУСК БАЗЫ ДАННЫХ POSTGRESQL В DOCKER И ПОДКЛЮЧЕНИЕ С ХОСТА (ВНЕ КОНТЕЙНЕРА) - https://reachmnadeem.wordpress.com/2020/06/02/running-postgresql-database-in-docker-and-connecting-from-host-outside-container/
15) Подключение к Postgresql в Docker-контейнере извне - https://stackoverflow.com/questions/37694987/connecting-to-postgresql-in-a-docker-container-from-outside
16) Настроить SQL Server в докер-контейнере - https://www.michalbialecki.com/2020/04/23/set-up-a-sql-server-in-a-docker-container/
17) Подключение Redis из моей сети в приложении docker .net Core - https://forums.docker.com/t/connecting-redis-from-my-network-in-docker-net-core-application/92405
18) Docker doc - https://docs.docker.com/
    Networking in Compose - https://docs.docker.com/compose/networking/
19) Dapr, Tye and k8s - https://github.com/thangchung/practical-dapr     https://github.com/kimcu-on-thenet/dapr-tye-simple-microservices
20) RabbitMQ - https://www.rabbitmq.com/getstarted.html
21) Serilog - https://serilog.net/    https://habr.com/ru/post/550582/   https://stackoverflow.com/questions/66658664/why-serilog-postgresql-sink-is-not-working-through-configuration
    Serilog Tutorial - https://blog.datalust.co/serilog-tutorial/
22) CI/CD (GitLab) - https://docs.gitlab.com/ee/ci/README.html 
    Концепции CI/CD - https://docs.gitlab.com/ee/ci/introduction/index.html
24) CI/CD (GitHub) - https://www.azuredevopslabs.com/labs/vstsextend/github/
25) Аутентификация в .Net (Использование Active Directory в .NET и др.) - 
    https://docs.microsoft.com/en-us/aspnet/core/security/authentication/windowsauth?view=aspnetcore-5.0&tabs=visual-studio
    https://www.codemag.com/article/1312041/Using-Active-Directory-in-.NET
    https://auth0.com/blog/using-ldap-with-c-sharp/
    https://github.com/OneBitSoftware/Microsoft.AspNetCore.Authentication.ActiveDirectory/blob/master/README.md
    https://stackoverflow.com/questions/45805411/asp-net-core-2-0-authentication-middleware/46118528#46118528
    https://stackoverflow.com/questions/49682644/asp-net-core-2-0-ldap-active-directory-authentication
    https://habr.com/ru/company/avanpost/blog/489852/
26) Camunda - https://camunda.com/  (https://camundarus.ru/)
    https://altkomsoftware.pl/en/blog/camunda-net-core-friends-foes/
    https://habr.com/ru/post/510490/
    https://github.com/berndruecker/camunda-csharp-showcase
    https://docs.camunda.io/docs/guides/setting-up-development-project/
    https://camunda.com/best-practices/_book/
    https://tproger.ru/articles/modelirovanie-biznes-processov-praktika-ispolzovanija-camunda-bpm-v-java-razrabotke/

