using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HybirdFrameworkCore.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Common.Util
{
    public class JwtUtil
    {
        public static string BuildToken(string username)
        {
            var issuer = AppSettingsHelper.GetContent("TokenOptions", "Issuer");
            var audience = AppSettingsHelper.GetContent("TokenOptions", "Audience");
            var securityKey = AppSettingsHelper.GetContent("TokenOptions", "SecurityKey");


            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMonths(12)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name, username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMonths(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GetUsername(HttpRequest request)
        {
            var authorization = request.Headers["Authorization"].ToString();

            var arr = authorization.Split(" ");

            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(arr[1]);

            return jwtToken.Claims.Where(m => m.Type == ClaimTypes.Name).FirstOrDefault().Value;
        }
    }
}