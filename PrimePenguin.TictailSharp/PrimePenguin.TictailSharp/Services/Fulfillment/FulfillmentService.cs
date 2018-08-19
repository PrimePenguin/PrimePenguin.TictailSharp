using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Entities;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Fulfillment
{
    /// <summary>
    ///     A service for manipulating Ticatil fulfillments.
    /// </summary>
    public class FulfillmentService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="FulfillmentService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public FulfillmentService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Retrieves the <see cref="TictailFulfillment" /> with the given id.
        /// </summary>
        /// <param name="orderId">The order id to which the fulfillments belong.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="TictailFulfillment" />.</returns>
        public virtual async Task<Entities.TictailFulfillment> GetAsync(long orderId, string fields = null)
        {
            var req = PrepareRequest($"/orders/{orderId}");

            if (!string.IsNullOrEmpty(fields)) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.TictailFulfillment>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Creates a new <see cref="TictailFulfillment" /> on the order.
        /// </summary>
        /// <param name="orderId">The order id to which the fulfillments belong.</param>
        /// <param name="fulfillment">A new <see cref="TictailFulfillment" />. Id should be set to null.</param>
        /// <returns>The new <see cref="TictailFulfillment" />.</returns>
        public virtual async Task<Entities.TictailFulfillment> CreateAsync(string orderId, Entities.TictailFulfillment fulfillment)
        {
            var req = PrepareRequest($"orders/{orderId}/fulfill");
            var body = fulfillment.ToDictionary();

            var content = new JsonContent(new
            {
                fulfillment = body
            });

            return await ExecuteRequestAsync<Entities.TictailFulfillment>(req, HttpMethod.Post, content);
        }
    }
}