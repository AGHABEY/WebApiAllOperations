using WebApiAllOperations.Model;

namespace WebApiAllOperations.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}