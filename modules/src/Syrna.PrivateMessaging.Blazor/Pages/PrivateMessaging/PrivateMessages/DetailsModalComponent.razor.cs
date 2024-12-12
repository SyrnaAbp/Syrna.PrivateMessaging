using System;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syrna.PrivateMessaging.Authorization;
using Syrna.PrivateMessaging.Blazor.Pages.PrivateMessaging.InfoModels;
using Syrna.PrivateMessaging.Localization;
using Syrna.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;

namespace Syrna.PrivateMessaging.Blazor.Pages.PrivateMessaging.PrivateMessages;

public partial class DetailsModalComponent
{
    [Inject]
    protected new IStringLocalizer<PrivateMessagingResource> L { get; set; }

    [Inject]
    protected IPrivateMessageAppService PrivateMessageAppService { get; set; }

    protected Modal DetailsModalRef { get; set; }

    protected DetailsPrivateMessageViewModel DetailsEntity { get; set; }

    protected bool IsSystemUserMessage { get; set; }

    protected virtual Task ClosingDetailsModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }

    [Parameter]
    public Func<string, Task> Saved { get; set; }

    public DetailsModalComponent()
    {
        LocalizationResource = typeof(PrivateMessagingResource);
    }

    protected override async Task OnInitializedAsync()
    {
        DetailsEntity = new DetailsPrivateMessageViewModel();

        await TrySetPermissionsAsync().ConfigureAwait(false);
        await base.OnInitializedAsync();
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

    protected bool HasDetailsPermission { get; set; }
    private string DetailsPolicyName = PrivateMessagingPermissions.PrivateMessages.Default;
    public virtual async Task OpenDetailsModalAsync(Guid id)
    {
        try
        {
            await CheckDetailsPolicyAsync();

            var entityDto = await PrivateMessageAppService.GetAsync(id);

            DetailsEntity = MapToDetailsEntity(entityDto);
            if (DetailsEntity.FromUserName.IsNullOrEmpty())
            {
                DetailsEntity.FromUserName = L["SystemUserName"];
                IsSystemUserMessage = true;
            }

            await InvokeAsync(async () =>
            {
                StateHasChanged();
                if (DetailsModalRef != null)
                {
                    await DetailsModalRef.Show();
                    await PrivateMessageAppService.SetReadAsync([DetailsEntity.Id]);
                }
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual DetailsPrivateMessageViewModel MapToDetailsEntity(PrivateMessageDto entityDto)
    {
        return ObjectMapper.Map<PrivateMessageDto, DetailsPrivateMessageViewModel>(entityDto);
    }

    protected virtual async Task CheckDetailsPolicyAsync()
    {
        await this.CheckPolicyAsync(this.DetailsPolicyName).ConfigureAwait(false);
    }

    protected virtual async Task CheckPolicyAsync(string policyName)
    {
        if (string.IsNullOrEmpty(policyName))
            return;
        await AuthorizationService.CheckAsync(policyName).ConfigureAwait(false);
    }

    protected async Task ReplyAsync()
    {
        await CloseDetailsModalAsync();
        await CreateModalComponentRef.OpenCreateModalAsync(DetailsEntity);
    }

    protected virtual Task CloseDetailsModalAsync()
    {
        return this.InvokeAsync(new Func<Task>(DetailsModalRef.Hide));
    }

    protected CreateModalComponent CreateModalComponentRef;
    protected bool HasCreatePermission { get; set; }
    private string CreatePolicyName = PrivateMessagingPermissions.PrivateMessages.Create;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            DetailsModalRef?.Dispose();
        }
        base.Dispose(disposing);
    }
}