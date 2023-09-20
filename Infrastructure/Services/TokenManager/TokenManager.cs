using Application.Dtos;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _context;
        private readonly IRepositoryManager _repositoryManager;
        public TokenManager(IConfiguration configuration, IHttpContextAccessor httpContext, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _context = httpContext;
            _repositoryManager = repositoryManager;
        }

        public string GenerateRefreshToken()
        {
            //var jwtKey = _configuration["jwt:key"];
            //var jwtAudience = _configuration["jwt:audience"];
            //var jwtTokenExpire = _configuration["jwt:expireMin"];

            //DateTime issuedAt = DateTime.Now;

            ////set the time when it expires
            //var mins = int.TryParse(jwtTokenExpire, out int expiresMin) ? expiresMin : 0;
            //DateTime expires = DateTime.Now.AddMinutes(expiresMin);


            ////create a identity and add claims to the user which we want to log in
            //var claims = new Claim[]
            //{
            //    new Claim(ClaimTypes.Name, loginResponse.Data.Name ?? ""),
            //    new Claim(ClaimTypes.NameIdentifier,loginResponse.Data.UserId.ToString() ?? ""),
            //    new Claim(ClaimTypes.Email,loginResponse.Data.Email ?? ""),
            //};

            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            //var token = new JwtSecurityToken(
            //    issuer: jwtAudience,
            //    audience: jwtAudience,
            //    claims: claims,
            //    notBefore: DateTime.UtcNow,
            //    expires: expires,
            //    signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            //);

            //var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            //return jwtToken;

            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string GenerateToken(ref GenericResponse<LoginResponse> loginResponse)
        {
            var jwtKey = _configuration["jwt:key"];
            var jwtAudience = _configuration["jwt:audience"];
            var jwtTokenExpire = _configuration["jwt:expireMin"];

            DateTime issuedAt = DateTime.Now;

            //set the time when it expires
            var mins = int.TryParse(jwtTokenExpire, out int expiresMin) ? expiresMin : 0;
            DateTime expires = DateTime.Now.AddMinutes(expiresMin);


            //create a identity and add claims to the user which we want to log in
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, loginResponse.Data.Name ?? ""),
                new Claim(ClaimTypes.NameIdentifier,loginResponse.Data.UserId.ToString() ?? ""),
                new Claim(ClaimTypes.Email,loginResponse.Data.Email ?? ""),
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var token = new JwtSecurityToken(
                issuer: jwtAudience,
                audience: jwtAudience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }


    }
}
