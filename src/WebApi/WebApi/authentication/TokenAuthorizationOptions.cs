using Microsoft.AspNetCore.Authentication;

namespace WebApi
{
    public class TokenAuthorizationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "Token Authorization";
        public string Scheme => DefaultScheme;
        public string TokenApiUrl { get; set; }
    }
}