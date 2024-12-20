using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.Authorization;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Syrna.PrivateMessaging.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Users;
using Microsoft.Extensions.Localization;
using Syrna.PrivateMessaging.Localization;

namespace Syrna.PrivateMessaging.PrivateMessages
{
    [Authorize]
    public class PrivateMessageAppService : ApplicationService,
        IPrivateMessageAppService
    {
        private readonly IDataFilter _dataFilter;
        private readonly IStringLocalizer<PrivateMessagingResource> _localizer;
        private readonly IExternalUserLookupServiceProvider _externalUserLookupServiceProvider;
        private readonly IPrivateMessageRepository _privateMessageRepository;
        private readonly IPrivateMessageSenderSideManager _privateMessageSenderSideManager;
        private readonly IPrivateMessageReceiverSideManager _privateMessageReceiverSideManager;

        public PrivateMessageAppService(
            IStringLocalizer<PrivateMessagingResource> localizer,
            IDataFilter dataFilter,
            IExternalUserLookupServiceProvider externalUserLookupServiceProvider,
            IPrivateMessageRepository privateMessageRepository,
            IPrivateMessageSenderSideManager privateMessageSenderSideManager,
            IPrivateMessageReceiverSideManager privateMessageReceiverSideManager)
        {
            _dataFilter = dataFilter;
            _localizer = localizer;
            _externalUserLookupServiceProvider = externalUserLookupServiceProvider;
            _privateMessageRepository = privateMessageRepository;
            _privateMessageSenderSideManager = privateMessageSenderSideManager;
            _privateMessageReceiverSideManager = privateMessageReceiverSideManager;
        }

        public virtual async Task<PrivateMessageDto> GetAsync(Guid id)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var message = await _privateMessageRepository.GetAsync(id);

                await AuthorizationService.CheckAsync(message,
                    new OperationAuthorizationRequirement
                    { Name = PrivateMessagingPermissions.PrivateMessages.Default });

                return await MapToDtoAndLoadMoreInfosAsync(message);
            }
        }

        [Authorize(PrivateMessagingPermissions.PrivateMessages.Default)]
        public virtual async Task<PagedResultDto<PrivateMessageDto>> GetListAsync(GetPrivateMessageListInput input)
        {
            var list = await _privateMessageReceiverSideManager.GetListAsync(CurrentUser.GetId(), input.SkipCount,
                input.MaxResultCount, input.Sorting);

            var count = await _privateMessageReceiverSideManager.CountAsync(CurrentUser.GetId());

            return new PagedResultDto<PrivateMessageDto>(count, await MapToDtoAndLoadMoreInfosAsync(list));
        }

        [Authorize(PrivateMessagingPermissions.PrivateMessages.Default)]
        public virtual async Task<PagedResultDto<PrivateMessageDto>> GetListUnreadAsync(GetPrivateMessageListInput input)
        {
            var list = await _privateMessageReceiverSideManager.GetListAsync(CurrentUser.GetId(), input.SkipCount,
                input.MaxResultCount, input.Sorting, true);

            var count = await _privateMessageReceiverSideManager.CountAsync(CurrentUser.GetId(), true);

            return new PagedResultDto<PrivateMessageDto>(count, await MapToDtoAndLoadMoreInfosAsync(list));
        }

        [Authorize(PrivateMessagingPermissions.PrivateMessages.Default)]
        public virtual async Task<PagedResultDto<PrivateMessageDto>> GetListSentAsync(GetPrivateMessageListInput input)
        {
            var list = await _privateMessageSenderSideManager.GetListAsync(CurrentUser.GetId(), input.SkipCount,
                input.MaxResultCount, input.Sorting);

            var count = await _privateMessageSenderSideManager.CountAsync(CurrentUser.GetId());

            return new PagedResultDto<PrivateMessageDto>(count, await MapToDtoAndLoadMoreInfosAsync(list));
        }

        public virtual async Task DeleteManyAsync(IEnumerable<Guid> ids)
        {
            var messageList = await _privateMessageRepository.GetListAsync(ids);

            foreach (var message in messageList)
            {
                await AuthorizationService.CheckAsync(message,
                    new OperationAuthorizationRequirement
                    { Name = PrivateMessagingPermissions.PrivateMessages.Delete });

                await _privateMessageReceiverSideManager.DeleteAsync(message);
            }
        }

        public virtual async Task SetReadAsync(IEnumerable<Guid> ids)
        {
            var messageList = await _privateMessageRepository.GetListAsync(ids);

            foreach (var message in messageList)
            {
                await AuthorizationService.CheckAsync(message,
                    new OperationAuthorizationRequirement
                    { Name = PrivateMessagingPermissions.PrivateMessages.SetRead });

                await _privateMessageReceiverSideManager.SetReadAsync(message);
            }
        }

        [Authorize(PrivateMessagingPermissions.PrivateMessages.Create)]
        public virtual async Task<PrivateMessageDto> CreateAsync(CreatePrivateMessageDto input)
        {
            var fromUser = await _externalUserLookupServiceProvider.FindByIdAsync(CurrentUser.GetId());
            var toUser = await _externalUserLookupServiceProvider.FindByUserNameAsync(input.ToUserName);
            if (toUser == null)
            {
                throw new UserFriendlyException(_localizer["ErrorEnterValidToUserName"]);
            }

            var message =
                await _privateMessageSenderSideManager.CreateAsync(fromUser, toUser, input.Title, input.Content);

            input.MapExtraPropertiesTo(message);

            await _privateMessageRepository.InsertAsync(message, true);

            return await MapToDtoAndLoadMoreInfosAsync(message);
        }

        [Authorize(PrivateMessagingPermissions.PrivateMessages.Create)]
        public virtual async Task<PrivateMessageDto> CreateByUserIdAsync(CreatePrivateMessageByUserIdDto input)
        {
            var fromUser = await _externalUserLookupServiceProvider.FindByIdAsync(CurrentUser.GetId());
            var toUser = await _externalUserLookupServiceProvider.FindByIdAsync(input.ToUserId);

            var message =
                await _privateMessageSenderSideManager.CreateAsync(fromUser, toUser, input.Title, input.Content);

            input.MapExtraPropertiesTo(message);

            await _privateMessageRepository.InsertAsync(message, true);

            return await MapToDtoAndLoadMoreInfosAsync(message);
        }

        protected virtual async Task<IReadOnlyList<PrivateMessageDto>> MapToDtoAndLoadMoreInfosAsync(
            IReadOnlyList<PrivateMessage> entityList)
        {
            var dtoList = ObjectMapper.Map<IReadOnlyList<PrivateMessage>, IReadOnlyList<PrivateMessageDto>>(entityList);

            var fromUserIds = dtoList.Where(dto => dto.FromUserId.HasValue).Select(dto => dto.FromUserId.Value);
            var toUserIds = dtoList.Select(dto => dto.ToUserId);

            var userIds = toUserIds.Concat(fromUserIds).Distinct().ToList();

            var userDtoDict = new Dictionary<Guid, PmUserDto>();

            foreach (var userId in userIds)
            {
                userDtoDict[userId] =
                    ObjectMapper.Map<IUserData, PmUserDto>(
                        await _externalUserLookupServiceProvider.FindByIdAsync(userId));
            }

            foreach (var dto in dtoList)
            {
                dto.ToUser = userDtoDict[dto.ToUserId];

                if (dto.FromUserId.HasValue)
                {
                    dto.FromUser = userDtoDict[dto.FromUserId.Value];
                }
            }

            return dtoList;
        }

        protected virtual async Task<PrivateMessageDto> MapToDtoAndLoadMoreInfosAsync(PrivateMessage entity)
        {
            return (await MapToDtoAndLoadMoreInfosAsync(new[] { entity })).First();
        }

        public Task<PrivateMessageDto> UpdateAsync(Guid id, UpdatePrivateMessageDto input)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            var message = await _privateMessageRepository.GetAsync(id);

            await _privateMessageReceiverSideManager.DeleteAsync(message);
        }
    }
}