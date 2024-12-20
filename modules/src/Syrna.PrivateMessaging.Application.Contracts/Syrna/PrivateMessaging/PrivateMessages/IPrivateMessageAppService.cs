using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Syrna.PrivateMessaging.PrivateMessages
{
    public interface IPrivateMessageAppService :
        ICrudAppService<
            PrivateMessageDto,
            PrivateMessageDto,
            Guid,
            GetPrivateMessageListInput,
            CreatePrivateMessageDto,
            UpdatePrivateMessageDto>
    {
        //Task<PrivateMessageDto> GetAsync(Guid id);

        //Task<PagedResultDto<PrivateMessageDto>> GetListAsync(GetPrivateMessageListInput input);

        Task<PagedResultDto<PrivateMessageDto>> GetListUnreadAsync(GetPrivateMessageListInput input);

        Task<PagedResultDto<PrivateMessageDto>> GetListSentAsync(GetPrivateMessageListInput input);

        Task DeleteManyAsync(IEnumerable<Guid> ids);

        Task SetReadAsync(IEnumerable<Guid> ids);

        //Task<PrivateMessageDto> CreateAsync(CreatePrivateMessageDto input);

        Task<PrivateMessageDto> CreateByUserIdAsync(CreatePrivateMessageByUserIdDto input);
    }
}