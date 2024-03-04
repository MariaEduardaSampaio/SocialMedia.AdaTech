using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace SocialMedia.AdaTech.Filters
{
    public class NotLoggedInFilter : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotLoggedInFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("jwt"))
            {
                context.Result = new ContentResult
                {
                    Content = "Usuário já está logado.",
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ContentType = "text/plain"
                };
            }
        }
    }
}
