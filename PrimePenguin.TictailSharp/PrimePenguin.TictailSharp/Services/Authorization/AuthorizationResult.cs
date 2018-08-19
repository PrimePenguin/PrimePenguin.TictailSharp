using PrimePenguin.TictailSharp.Entities;

namespace PrimePenguin.TictailSharp.Services.Authorization
{
    public class AuthorizationResult
    {
        internal AuthorizationResult(string accessToken, string[] grantedScopes, TictailStore tictailStore)
        {
            AccessToken = accessToken;
            GrantedScopes = grantedScopes;
            TictailStore = tictailStore;
        }

        public string AccessToken { get; }
        public string[] GrantedScopes { get; }
        public TictailStore TictailStore { get; }
    }
}