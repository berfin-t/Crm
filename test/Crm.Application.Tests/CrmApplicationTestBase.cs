using Volo.Abp.Modularity;

namespace Crm;

public abstract class CrmApplicationTestBase<TStartupModule> : CrmTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
