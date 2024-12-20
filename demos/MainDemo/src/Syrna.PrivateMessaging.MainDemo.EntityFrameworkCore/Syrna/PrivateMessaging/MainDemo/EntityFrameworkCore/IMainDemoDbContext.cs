using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Syrna.PrivateMessaging;

namespace Syrna.PrivateMessaging.MainDemo.EntityFrameworkCore
{
    [ConnectionStringName(PrivateMessagingDbProperties.ConnectionStringName)]
    public interface IMainDemoDbContext : IEfCoreDbContext
    {
    }
}
