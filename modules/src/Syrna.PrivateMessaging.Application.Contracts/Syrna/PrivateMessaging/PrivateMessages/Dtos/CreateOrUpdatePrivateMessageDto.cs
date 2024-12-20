using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Syrna.PrivateMessaging.PrivateMessages.Dtos
{
    public class CreateOrUpdatePrivateMessageDto : ExtensibleObject
    {
        [Required]
        [DisplayName("PrivateMessageToUserName")]
        public string ToUserName { get; set; }

        [Required]
        [DynamicMaxLength(typeof(PrivateMessageConsts), nameof(PrivateMessageConsts.TitleMaxLength))]
        [DisplayName("PrivateMessageTitle")]
        public string Title { get; set; }

        [DynamicMaxLength(typeof(PrivateMessageConsts), nameof(PrivateMessageConsts.ContentMaxLength))]
        [DisplayName("PrivateMessageContent")]
        public string Content { get; set; }

        protected CreateOrUpdatePrivateMessageDto()
            : base(setDefaultsForExtraProperties: false)
        {
        }
    }
}