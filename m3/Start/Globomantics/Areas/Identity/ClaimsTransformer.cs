using Globomantics.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Globomantics.Areas.Identity;

public class ClaimsTransformer : IClaimsTransformation
{
    private readonly IUserStore<ApplicationUser> _userStore;

    public ClaimsTransformer(IUserStore<ApplicationUser> userStore)
    {
        this._userStore = userStore;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var clonedPrincipal = principal.Clone();

        if (clonedPrincipal.Identity is null)
        {
            return clonedPrincipal;
        }

        var identity = (ClaimsIdentity)clonedPrincipal.Identity;

        var existingClaim = identity.Claims.FirstOrDefault(c => c.Type == GloboClaimTypes.CareerStarted);

        if (existingClaim is not null)
        {
            return clonedPrincipal;
        }

        var nameIdClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (nameIdClaim is null)
        {
            return clonedPrincipal;
        }

        var user = await _userStore.FindByIdAsync(nameIdClaim.Value, CancellationToken.None);

        if (user != null)
        {
            identity.AddClaim(new Claim(GloboClaimTypes.CareerStarted, user.CareerStarted.ToString()));
        }
       
        return clonedPrincipal;
    }
}
