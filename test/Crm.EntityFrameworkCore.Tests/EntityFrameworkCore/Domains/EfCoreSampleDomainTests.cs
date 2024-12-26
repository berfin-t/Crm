using Crm.Samples;
using Xunit;

namespace Crm.EntityFrameworkCore.Domains;

[Collection(CrmTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CrmEntityFrameworkCoreTestModule>
{

}
