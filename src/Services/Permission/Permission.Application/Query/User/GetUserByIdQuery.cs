using System;

namespace Permission.Application.Query.User
{
    public class GetUserByIdQuery : QueryBase<Infrastructure.Database.Query.Model.User>
    {
        public Guid Id { get; set; }
    }
}