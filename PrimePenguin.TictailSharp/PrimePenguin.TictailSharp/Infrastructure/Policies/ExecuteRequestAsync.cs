using System.Threading.Tasks;

namespace PrimePenguin.TictailSharp.Infrastructure.Policies
{
    public delegate Task<RequestResult<T>> ExecuteRequestAsync<T>(CloneableRequestMessage request);
}