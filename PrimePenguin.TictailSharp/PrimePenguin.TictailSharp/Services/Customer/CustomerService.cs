using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Filters;
using PrimePenguin.TictailSharp.Infrastructure;
using PrimePenguin.TictailSharp.Services.Carrier;

namespace PrimePenguin.TictailSharp.Services.Customer
{
    /// <summary>
    ///     A service for Fetching Tictail customers.
    /// </summary>
    public class CustomerService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CarrierService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public CustomerService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Gets a list of up to 100 of the shop's customers.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Entities.TictailCustomer>> ListAsync(string storeId, ListFilter filter = null)
        {
            var req = PrepareRequest($"stores/{storeId}/customers");

            if (filter != null) req.QueryParams.AddRange(filter.ToParameters());

            return await ExecuteRequestAsync<List<Entities.TictailCustomer>>(req, HttpMethod.Get, new JsonContent(null));
        }

        /// <summary>
        ///     Retrieves the <see cref="Customer" /> with the given id.
        /// </summary>
        /// <param name="customerId">The id of the customer to retrieve.</param>
        /// <param name="storeId"></param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Customer" />.</returns>
        public virtual async Task<Entities.TictailCustomer> GetAsync(long customerId, long storeId, string fields = null)
        {
            var req = PrepareRequest($"stores/{storeId}/customers/{customerId}");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.TictailCustomer>(req, HttpMethod.Get);
        }

        public virtual async Task<int> CountAsync(string storeId)
        {
            var req = PrepareRequest($"stores/{storeId}/customers");
            return await ExecuteRequestAsync<int>(req, HttpMethod.Head);
        }
    }
}