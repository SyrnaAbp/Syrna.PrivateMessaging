using System.Threading.Tasks;
using Blazorise;
using System;
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Syrna.PrivateMessaging.Authorization;
using Syrna.PrivateMessaging.Localization;
using Syrna.PrivateMessaging.ObjectExtending;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Syrna.PrivateMessaging.Blazor.Pages.PrivateMessaging.PrivateMessages;

public partial class Inbox
{
    private PageToolbar Toolbar { get; } = new();

    private List<TableColumn> PrivateMessageTableColumns => TableColumns.Get<PrivateMessageViewModel>();

    public Inbox()
    {
        ObjectMapperContext = typeof(PrivateMessagingBlazorModule);
        LocalizationResource = typeof(PrivateMessagingResource);

        CreatePolicyName = PrivateMessagingPermissions.PrivateMessages.Create;
        //UpdatePolicyName = PrivateMessagingPermissions.PrivateMessages.Update;
        DeletePolicyName = PrivateMessagingPermissions.PrivateMessages.Delete;
    }

    protected override async Task GetEntitiesAsync()
    {
        try
        {
            await UpdateGetListInputAsync();
            var result = await AppService.GetListAsync(GetListInput);
            Entities = MapToListViewModel(result.Items);
            TotalCount = (int?)result.TotalCount;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
    private IReadOnlyList<PrivateMessageViewModel> MapToListViewModel(IReadOnlyList<PrivateMessageDto> dtos)
    {
        return ObjectMapper.Map<IReadOnlyList<PrivateMessageDto>, List<PrivateMessageViewModel>>(dtos);
    }
    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();
        if (DetailsPolicyName != null)
        {
            HasDetailsPermission = await AuthorizationService.IsGrantedAsync(DetailsPolicyName);
        }
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

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<PrivateMessageViewModel>()
            .AddRange(new EntityAction[]
            {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => { await OpenEditModalAsync(data.As<PrivateMessageViewModel>()); }
                    },
                    new EntityAction
                    {
                        Text = L["Read"],
                        Visible = (data) => HasDetailsPermission,
                        Clicked = async (data) => { await OpenDetailsModalAsync(data.As<PrivateMessageViewModel>().Id); }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<PrivateMessageViewModel>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<PrivateMessageViewModel>())
                    }
            });

        return base.SetEntityActionsAsync();
    }

    protected override async ValueTask SetTableColumnsAsync()
    {
        PrivateMessageTableColumns
            .AddRange(new TableColumn[]
            {
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<PrivateMessageViewModel>(),
                    },
                    new TableColumn
                    {
                        Title = L["PrivateMessageFromUserName"],
                        Sortable = true,
                        Data = nameof(PrivateMessageViewModel.FromUserName),
                        ValueConverter = data=>FormatCell(data,"FromUserName")
                    },
                    new TableColumn
                    {
                        Title = L["PrivateMessageTitle"],
                        Sortable = true,
                        Data = nameof(PrivateMessageViewModel.Title),
                        ValueConverter = data=>FormatCell(data,"Title")
                    },
                    new TableColumn
                    {
                        Title = L["PrivateMessageCreationTime"],
                        Sortable = true,
                        Data = nameof(PrivateMessageViewModel.CreationTime),
                        ValueConverter = data=>FormatCell(data,"CreationTime")
                    },
            });

        PrivateMessageTableColumns.AddRange(await GetExtensionTableColumnsAsync(PrivateMessagingModuleExtensionConsts.ModuleName,
            PrivateMessagingModuleExtensionConsts.EntityNames.PrivateMessage));

        await base.SetTableColumnsAsync();
    }

    private static string FormatCell(object data,string name)
    {
        var row = data.As<PrivateMessageViewModel>();
        var cell = row.ReadTime;
        var valueStr = name switch
        {
            "Title" => row.Title,
            "CreationTime" => row.CreationTime.ToString("F"),
            _ => row.FromUserName
        };

        return cell == null ? "<span class='bold'>" + valueStr + "</span>" : valueStr;
    }

    protected override string GetDeleteConfirmationMessage(PrivateMessageViewModel entity)
    {
        return string.Format(L["PrivateMessageDeletionConfirmationMessage"], $"{entity.FromUserName} - {entity.Title}");
    }

    protected virtual async Task GotoOutboxAsync()
    {
        await InvokeAsync(() =>
        {
            Navigation.NavigateTo("/PrivateMessaging/PrivateMessages/PrivateMessage/Outbox");
        });
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["NewPrivateMessage"],
            OpenCreateModalAsync,
            IconName.Add,
            requiredPolicyName: CreatePolicyName);

        Toolbar.AddButton(L["PrivateMessageOutbox"],
            GotoOutboxAsync,
            IconName.CommentAlt,
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}