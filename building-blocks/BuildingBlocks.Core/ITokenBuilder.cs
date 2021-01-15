using System.Security.Claims;

namespace BuildingBlocks.Core
{
    public interface ITokenBuilder
    {
        TokenBuilder AddOrUpdateClaim(Claim claim);
        TokenBuilder AddUsername(string username);
        string Build();
        TokenBuilder FromClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
        TokenBuilder RemoveClaim(Claim claim);
    }
}