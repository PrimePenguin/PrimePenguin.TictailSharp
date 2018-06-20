namespace PrimePenguin.TictailSharp.Services.Authorization
{
    public class AuthorizationResult
    {
        internal AuthorizationResult(string accessToken, string[] grantedScopes)
        {
            AccessToken = accessToken;
            GrantedScopes = grantedScopes;
        }

        public string AccessToken { get; }
        public string[] GrantedScopes { get; }
    }
}