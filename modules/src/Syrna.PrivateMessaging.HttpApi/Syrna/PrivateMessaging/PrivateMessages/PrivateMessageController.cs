using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Syrna.PrivateMessaging.PrivateMessages
{
    [RemoteService(Name = PrivateMessagingRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/private-messaging/private-message")]
    public class PrivateMessageController : PrivateMessagingController, IPrivateMessageAppService
    {
        private readonly IPrivateMessageAppService _service;

        public PrivateMessageController(IPrivateMessageAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<PrivateMessageDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<PrivateMessageDto>> GetListAsync(GetPrivateMessageListInput input)
        {
            return _service.GetListAsync(input);
        }

        [HttpGet]
        [Route("unread")]
        public Task<PagedResultDto<PrivateMessageDto>> GetListUnreadAsync(GetPrivateMessageListInput input)
        {
            return _service.GetListUnreadAsync(input);
        }

        [HttpGet]
        [Route("sent")]
        public Task<PagedResultDto<PrivateMessageDto>> GetListSentAsync(GetPrivateMessageListInput input)
        {
            return _service.GetListSentAsync(input);
        }

        [HttpDelete]
        public Task DeleteManyAsync(IEnumerable<Guid> ids)
        {
            return _service.DeleteManyAsync(ids);
        }

        [HttpPost]
        [Route("set-read")]
        public Task SetReadAsync(IEnumerable<Guid> ids)
        {
            return _service.SetReadAsync(ids);
        }

        [HttpPost]
        public Task<PrivateMessageDto> CreateAsync(CreatePrivateMessageDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPost]
        [Route("by-user-id")]
        public Task<PrivateMessageDto> CreateByUserIdAsync(CreatePrivateMessageByUserIdDto input)
        {
            return _service.CreateByUserIdAsync(input);
        }

        public Task<PrivateMessageDto> UpdateAsync(Guid id, UpdatePrivateMessageDto input)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}