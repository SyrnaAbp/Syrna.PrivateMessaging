using AutoMapper;
using Syrna.PrivateMessaging.Blazor.Pages.PrivateMessaging.InfoModels;
using Syrna.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Volo.Abp.AutoMapper;

namespace Syrna.PrivateMessaging.Blazor
{
    public class PrivateMessagingBlazorAutoMapperProfile : Profile
    {
        public PrivateMessagingBlazorAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<PrivateMessageDto, CreatePrivateMessageDto>().Ignore(x => x.ExtraProperties);
            CreateMap<PrivateMessageDto, DetailsPrivateMessageViewModel>().Ignore(x => x.ExtraProperties);
            CreateMap<CreatePrivateMessageViewModel, CreatePrivateMessageDto>().Ignore(x => x.ExtraProperties);
            CreateMap<PrivateMessageDto, PrivateMessageViewModel>().Ignore(x => x.ExtraProperties);
        }
    }
}