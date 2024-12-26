using Volo.Abp.Modularity;

namespace Crm;

[DependsOn(
    typeof(CrmDomainModule),
    typeof(CrmTestBaseModule)
)]
public class CrmDomainTestModule : AbpModule
{

}
