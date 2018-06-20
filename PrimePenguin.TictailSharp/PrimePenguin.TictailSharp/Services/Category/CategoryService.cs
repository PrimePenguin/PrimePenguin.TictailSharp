using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Filters;
using PrimePenguin.TictailSharp.Infrastructure;
using PrimePenguin.TictailSharp.Services.Customer;

namespace PrimePenguin.TictailSharp.Services.Category
{
    /// <summary>
    ///     A service for Fetching Tictail customers.
    /// </summary>
    public class MeService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CarrierService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public MeService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Gets a list of up to 250 of the shop's customers.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Entities.Customer>> ListAsync(string storeId, ListFilter filter = null)
        {
            var req = PrepareRequest($"stores/{storeId}/categories");

            if (filter != null) req.QueryParams.AddRange(filter.ToParameters());

            return await ExecuteRequestAsync<List<Entities.Customer>>(req, HttpMethod.Get, new JsonContent(null));
        }

        /// <summary>
        ///     Create Category for stores
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public virtual async Task<Entities.Category> CreateAsync(string storeId, Entities.Category category)
        {
            var req = PrepareRequest($"stores/{storeId}/categories");
            var content = new JsonContent(new
            {
                category
            });

            return await ExecuteRequestAsync<Entities.Category>(req, HttpMethod.Post, content);
        }

        /// <summary>
        ///     Retrieves the <see cref="Category" /> with the given id.
        /// </summary>
        /// <param name="categoryId">The id of the customer to retrieve.</param>
        /// <param name="storeId"></param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Category" />.</returns>
        public virtual async Task<Entities.Category> GetAsync(string categoryId, string storeId, string fields = null)
        {
            var req = PrepareRequest($"stores/{storeId}/categories/{categoryId}");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.Category>(req, HttpMethod.Get, new JsonContent(null));
        }

        /// <summary>
        ///     Rename a Category
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public virtual async Task<Entities.Category> UpdateAsync(string storeId, string categoryId,
            Entities.Category category)
        {
            var req = PrepareRequest($"stores/{storeId}/categories/{categoryId}");
            var body = category.Title.ToDictionary();

            var content = new JsonContent(new
            {
                title = body
            });

            return await ExecuteRequestAsync<Entities.Category>(req, HttpMethod.Put, content);
        }


        /// <summary>
        ///     Delete a Category
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(string storeId, string categoryId)
        {
            var req = PrepareRequest($"stores/{storeId}/categories/{categoryId}");

            await ExecuteRequestAsync(req, HttpMethod.Delete);
        }
    }
}