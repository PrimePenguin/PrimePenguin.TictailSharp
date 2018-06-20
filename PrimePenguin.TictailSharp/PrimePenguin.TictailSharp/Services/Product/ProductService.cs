using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Filters;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Product
{
    /// <summary>
    ///     A service for manipulating Tictail products.
    /// </summary>
    public class ProductService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="ProductService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public ProductService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Gets a list of up to 250 of the shop's products.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Entities.Product>> ListAsync(string storeId, ProductFilter options = null)
        {
            var req = PrepareRequest($"stores/{storeId}/products.json");
            if (options != null) req.QueryParams.AddRange(options.ToParameters());

            return await ExecuteRequestAsync<List<Entities.Product>>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Retrieves the <see cref="Product" /> with the given id.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <param name="storeId"></param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Product" />.</returns>
        public virtual async Task<Entities.Product> GetAsync(string productId, string storeId, string fields = null)
        {
            var req = PrepareRequest($"stores/{storeId}/products/{productId}");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.Product>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Creates a new <see cref="Product" /> on the store.
        /// </summary>
        /// <param name="product">A new <see cref="Product" />. Id should be set to null.</param>
        /// <param name="storeId"></param>
        /// <returns>The new <see cref="Product" />.</returns>
        public virtual async Task<Entities.Product> CreateAsync(Entities.Product product, string storeId)
        {
            var req = PrepareRequest($"stores/{storeId}/products");
            var body = product.ToDictionary();

            var content = new JsonContent(new
            {
                product = body
            });

            return await ExecuteRequestAsync<Entities.Product>(req, HttpMethod.Post, content);
        }

        /// <summary>
        ///     Updates the given <see cref="Product" />.
        /// </summary>
        /// <param name="productId">Id of the object being updated.</param>
        /// <param name="product">The <see cref="Product" /> to update.</param>
        /// <returns>The updated <see cref="Product" />.</returns>
        public virtual async Task<Entities.Product> UpdateAsync(string productId, Entities.Product product)
        {
            var req = PrepareRequest($"products/{productId}.json");
            var content = new JsonContent(new
            {
                product
            });

            return await ExecuteRequestAsync<Entities.Product>(req, HttpMethod.Put, content, "product");
        }

        /// <summary>
        ///     Deletes a product with the given Id.
        /// </summary>
        /// <param name="productId">The product object's Id.</param>
        /// <param name="storeId"></param>
        public virtual async Task DeleteAsync(string productId, string storeId)
        {
            var req = PrepareRequest($"stores/{storeId}/products/{productId}");

            await ExecuteRequestAsync(req, HttpMethod.Delete);
        }
    }
}