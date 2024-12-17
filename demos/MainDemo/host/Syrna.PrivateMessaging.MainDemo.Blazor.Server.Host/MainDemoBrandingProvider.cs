﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Syrna.PrivateMessaging.MainDemo.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class MainDemoBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "PrivateMessaging";
    }
}
