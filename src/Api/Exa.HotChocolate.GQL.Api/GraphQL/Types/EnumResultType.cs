using Exa.Configure.Models.Enums;
using HotChocolate.Types;

namespace Exa.HotChocolate.GQL.Api.GraphQL.Types
{
    public class EnumResultType : EnumType<QLResultType>
    {
        protected override void Configure(IEnumTypeDescriptor<QLResultType> descriptor)
        {
            descriptor
                .Value(QLResultType.Success)
                .Name("success");

            descriptor
            .Value(QLResultType.Error)
            .Name("error");

            descriptor
            .Value(QLResultType.Empty)
            .Name("empty");

            descriptor
            .Value(QLResultType.NotFound)
            .Name("notfound");

            descriptor
            .Value(QLResultType.Exception)
            .Name("exception");

            descriptor
            .Value(QLResultType.Duplicate)
            .Name("duplicate");
        }
    }
}
