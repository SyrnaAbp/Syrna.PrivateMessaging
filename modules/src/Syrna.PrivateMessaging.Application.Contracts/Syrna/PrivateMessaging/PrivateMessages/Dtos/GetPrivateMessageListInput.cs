using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Syrna.PrivateMessaging.PrivateMessages.Dtos
{
    public class GetPrivateMessageListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}