using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrimePenguin.TictailSharp.Entities;
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
        ///     Gets a list of up to 100 of the shop's products.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Entities.TictailProduct>> ListAsync(string storeId, ProductFilter options = null)
        {
            var req = PrepareRequest($"stores/{storeId}/products");
            if (options != null) req.QueryParams.AddRange(options.ToParameters());

            return await ExecuteRequestAsync<List<Entities.TictailProduct>>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Retrieves the <see cref="Product" /> with the given id.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <param name="storeId"></param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Product" />.</returns>
        public virtual async Task<Entities.TictailProduct> GetAsync(string productId, string storeId, string fields = null)
        {
            var req = PrepareRequest($"stores/{storeId}/products/{productId}");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.TictailProduct>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Creates a new <see cref="Product" /> on the store.
        /// </summary>
        /// <param name="product">A new <see cref="Product" />. Id should be set to null.</param>
        /// <param name="storeId"></param>
        /// <returns>The new <see cref="Product" />.</returns>
        public virtual async Task<Entities.TictailProduct> CreateAsync(Entities.TictailProduct product, string storeId)
        {
            var req = PrepareRequest($"stores/{storeId}/products");
            var body = product.ToDictionary();

            var content = new JsonContent(new
            {
                product = body
            });

            return await ExecuteRequestAsync<Entities.TictailProduct>(req, HttpMethod.Post);
        }

        /// <summary>
        ///     Updates the given <see cref="Product" />.
        /// </summary>
        /// <param name="product">The <see cref="Product" /> to update.</param>
        /// <returns>The updated <see cref="Product" />.</returns>
        public virtual async Task<Entities.TictailProduct> UpdateAsync(TictailProductUpdate product)
        {
            var method = new HttpMethod("PATCH");
            var req = PrepareRequest($"stores/{product.StoreId}/products/{product.Id}");
            var json = new StringContent(JsonConvert.SerializeObject(product).ToString(), Encoding.UTF8, "application/json");
            return await ExecuteRequestAsync<TictailProduct>(req, method, json);
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