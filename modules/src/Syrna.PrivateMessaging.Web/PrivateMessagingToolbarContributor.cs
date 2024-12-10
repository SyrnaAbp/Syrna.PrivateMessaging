using System.Threading.Tasks;
using Syrna.PrivateMessaging.Authorization;
using Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.Components.PmNotification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Syrna.PrivateMessaging.Web
{
    public class PrivateMessagingToolbarContributor : IToolbarContributor
    {
        public virtual async Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name != StandardToolbars.Main)
            {
                return;
            }

            if (await context.IsGrantedAsync(
                PrivateMessagingPermissions.PrivateMessageNotifications.Default))
            {
                context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(PmNotificationViewComponent)));
            }
        }
    }
}