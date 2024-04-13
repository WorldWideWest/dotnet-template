using Microsoft.AspNetCore.Identity;

namespace Template.Domain.Identity.Entites;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
