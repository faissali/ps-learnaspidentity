using Microsoft.AspNetCore.Identity;

namespace Globomantics.Areas.Identity.Data;

public class ApplicationUser: IdentityUser
{
    [PersonalData]
    public DateTime CareerStarted { get; set; }
}
