using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Filters;

namespace PrimePenguin.TictailSharp.Services.Event
{
    /// <summary>
    ///     A service for getting Tictail Events
    /// </summary>
    public class EventService : TictailService
    {
        /// <summary>
        ///     Creates a new instance of <see cref="EventService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public EventService(string myTictailUrl, string shopAccessToken) : base(myTictailUrl, shopAccessToken)
        {
        }

        /// <summary>
        ///     Retrieves the <see cref="Event" /> with the given id.
        /// </summary>
        /// <param name="eventId">The id of the event to retrieve.</param>
        /// <param name="appId">ID for application owning the hook.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <param name="hookId">ID for hook owning the event.</param>
        /// <returns>The <see cref="Event" />.</returns>
        public virtual async Task<Entities.TictailEvent> GetAsync(string eventId, string hookId, string appId,
            string fields = null)
        {
            var req = PrepareRequest($"apps/{appId}/hooks/{hookId}/events/{eventId}");

            if (string.IsNullOrEmpty(fields) == false) req.QueryParams.Add("fields", fields);

            return await ExecuteRequestAsync<Entities.TictailEvent>(req, HttpMethod.Get);
        }

        /// <summary>
        ///     Returns a list of events.
        /// </summary>
        /// <param name="appId">ID for application owning the hook.</param>
        /// <param name="hookId">ID for hook owning the event.</param>
        /// <param name="options">Options for filtering the result.</param>
        public virtual async Task<IEnumerable<Entities.TictailEvent>> ListAsync(string hookId, string appId,
            EventListFilter options = null)
        {
            var req = PrepareRequest($"apps/{appId}/hooks/{hookId}/events");

            //Add optional parameters to request
            if (options != null) req.QueryParams.AddRange(options.ToParameters());

            return await ExecuteRequestAsync<List<Entities.TictailEvent>>(req, HttpMethod.Get);
        }
    }
}