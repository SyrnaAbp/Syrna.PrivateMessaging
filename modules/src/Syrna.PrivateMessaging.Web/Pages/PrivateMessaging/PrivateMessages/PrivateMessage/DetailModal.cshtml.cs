using System;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.PrivateMessages.PrivateMessage.InfoModels;

namespace Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.PrivateMessages.PrivateMessage
{
    public class DetailModalModel : PrivateMessagingPageModel
    {
        public bool IsSystemUserMessage { get; set; }
        
        public PrivateMessageInfoModel PrivateMessage { get; set; }

        private readonly IPrivateMessageAppService _service;

        public DetailModalModel(IPrivateMessageAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync(Guid id)
        {
            PrivateMessage = ObjectMapper.Map<PrivateMessageDto, PrivateMessageInfoModel>(await _service.GetAsync(id));

            if (PrivateMessage.FromUserName.IsNullOrEmpty())
            {
                PrivateMessage.FromUserName = L["SystemUserName"];
                IsSystemUserMessage = true;
            }
        }
    }
}