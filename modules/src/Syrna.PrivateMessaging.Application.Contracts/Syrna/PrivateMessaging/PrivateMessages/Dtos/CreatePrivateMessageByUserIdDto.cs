﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Syrna.PrivateMessaging.PrivateMessages.Dtos
{
    public class CreatePrivateMessageByUserIdDto : ExtensibleObject
    {
        [Required]
        [DisplayName("PrivateMessageToUserId")]
        public Guid ToUserId { get; set; }

        [Required]
        [DynamicMaxLength(typeof(PrivateMessageConsts), nameof(PrivateMessageConsts.TitleMaxLength))]
        [DisplayName("PrivateMessageTitle")]
        public string Title { get; set; }

        [DynamicMaxLength(typeof(PrivateMessageConsts), nameof(PrivateMessageConsts.ContentMaxLength))]
        [DisplayName("PrivateMessageContent")]
        public string Content { get; set; }
    }
}
