using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Webhook
{
    /// <summary>
    ///     A service for manipulating tictail webhooks.
    /// </summary>
    public class WebhookService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="WebhookService" />.
        /// </summary>
        /// <param name="mytictailUrl">The shop's *.mytictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public WebhookService(string mytictailUrl, string shopAccessToken) : base(mytictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Gets a list of up to 100 of the shop's webhooks.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns>The list of webhooks matching the filter.</returns>
        public virtual async Task<IEnumerable<Entities.TictailWebhook>> ListAsync(string appId)
        {
            var req = PrepareRequest($"apps/{appId}/hooks");
            return await ExecuteRequestAsync<List<Entities.TictailWebhook>>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Retrieves the <see cref="Webhook" /> with the given id.
        /// </summary>
        /// <returns>The <see cref="Webhook" />.</returns>
        public virtual async Task<Entities.TictailWebhook> GetAsync(string appId, string hookId = null)
        {
            var req = PrepareRequest($"apps/{appId}/hooks/{hookId}");
            return await ExecuteRequestAsync<Entities.TictailWebhook>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Creates a new <see cref="Webhook" /> on the store.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="webhook">A new <see cref="Webhook" />. Id should be set to null.</param>
        /// <returns>The new <see cref="Webhook" />.</returns>
        public virtual async Task<Entities.TictailWebhook> CreateAsync(string appId, Entities.TictailWebhook webhook)
        {
            var req = PrepareRequest($"apps/{appId}/hooks");
            var content = new JsonContent(new
            {
                webhook
            });

            return await ExecuteRequestAsync<Entities.TictailWebhook>(req, HttpMethod.Post, content);
        }

        /// <summary>
        ///     Deletes the webhook with the given Id.
        /// </summary>
        /// <param name="webhookId">The order object's Id.</param>
        /// <param name="appId"></param>
        public virtual async Task DeleteAsync(string webhookId, string appId)
        {
            var req = PrepareRequest($"apps/{appId}/hooks/{webhookId}");

            await ExecuteRequestAsync(req, HttpMethod.Delete);
        }
    }
}