using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.Department.Types
{
    public class DepartmentPayloadBase : Payload
    {
        public DepartmentPayloadBase(Infrastructure.Database.Query.Model.Department department)
        {
            Department = department;
        }

        public DepartmentPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Department? Department { get; }
    }
}