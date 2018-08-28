using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Enums;
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
        ///     Gets a list of up to 100 of the shop's orders.
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="options">Options for filtering the list.</param>
        /// <returns>The list of orders matching the filter.</returns>
        public virtual async Task<IEnumerable<Entities.TictailOrder>> ListAsync(IEnumerable<TictailOrderScope> scopes, OrderFilter options = null)
        {
            var req = PrepareRequest("orders");
            if (options == null) return await ExecuteRequestAsync<List<Entities.TictailOrder>>(req, HttpMethod.Get);
            req.QueryParams.AddRange(options.ToParameters());
            req.QueryParams.Add("expand", string.Join(",", scopes.Select(s => s.ToSerializedString())));
            return await ExecuteRequestAsync<List<Entities.TictailOrder>>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Retrieves the <see cref="Order" /> with the given id.
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve.</param>
        /// <param name="scopes"></param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Order" />.</returns>
        public virtual async Task<Entities.TictailOrder> GetAsync(string orderId, IEnumerable<TictailOrderScope> scopes, string fields = null)
        {
            var req = PrepareRequest("orders/" + orderId + "");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);
            req.QueryParams.Add("expand", string.Join(",", scopes.Select(s => s.ToSerializedString())));
            return await ExecuteRequestAsync<Entities.TictailOrder>(req, HttpMethod.Get, rootElement: "order");
        }

        /// <summary>
        ///     Cancels an order.
        /// </summary>
        /// <param name="orderId">The order's id.</param>
        public virtual async Task<Entities.TictailOrder> CancelAsync(string orderId)
        {
            var req = PrepareRequest($"orders/{orderId}/cancel");

           return await ExecuteRequestAsync<Entities.TictailOrder>(req, HttpMethod.Post);
        }

        /// <summary>
        ///     Refund an order.
        /// </summary>
        /// <param name="orderId">The order's id.</param>
        /// <param name="options"></param>
        /// <returns>The Refunded <see cref="Order" />.</returns>
        public virtual async Task<Entities.TictailRefund> RefundAsync(string orderId, OrderCancelOptions options = null)
        {
            var req = PrepareRequest($"orders/{orderId}/transactions");
            var content = new JsonContent(options ?? new OrderCancelOptions());

            return await ExecuteRequestAsync<Entities.TictailRefund>(req, HttpMethod.Post, content);
        }
        /// <summary>
        ///     Count an order.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(OrderFilter options = null)
        {
            var req = PrepareRequest("orders");
            if (options != null)
            req.QueryParams.AddRange(options.ToParameters());

            return await ExecuteRequestAsync<int>(req, HttpMethod.Head);
        }
    }
}