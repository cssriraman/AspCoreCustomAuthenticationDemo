using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebApi
{
    public class TokenAuthorizationHandler : AuthenticationHandler<TokenAuthorizationOptions>
    {
        public TokenAuthorizationHandler(IOptionsMonitor<TokenAuthorizationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Get Authorization header value
            if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Cannot read authorization header."));
            }

            // The auth key from Authorization header check against the configured ones
            //if (authorization.Any(key => Options.AuthKey.All(ak => ak != key)))
            //{
            //    return Task.FromResult(AuthenticateResult.Fail("Invalid auth key."));
            //}
            if (!IsValidToken(Options, token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
            }

            // Create authenticated user
            var identities = new List<ClaimsIdentity> { new ClaimsIdentity("Token Authorization type") };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private bool IsValidToken(TokenAuthorizationOptions options, StringValues token)
        {
            string tokenApiUrl = options.TokenApiUrl;

            //logic to validate token

            return token.Any(key => key == "r56b600e-646e-4821-b77a-244fc9196ea2");
        }
    }
}