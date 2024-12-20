using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;

namespace Syrna.PrivateMessaging.Blazor.Pages.PrivateMessaging.InfoModels
{
    public class DetailsPrivateMessageViewModel : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
    {
        //[DisabledInput]
        [Display(Name = "PrivateMessageFromUserName")]
        public string FromUserName { get; set; }

        //[DisabledInput]
        [Display(Name = "PrivateMessageToUserName")]
        public string ToUserName { get; set; }

        //[DisabledInput]
        [Display(Name = "PrivateMessageCreationTime")]
        public DateTime CreationTime { get; set; }

        //[DisabledInput]
        [Display(Name = "PrivateMessageTitle")]
        public string Title { get; set; }

        //[DisabledInput]
        //[TextArea(Rows = 4)]
        [Display(Name = "PrivateMessageContent")]
        public string Content { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}