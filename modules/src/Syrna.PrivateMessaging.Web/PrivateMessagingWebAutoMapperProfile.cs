using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using AutoMapper;
using Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.PrivateMessages.PrivateMessage.InfoModels;
using Volo.Abp.AutoMapper;

namespace Syrna.PrivateMessaging.Web
{
    public class PrivateMessagingWebAutoMapperProfile : Profile
    {
        public PrivateMessagingWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<PrivateMessageDto, CreateOrUpdatePrivateMessageDto>();
            CreateMap<PrivateMessageDto, PrivateMessageInfoModel>();
            CreateMap<CreatePrivateMessageInfoModel, CreateOrUpdatePrivateMessageDto>().Ignore(x => x.ExtraProperties);
        }
    }
}
