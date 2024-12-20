﻿using System;
using Volo.Abp.Application.Dtos;

namespace Syrna.PrivateMessaging.Users.Dtos
{
    public class PmUserDto : ExtensibleEntityDto<Guid>
    {
        public string UserName { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }
    }
}