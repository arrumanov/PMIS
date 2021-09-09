using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    //[GraphQLType(typeof(NonNullType<ListType<NonNullType<IdType>>>))]
    public record AddProjectInput(string Name, string Description,
        [GraphQLType(typeof(IdType))]  Guid CategoryId,
        [GraphQLType (typeof(IdType))]  Guid TypeId,
        [GraphQLType(typeof(IdType))]  Guid ResponsibleDepartmentId,
        [GraphQLType(typeof(IdType))] Guid InitiatorId,
        [GraphQLType(typeof(IdType))] Guid CuratorId,
        [GraphQLType(typeof(IdType))] Guid ManagerId,
        [GraphQLType(typeof(ListType<IdType>))] List<Guid> DepartmentIds,
        [GraphQLType(typeof(ListType<IdType>))] List<Guid> ContragentIds,
        [GraphQLType(typeof(ListType<IdType>))] List<Guid> ProductIds);
}