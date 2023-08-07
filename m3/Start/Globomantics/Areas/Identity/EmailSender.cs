using Microsoft.AspNetCore.Identity.UI.Services;

namespace Globomantics.Areas.Identity;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        throw new NotImplementedException();
    }
}
