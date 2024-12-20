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

public partial class CreateModalComponent
{
    [Inject]
    protected new IStringLocalizer<PrivateMessagingResource> L { get; set; }

    [Inject]
    protected IPrivateMessageAppService PrivateMessageAppService { get; set; }

    protected Modal CreateModalRef { get; set; }

    protected CreatePrivateMessageViewModel NewEntity { get; set; }

    protected Validations CreateValidationsRef { get; set; }

    [Parameter]
    public bool IsReply { get; set; }

    protected DetailsPrivateMessageViewModel ReplyMessage { get; set; }

    protected bool HasCreatePermission { get; set; }
    private string CreatePolicyName = PrivateMessagingPermissions.PrivateMessages.Create;

    protected virtual Task ClosingCreateModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }

    protected virtual Task CloseCreateModalAsync()
    {
        this.NewEntity = new CreatePrivateMessageViewModel();
        return this.InvokeAsync(new Func<Task>(CreateModalRef.Hide));
    }

    protected override async Task OnInitializedAsync()
    {
        NewEntity = new CreatePrivateMessageViewModel();

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
    }

    protected virtual async Task CreateEntityAsync()
    {
        try
        {
            var validate = true;
            if (CreateValidationsRef != null)
            {
                validate = await CreateValidationsRef.ValidateAll();
            }

            if (validate)
            {
                await OnCreatingEntityAsync();
                await CheckCreatePolicyAsync();
                var createInput = MapToCreateInput(NewEntity);
                await PrivateMessageAppService.CreateAsync(createInput);
                await OnCreatedEntityAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual CreatePrivateMessageDto MapToCreateInput(CreatePrivateMessageViewModel createViewModel)
    {
        return ObjectMapper.Map<CreatePrivateMessageViewModel, CreatePrivateMessageDto>(createViewModel);
    }

    public CreateModalComponent()
    {
        LocalizationResource = typeof(PrivateMessagingResource);
    }

    protected virtual async Task OnCreatedEntityAsync()
    {
        NewEntity = new CreatePrivateMessageViewModel();
        // await GetEntitiesAsync().ConfigureAwait(false);
        await InvokeAsync(new Func<Task>(CreateModalRef.Hide)).ConfigureAwait(false);
        await Notify.Success(GetCreateMessage()).ConfigureAwait(false);
        if (Saved != null)
        {
            await Saved.Invoke("Ok");
        }
    }

    [Parameter]
    public Func<string, Task> Saved { get; set; }

    protected virtual string GetCreateMessage() => (string)this.L["CreatedSuccessfully"];

    protected virtual Task OnCreatingEntityAsync() => Task.CompletedTask;

    public virtual async Task OpenCreateModalAsync(DetailsPrivateMessageViewModel replyMessage = null)
    {
        try
        {
            if (CreateValidationsRef != null)
            {
                await CreateValidationsRef.ClearAll();
            }

            await CheckCreatePolicyAsync();
            NewEntity = new CreatePrivateMessageViewModel();
            if (replyMessage != null)
            {
                NewEntity.ToUserName = replyMessage.FromUserName;
                IsReply = true;
            }

            // Mapper will not notify Blazor that binded values are changed
            // so we need to notify it manually by calling StateHasChanged
            await InvokeAsync(async () =>
            {
                StateHasChanged();
                if (CreateModalRef != null)
                {
                    await CreateModalRef.Show();
                }
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual async Task CheckCreatePolicyAsync()
    {
        await this.CheckPolicyAsync(this.CreatePolicyName).ConfigureAwait(false);
    }

    protected virtual async Task CheckPolicyAsync(string policyName)
    {
        if (string.IsNullOrEmpty(policyName))
            return;
        await AuthorizationService.CheckAsync(policyName).ConfigureAwait(false);
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            CreateModalRef?.Dispose();
        }
        base.Dispose(disposing);
    }
}