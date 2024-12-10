using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Syrna.PrivateMessaging.EntityFrameworkCore
{
    public class PrivateMessagingModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public PrivateMessagingModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}