using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace SigoWeb
{
    public static class JwtTokenValidator
    {
        public static bool ValidaToken()
        {
            var token = "";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("CdWiH4fu7byjWwzgIzRa9PGHM7WdhpZr0F0_3a-F71LdGu-BSw0ZVOOLw5quQgD080b7eK1sJqg3jaRU9hwtfPyAZG9STaMPg4DdmQlmi6EUbcSjdBeKmTdGCZ8wkEltDWj1p51otfaWrdxICzCFZ6bVvjTzdWI-dQXMFCIXACP-aN1cAL1JewNbFetnAkA9c9Z3hgJLmGYQTC3LUkvVAQXxm4J_RRE7v5kWKbn0BPziJQF-_sedgHrIP0zpsHU7g1Ztch0tLwIu7NiXe3-gCrWVLhe2tUB6IkxLjD3eMEXNdUI2uy6Croa_sXyX6lT4npYJnJ1lChSZwiFqUgK_");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken is null)
                    return false;

                tokenHandler.WriteToken(validatedToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ValidaToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("CdWiH4fu7byjWwzgIzRa9PGHM7WdhpZr0F0_3a-F71LdGu-BSw0ZVOOLw5quQgD080b7eK1sJqg3jaRU9hwtfPyAZG9STaMPg4DdmQlmi6EUbcSjdBeKmTdGCZ8wkEltDWj1p51otfaWrdxICzCFZ6bVvjTzdWI-dQXMFCIXACP-aN1cAL1JewNbFetnAkA9c9Z3hgJLmGYQTC3LUkvVAQXxm4J_RRE7v5kWKbn0BPziJQF-_sedgHrIP0zpsHU7g1Ztch0tLwIu7NiXe3-gCrWVLhe2tUB6IkxLjD3eMEXNdUI2uy6Croa_sXyX6lT4npYJnJ1lChSZwiFqUgK_");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken is null)
                    return false;

                tokenHandler.WriteToken(validatedToken);
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ValidaToken(List<string> roles, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("CdWiH4fu7byjWwzgIzRa9PGHM7WdhpZr0F0_3a-F71LdGu-BSw0ZVOOLw5quQgD080b7eK1sJqg3jaRU9hwtfPyAZG9STaMPg4DdmQlmi6EUbcSjdBeKmTdGCZ8wkEltDWj1p51otfaWrdxICzCFZ6bVvjTzdWI-dQXMFCIXACP-aN1cAL1JewNbFetnAkA9c9Z3hgJLmGYQTC3LUkvVAQXxm4J_RRE7v5kWKbn0BPziJQF-_sedgHrIP0zpsHU7g1Ztch0tLwIu7NiXe3-gCrWVLhe2tUB6IkxLjD3eMEXNdUI2uy6Croa_sXyX6lT4npYJnJ1lChSZwiFqUgK_");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken is null)
                    return false;

                List<string> rolesUser = jwtToken.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).ToList();
                if (!roles.Any(x => rolesUser.Contains(x)))
                    return false;

                tokenHandler.WriteToken(validatedToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static JwtSecurityToken ObterDetalhesToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("CdWiH4fu7byjWwzgIzRa9PGHM7WdhpZr0F0_3a-F71LdGu-BSw0ZVOOLw5quQgD080b7eK1sJqg3jaRU9hwtfPyAZG9STaMPg4DdmQlmi6EUbcSjdBeKmTdGCZ8wkEltDWj1p51otfaWrdxICzCFZ6bVvjTzdWI-dQXMFCIXACP-aN1cAL1JewNbFetnAkA9c9Z3hgJLmGYQTC3LUkvVAQXxm4J_RRE7v5kWKbn0BPziJQF-_sedgHrIP0zpsHU7g1Ztch0tLwIu7NiXe3-gCrWVLhe2tUB6IkxLjD3eMEXNdUI2uy6Croa_sXyX6lT4npYJnJ1lChSZwiFqUgK_");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken is null)
                    return null;

                tokenHandler.WriteToken(validatedToken);

                return jwtToken;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
