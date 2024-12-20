using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlazoriseUI;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Syrna.PrivateMessaging.Blazor
{
    [DependsOn(
        typeof(PrivateMessagingApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpBlazoriseUIModule)
        )]
    public class PrivateMessagingBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new PrivateMessagingToolbarContributor());
            });

            context.Services.AddAutoMapperObjectMapper<PrivateMessagingBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PrivateMessagingBlazorAutoMapperProfile>(validate: true);
            });

            context.Services.AddAutoMapperObjectMapper<PrivateMessagingBlazorModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<PrivateMessagingBlazorModule>(validate: true);
            });
            
            //Configure<AbpNavigationOptions>(options =>
            //{
            //    options.MenuContributors.Add(new PrivateMessagingMenuContributor());
            //});

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(PrivateMessagingBlazorModule).Assembly);
            });
        }
    }
}