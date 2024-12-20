﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Syrna.PrivateMessaging.Localization;
using Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.Components.PmNotification;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Syrna.PrivateMessaging.Web
{
    [DependsOn(
        typeof(PrivateMessagingApplicationContractsModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    public class PrivateMessagingWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(PrivateMessagingResource), typeof(PrivateMessagingWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(PrivateMessagingWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new PrivateMessagingToolbarContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PrivateMessagingWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<PrivateMessagingWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<PrivateMessagingWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
            
            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Configure(StandardBundles.Styles.Global, bundle => {
                        bundle.AddContributors(typeof(PmNotificationStyleBundleContributor));
                    });
                
                options
                    .ScriptBundles
                    .Configure(StandardBundles.Scripts.Global, bundle => {
                        bundle.AddContributors(typeof(PmNotificationScriptBundleContributor));
                    });
            });
        }
    }
}
