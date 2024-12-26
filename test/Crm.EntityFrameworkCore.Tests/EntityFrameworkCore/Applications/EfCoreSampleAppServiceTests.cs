using Crm.Samples;
using Xunit;

namespace Crm.EntityFrameworkCore.Applications;

[Collection(CrmTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CrmEntityFrameworkCoreTestModule>
{

}
