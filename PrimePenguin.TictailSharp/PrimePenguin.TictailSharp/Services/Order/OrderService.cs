using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Filters;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Order
{
    /// <summary>
    ///     A service for manipulating Tictail orders.
    /// </summary>
    public class OrderService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="OrderService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public OrderService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Gets a count of all of the shop's orders.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filter">Options for filtering the count.</param>
        /// <param name="storeId"></param>
        /// <returns>The count of all orders for the shop.</returns>
        public virtual async Task<int> CountAsync(string storeId, string userId, OrderFilter filter = null)
        {
            var req = PrepareRequest("orders{?" + storeId + "," + userId + "}");

            if (filter != null) req.QueryParams.AddRange(filter.ToParameters());

            return await ExecuteRequestAsync<int>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Gets a list of up to 250 of the shop's orders.
        /// </summary>
        /// <param name="options">Options for filtering the list.</param>
        /// <returns>The list of orders matching the filter.</returns>
        public virtual async Task<IEnumerable<Entities.Order>> ListAsync(OrderFilter options = null)
        {
            var req = PrepareRequest("orders");

            if (options != null) req.QueryParams.AddRange(options.ToParameters());

            return await ExecuteRequestAsync<List<Entities.Order>>(req, HttpMethod.Get, rootElement: "orders");
        }

        /// <summary>
        ///     Gets a list of up to 250 of the customer's orders.
        /// </summary>
        /// <param name="options">Options for filtering the list.</param>
        /// <returns>The list of orders matching the filter.</returns>
        public virtual async Task<IEnumerable<Entities.Order>> ListForCustomerAsync(OrderFilter options = null)
        {
            var req = PrepareRequest("orders");

            if (options != null) req.QueryParams.AddRange(options.ToParameters());

            return await ExecuteRequestAsync<List<Entities.Order>>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Retrieves the <see cref="Order" /> with the given id.
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Order" />.</returns>
        public virtual async Task<Entities.Order> GetAsync(long orderId, string fields = null)
        {
            var req = PrepareRequest("orders/{" + orderId + "}{? expand}");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.Order>(req, HttpMethod.Get, rootElement: "order");
        }

        /// <summary>
        ///     Cancels an order.
        /// </summary>
        /// <param name="orderId">The order's id.</param>
        public virtual async Task CancelAsync(string orderId)
        {
            var req = PrepareRequest($"orders/{orderId}/cancel");

            await ExecuteRequestAsync(req, HttpMethod.Post);
        }

        /// <summary>
        ///     Refund an order.
        /// </summary>
        /// <param name="orderId">The order's id.</param>
        /// <param name="options"></param>
        /// <returns>The Refunded <see cref="Order" />.</returns>
        public virtual async Task RefundAsync(string orderId, OrderCancelOptions options = null)
        {
            var req = PrepareRequest($"orders/{orderId}/transactions");
            var content = new JsonContent(options ?? new OrderCancelOptions());

            await ExecuteRequestAsync(req, HttpMethod.Post, content);
        }
    }
}