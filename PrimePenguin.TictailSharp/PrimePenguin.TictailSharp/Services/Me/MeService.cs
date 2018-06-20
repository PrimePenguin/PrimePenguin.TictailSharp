using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Me
{
    /// <summary>
    ///     This resource might come in handy when you need to figure out who you are
    /// </summary>
    public class MeService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="MeService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public MeService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Get token information
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Entities.Me> GetAsync()
        {
            var req = PrepareRequest("me");
            return await ExecuteRequestAsync<Entities.Me>(req, HttpMethod.Get, new JsonContent(null));
        }
    }
}