using Crm.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Crm.Blazor;

public abstract class CrmComponentBase : AbpComponentBase
{
    protected CrmComponentBase()
    {
        LocalizationResource = typeof(CrmResource);
    }
}
