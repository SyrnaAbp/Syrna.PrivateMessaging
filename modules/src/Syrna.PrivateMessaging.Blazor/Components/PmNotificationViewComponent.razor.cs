using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using Syrna.PrivateMessaging.Authorization;
using Syrna.PrivateMessaging.Blazor.Pages.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.Localization;
using Syrna.PrivateMessaging.PrivateMessageNotifications.Dtos;
using Syrna.PrivateMessaging.PrivateMessages;
using Volo.Abp.Application.Dtos;

namespace Syrna.PrivateMessaging.Blazor.Components;

public partial class PmNotificationViewComponent
{
    [Inject]
    protected new IStringLocalizer<PrivateMessagingResource> L { get; set; }

    [Inject]
    protected IPrivateMessageAppService PrivateMessageAppService { get; set; }

    [Parameter] public PagedResultDto<PrivateMessageNotificationDto> Messages { get; set; } = new();

    protected string Title { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await UpdateMessages();

        await TrySetPermissionsAsync().ConfigureAwait(false);
        Navigation.LocationChanged += OnLocationChanged;
        await base.OnInitializedAsync();
    }

    protected async Task UpdateMessages()
    {
        Messages = await NotificationAppService.GetListAsync(new PagedResultRequestDto());
        var count = Messages.TotalCount;
        Title = count > 0 ? L["PmNotificationDropdownTitle", count] : L["PmNotificationDropdownTitleEmpty"];
    }

    private async Task TrySetPermissionsAsync()
    {
        if (IsDisposed)
        {
            return;
        }

        await SetPermissionsAsync();
    }

    protected virtual async Task SetPermissionsAsync()
    {
        if (CreatePolicyName != null)
        {
            HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
        }
        if (DetailsPolicyName != null)
        {
            HasDetailsPermission = await AuthorizationService.IsGrantedAsync(DetailsPolicyName);
        }
    }

    private async Task NotificationShowMoreAsync()
    {
        Navigation.NavigateTo("/PrivateMessaging/PrivateMessages/PrivateMessage/Inbox");

        // result = await NotificationAppService.GetListAsync(new PagedResultRequestDto());
        // await InvokeAsync(StateHasChanged);
    }

    private async Task NotificationHideAsync(Guid id)
    {
        await NotificationAppService.DeleteAsync([id]);
        await InvokeAsync(async () =>
        {
            await UpdateMessages();
            StateHasChanged();
        });
    }

    private async Task NotificationHideTheseAsync()
    {
        var ids = Messages.Items.Select(w => w.Id).ToList();
        await NotificationAppService.DeleteAsync(ids);

        await UpdateMessages();
        await InvokeAsync(StateHasChanged);
    }

    //private async Task NavigateToAsync(string uri, string target = null)
    //{
    //    if (target == "_blank")
    //    {
    //        await JsRuntime.InvokeVoidAsync("open", uri, target);
    //    }
    //    else
    //    {
    //        Navigation.NavigateTo(uri);
    //    }
    //}

    protected virtual void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    protected async Task NewMessageSaved(string state)
    {
        await InvokeAsync(async () =>
        {
            await UpdateMessages();
            StateHasChanged();
        });
    }

    #region Details Modal
    protected DetailsModalComponent DetailsModalComponentRef;
    protected bool HasDetailsPermission { get; set; }
    private string DetailsPolicyName = PrivateMessagingPermissions.PrivateMessages.Default;
    protected virtual async Task OpenDetailsModalAsync(Guid id)
    {
        await DetailsModalComponentRef.OpenDetailsModalAsync(id);
    }
    #endregion

    #region Create Modal
    protected CreateModalComponent CreateModalComponentRef;
    protected bool HasCreatePermission { get; set; }
    private string CreatePolicyName = PrivateMessagingPermissions.PrivateMessages.Create;

    protected virtual async Task OpenCreateModalAsync()
    {
        await CreateModalComponentRef.OpenCreateModalAsync();
    }
    protected override void Dispose(bool disposing)
    {
        Navigation.LocationChanged -= OnLocationChanged;
        base.Dispose(disposing);
    }
    #endregion
}