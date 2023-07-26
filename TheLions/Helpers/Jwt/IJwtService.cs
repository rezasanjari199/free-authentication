using TheLions.Models.User;

namespace TheLions.Helpers.Jwt
{
    public interface  IJwtService
    {
         string GenerateToken(User user);

    }
}
