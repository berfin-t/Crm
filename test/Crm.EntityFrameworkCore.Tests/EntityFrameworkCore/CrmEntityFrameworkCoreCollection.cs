using Xunit;

namespace Crm.EntityFrameworkCore;

[CollectionDefinition(CrmTestConsts.CollectionDefinitionName)]
public class CrmEntityFrameworkCoreCollection : ICollectionFixture<CrmEntityFrameworkCoreFixture>
{

}
