using Syrna.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Syrna.PrivateMessaging.PrivateMessageNotifications;
using Syrna.PrivateMessaging.PrivateMessageNotifications.Dtos;
using AutoMapper;
using Syrna.PrivateMessaging.Users;
using Syrna.PrivateMessaging.Users.Dtos;
using Volo.Abp.Users;

namespace Syrna.PrivateMessaging
{
    public class PrivateMessagingApplicationAutoMapperProfile : Profile
    {
        public PrivateMessagingApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<PrivateMessage, PrivateMessageDto>();
            CreateMap<CreateOrUpdatePrivateMessageDto, PrivateMessage>(MemberList.Source);
            CreateMap<PrivateMessageNotification, PrivateMessageNotificationDto>();
            CreateMap<IUserData, PmUserDto>();
        }
    }
}
