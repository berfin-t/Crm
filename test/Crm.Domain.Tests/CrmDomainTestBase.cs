using Volo.Abp.Modularity;

namespace Crm;

/* Inherit from this class for your domain layer tests. */
public abstract class CrmDomainTestBase<TStartupModule> : CrmTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
