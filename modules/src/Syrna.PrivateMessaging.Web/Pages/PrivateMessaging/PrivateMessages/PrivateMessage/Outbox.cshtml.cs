using System.Threading.Tasks;

namespace Syrna.PrivateMessaging.Web.Pages.PrivateMessaging.PrivateMessages.PrivateMessage
{
    public class OutboxModel : PrivateMessagingPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}