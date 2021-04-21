using HotChocolate.Data.Filters;
using PMIS.DogovorGql.Data;

namespace PMIS.DogovorGql.Types
{
    public class ContractFilterInputType : FilterInputType<Contract>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Contract> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}