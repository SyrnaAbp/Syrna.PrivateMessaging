using System;
using System.ComponentModel.DataAnnotations;
using Syrna.PrivateMessaging.PrivateMessages;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Syrna.PrivateMessaging.Blazor.Pages.PrivateMessaging.InfoModels
{
    public class CreatePrivateMessageViewModel : ExtensibleEntityDto<Guid>
    {
        [Required]
        //[Placeholder("PrivateMessageToUserNamePlaceholder")]
        [Display(Name = "PrivateMessageToUserName")]
        public string ToUserName { get; set; }

        [Required]
        [DynamicMaxLength(typeof(PrivateMessageConsts), nameof(PrivateMessageConsts.TitleMaxLength))]
        [Display(Name = "PrivateMessageTitle")]
        public string Title { get; set; }

        //[TextArea(Rows = 4)]
        [DynamicMaxLength(typeof(PrivateMessageConsts), nameof(PrivateMessageConsts.ContentMaxLength))]
        [Display(Name = "PrivateMessageContent")]
        public string Content { get; set; }
    }
}