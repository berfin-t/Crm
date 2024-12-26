using Crm.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Crm.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CrmController : AbpControllerBase
{
    protected CrmController()
    {
        LocalizationResource = typeof(CrmResource);
    }
}
