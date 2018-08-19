using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Carrier
{
    /// <summary>
    ///     A service for Fetching Tictail customers.
    /// </summary>
    public class CarrierService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CarrierService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public CarrierService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Gets a list of up to 100 of the shop's customers.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Entities.TictailCarrier>> ListAsync()
        {
            var req = PrepareRequest("carriers");
            return await ExecuteRequestAsync<List<Entities.TictailCarrier>>(req, HttpMethod.Get, new JsonContent(null));
        }
    }
}