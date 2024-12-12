using System;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Microsoft.AspNetCore.Mvc;
using CreatePrivateMessageInfoModel = Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.PrivateMessages.PrivateMessage.InfoModels.CreatePrivateMessageInfoModel;

namespace Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.PrivateMessages.PrivateMessage
{
    public class CreateModalModel : PrivateMessagingPageModel
    {
        [BindProperty]
        public CreatePrivateMessageInfoModel PrivateMessage { get; set; } = new();

        private readonly IPrivateMessageAppService _service;

        public CreateModalModel(IPrivateMessageAppService service)
        {
            _service = service;
        }

        public virtual Task OnGetAsync(string toUserName)
        {
            PrivateMessage.ToUserName = toUserName;
            
            return Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(
                ObjectMapper.Map<CreatePrivateMessageInfoModel, CreatePrivateMessageDto>(PrivateMessage));
            
            return NoContent();
        }
    }
}