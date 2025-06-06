using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.PrivateMessageNotifications.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Syrna.PrivateMessaging.PrivateMessageNotifications
{
    [RemoteService(Name = PrivateMessagingRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/private-messaging/private-message-notification")]
    public class PrivateMessageNotificationController : PrivateMessagingController, IPrivateMessageNotificationAppService
    {
        private readonly IPrivateMessageNotificationAppService _service;

        public PrivateMessageNotificationController(IPrivateMessageNotificationAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/widgets/pm-notification")]
        public IActionResult PmNotification()
        {
            return ViewComponent("PmNotification");
        }
        
        [HttpPost]
        [Route("count")]
        public Task<long> CountAsync()
        {
            return _service.CountAsync();
        }

        [HttpDelete]
        public Task DeleteAsync(IEnumerable<Guid> ids)
        {
            return _service.DeleteAsync(ids);
        }

        [HttpGet]
        public Task<PagedResultDto<PrivateMessageNotificationDto>> GetListAsync(PagedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}