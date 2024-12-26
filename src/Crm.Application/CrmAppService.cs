using System;
using System.Collections.Generic;
using System.Text;
using Crm.Localization;
using Volo.Abp.Application.Services;

namespace Crm;

/* Inherit your application services from this class.
 */
public abstract class CrmAppService : ApplicationService
{
    protected CrmAppService()
    {
        LocalizationResource = typeof(CrmResource);
    }
}
