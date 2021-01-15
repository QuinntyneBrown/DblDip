using BuildingBlocks.Core;
using DblDip.Core;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DblDip.Domain.Extensions
{
    public static class TokenProviderExtensions
    {
        public static string Get(this ITokenProvider tokenProvider, User user, IEnumerable<Role> roles = null, Guid? accountId = null, Guid? profileId = null)
        {
            roles = roles == null ? new List<Role>() : roles;

            var claims = roles.Select(x => new Claim(Core.Constants.ClaimTypes.Role, x.Name)).ToList();

            if(accountId.HasValue)
            {
                claims.Add(new Claim(Constants.ClaimTypes.UserId, $"{accountId}"));
            }

            if (profileId.HasValue)
            {
                claims.Add(new Claim(Constants.ClaimTypes.UserId, $"{profileId}"));
            }

            return tokenProvider.Get(user.Username);
        }
    }
}
