using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Filters;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Category
{
    /// <summary>
    ///     A service for Fetching Tictail customers.
    /// </summary>
    public class CategoryService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CategoryService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public CategoryService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Gets a list of up to 100 of the shop's customers.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Entities.TictailCustomer>> ListAsync(string storeId, ListFilter filter = null)
        {
            var req = PrepareRequest($"stores/{storeId}/categories");

            if (filter != null) req.QueryParams.AddRange(filter.ToParameters());

            return await ExecuteRequestAsync<List<Entities.TictailCustomer>>(req, HttpMethod.Get, new JsonContent(null));
        }

        /// <summary>
        ///     Create Category for stores
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public virtual async Task<Entities.TictailCategory> CreateAsync(string storeId, Entities.TictailCategory category)
        {
            var req = PrepareRequest($"stores/{storeId}/categories");
            var content = new JsonContent(new
            {
                category
            });

            return await ExecuteRequestAsync<Entities.TictailCategory>(req, HttpMethod.Post, content);
        }

        /// <summary>
        ///     Retrieves the <see cref="Category" /> with the given id.
        /// </summary>
        /// <param name="categoryId">The id of the customer to retrieve.</param>
        /// <param name="storeId"></param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Category" />.</returns>
        public virtual async Task<Entities.TictailCategory> GetAsync(string categoryId, string storeId, string fields = null)
        {
            var req = PrepareRequest($"stores/{storeId}/categories/{categoryId}");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.TictailCategory>(req, HttpMethod.Get, new JsonContent(null));
        }

        /// <summary>
        ///     Rename a Category
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public virtual async Task<Entities.TictailCategory> UpdateAsync(string storeId, string categoryId,
            Entities.TictailCategory category)
        {
            var req = PrepareRequest($"stores/{storeId}/categories/{categoryId}");
            var body = category.Title.ToDictionary();

            var content = new JsonContent(new
            {
                title = body
            });

            return await ExecuteRequestAsync<Entities.TictailCategory>(req, HttpMethod.Put, content);
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