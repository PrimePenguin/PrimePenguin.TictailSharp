using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Store
{
    public class StoreService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StoreService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public StoreService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Get a store, specified by ID.
        /// </summary>
        public virtual async Task<Entities.Store> GetAsync(string storeId)
        {
            var request = PrepareRequest($"stores/{storeId}");

            return await ExecuteRequestAsync<Entities.Store>(request, HttpMethod.Get, new JsonContent(null));
        }

        /// <summary>
        ///     Update a store, specified by ID. Requires authorization
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        public virtual async Task<Entities.Store> UpdateAsync(string storeId, Entities.Store store)
        {
            var req = PrepareRequest($"stores/{storeId}");
            var content = new JsonContent(new
            {
                store
            });

            return await ExecuteRequestAsync<Entities.Store>(req, HttpMethod.Put, content);
        }
    }
}