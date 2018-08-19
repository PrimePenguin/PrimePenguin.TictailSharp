using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public abstract class TictailObject
    {
        /// <summary>
        ///     The object's unique id.
        /// </summary>
        /// <remarks>
        ///     Some object ids are longer than the max int32 value. Using long instead.
        ///     Marked as nullable due to issues I've run into when trying to create a resource. If Id is present when creating,
        ///     Tictail will try to find that resource. By default it's set to 0 when not null, so the resource won't exist and
        ///     Tictail will return a 404 Not Found. This is most obvious when creating a customer with a <see cref="TictailAddress" />
        ///     and the <see cref="TictailAddress" /> Id set to 0.
        /// </remarks>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}