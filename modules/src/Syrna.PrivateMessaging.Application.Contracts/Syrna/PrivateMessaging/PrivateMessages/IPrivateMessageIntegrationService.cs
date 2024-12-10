using System.Threading.Tasks;
using Syrna.PrivateMessaging.PrivateMessages.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Syrna.PrivateMessaging.PrivateMessages;

[IntegrationService]
public interface IPrivateMessageIntegrationService : IApplicationService
{
    Task<PrivateMessageDto> CreateAsync(CreatePrivateMessageInfoModel input);
}