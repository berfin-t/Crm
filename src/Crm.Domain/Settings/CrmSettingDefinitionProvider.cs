using System;
using Volo.Abp.Emailing;
using Volo.Abp.Settings;

namespace Crm.Settings;

public class CrmSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        var smtpPassword = context.GetOrNull(EmailSettingNames.Smtp.Password);
        if (smtpPassword != null)
        {
            smtpPassword.IsEncrypted = false;
        }

        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CrmSettings.MySetting1));
    }
}
