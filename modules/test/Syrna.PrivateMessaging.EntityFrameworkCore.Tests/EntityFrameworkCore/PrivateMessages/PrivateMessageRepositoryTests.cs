using System;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.PrivateMessages;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Syrna.PrivateMessaging.EntityFrameworkCore.PrivateMessages
{
    public class PrivateMessageRepositoryTests : PrivateMessagingEntityFrameworkCoreTestBase
    {
        private readonly IRepository<PrivateMessage, Guid> _privateMessageRepository;

        public PrivateMessageRepositoryTests()
        {
            _privateMessageRepository = GetRequiredService<IRepository<PrivateMessage, Guid>>();
        }

        [Fact]
        public virtual async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
    }
}
