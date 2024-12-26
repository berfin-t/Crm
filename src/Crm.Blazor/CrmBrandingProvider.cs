using Microsoft.Extensions.Localization;
using Crm.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Crm.Blazor;

[Dependency(ReplaceServices = true)]
public class CrmBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<CrmResource> _localizer;

    public CrmBrandingProvider(IStringLocalizer<CrmResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
