using System;

namespace Dogovor.Application.Query.Department
{
    public class GetDepartmentByIdQuery : QueryBase<Infrastructure.Database.Query.Model.Department>
    {
        public Guid Id { get; set; }
    }
}