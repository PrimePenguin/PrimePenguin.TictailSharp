using System.Threading.Tasks;

namespace PrimePenguin.TictailSharp.Infrastructure.Policies
{
    /// <summary>
    ///     Used to specify centralized logic that should run when executing Tictail requests.
    ///     It is most useful to implement retry logic, but it can also be used for other concerns (i.e. tracing)
    /// </summary>
    public interface IRequestExecutionPolicy
    {
        /// <param name="requestMessage"></param>
        /// <param name="executeRequestAsync">A delegate that executes the request you pass to it.</param>
        Task<T> Run<T>(CloneableRequestMessage requestMessage, ExecuteRequestAsync<T> executeRequestAsync);
    }
}