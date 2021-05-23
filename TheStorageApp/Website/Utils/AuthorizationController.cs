using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace TheStorageApp.Website.Utils
{
    public class AuthorizationController
    {
        private readonly CookieController _httpContextCookieController;
    
        public AuthorizationController(CookieController httpContextCookieController)
        {
            this._httpContextCookieController = httpContextCookieController;
        }

        public string GetUserClaim(JWTUserClaims claim)
        {
            List<Claim> userClaims = getJwtClaims();

            string returnVal = "";
            switch (claim)
            {
                case JWTUserClaims.UserName:
                    returnVal = userClaims.FirstOrDefault(s => s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                    break;
                case JWTUserClaims.UserId:
                    returnVal = userClaims.FirstOrDefault(s => s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                    break;
                default:
                    break;
            }

            return returnVal;
        }

        private List<Claim> getJwtClaims()
        {
            string jsonToken = _httpContextCookieController.Get("token");
            var userClaims = new List<Claim>();

            if (jsonToken != null)
            {
                JwtSecurityToken jwtToken = new JwtSecurityToken(jsonToken);
                userClaims = jwtToken.Claims.ToList();
            }

            return userClaims;
        }

        public bool isLoggedIn()
        {
            List<Claim> userClaims = getJwtClaims();

            bool returnval = false;

            if (userClaims.Count > 0)
                returnval = true;

            return returnval;
        }

        public bool IsInRole(string rolename)
        {
            List<Claim> userClaims = getJwtClaims();

            bool returnval = false;

            if(userClaims.FindAll(s => s.Type == "role" && s.Value.Contains(rolename.ToLower())).Count > 0)
                returnval = true;

            return returnval;
        }
    }

    public enum JWTUserClaims { UserName, UserId }
}
