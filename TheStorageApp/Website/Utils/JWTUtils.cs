using System.IdentityModel.Tokens.Jwt;

namespace TheStorageApp.Website.Utils
{
    public static  class JWTUtils
    {
        public static JwtSecurityToken DecodeJWTToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(token) as JwtSecurityToken;
        }
    }
}
