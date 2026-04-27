using Microsoft.AspNetCore.Identity;

namespace Platform.API.Models;

public class User : IdentityUser
{
    public UserProfile? Profile { get; set; }
}
