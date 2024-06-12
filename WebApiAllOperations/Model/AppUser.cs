using Microsoft.AspNetCore.Identity;

namespace WebApiAllOperations.Model;

public class AppUser:IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    
}