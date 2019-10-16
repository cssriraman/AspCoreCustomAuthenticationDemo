using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public static class AuthenticationBuilderExtensions
    {
        public static void AddAuthenticationWithTokenAuthorization(this IServiceCollection services, string tokenApiUrl)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = TokenAuthorizationOptions.DefaultScheme;
                options.DefaultChallengeScheme = TokenAuthorizationOptions.DefaultScheme;
            }).AddScheme<TokenAuthorizationOptions, TokenAuthorizationHandler>(TokenAuthorizationOptions.DefaultScheme, options =>
            {
                options.TokenApiUrl = tokenApiUrl;
            });
        }

        public static void AddMvcWithAuthorization(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // All endpoints need authentication
                options.Filters.Add(new AuthorizeFilter(
                    new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build()
                    ));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}
