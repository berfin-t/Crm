using Crm.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Crm.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CrmEntityFrameworkCoreModule),
    typeof(CrmApplicationContractsModule)
    )]
public class CrmDbMigratorModule : AbpModule
{
}
