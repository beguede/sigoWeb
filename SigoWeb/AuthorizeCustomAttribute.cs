using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace SigoWeb
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["token"];
            if (string.IsNullOrWhiteSpace(token))
                context.Result = new RedirectResult("/Auth/Login");

            var jwtTokenValidator = JwtTokenValidator.ObterDetalhesToken(token);

            if (jwtTokenValidator is null)
                context.Result = new RedirectResult("/Auth/Login");
        }
    }
}
