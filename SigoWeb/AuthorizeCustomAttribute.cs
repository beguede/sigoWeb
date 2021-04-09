using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SigoWeb
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _roles;

        public AuthorizeAttribute(string roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            List<string> roles = _roles.Split(';').ToList();
            var token = context.HttpContext.Request.Cookies["token"];
            if (string.IsNullOrWhiteSpace(token))
                context.Result = new RedirectResult("/Auth/Login");

            var jwtTokenValidator = JwtTokenValidator.ObterDetalhesToken(token);

            if (jwtTokenValidator is null)
                context.Result = new RedirectResult("/Auth/Login");

            List<string> rolesUser = jwtTokenValidator.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).ToList();
            if (!roles.Any(x => rolesUser.Contains(x)))
                context.Result = new RedirectResult("/Auth/SemPermissao");

        }
    }
}
