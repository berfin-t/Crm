using Volo.Abp.Modularity;

namespace Crm;

[DependsOn(
    typeof(CrmApplicationModule),
    typeof(CrmDomainTestModule)
)]
public class CrmApplicationTestModule : AbpModule
{

}
