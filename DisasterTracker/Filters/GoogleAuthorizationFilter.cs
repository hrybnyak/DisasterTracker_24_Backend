using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DisasterTracker.Filters
{
    /// <summary>
    /// Custom Google Authentication authorize attribute which validates the bearer token.
    /// </summary>
    public class GoogleAuthorizeAttribute : TypeFilterAttribute
    {
        public GoogleAuthorizeAttribute() : base(typeof(GoogleAuthorizeFilter)) { }
    }

    public class GoogleAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public GoogleAuthorizeFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var headers = context.HttpContext.Request.Headers;
                if (!headers.ContainsKey("Authorization"))
                {
                    context.Result = new ForbidResult();
                }
                var authHeader = headers["Authorization"].ToString();

                if (!authHeader.StartsWith("Bearer ") && authHeader.Length > 7)
                {
                    context.Result = new ForbidResult();
                }

                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string>() { _configuration["Authentication:Google:ClientId"] }
                };

                var token = authHeader.Remove(0, 7);

                var validated = GoogleJsonWebSignature.ValidateAsync(token, settings).Result;
            }
            catch (Exception)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
