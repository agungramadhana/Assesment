using AssesmentUser.Application;
using AssesmentUser.Application.Features;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentUser.Infrastructure.Authentications
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOption;

        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _jwtOption = options.Value;
        }

        public async Task<string> GenerateToken(UserModel user)
        {
            var claims = new Claim[]
            {
                new(ClaimConstant.IdUser, user.IdUser.ToString()),
                new(ClaimConstant.UserName, user.UserName!),
                new(ClaimConstant.Email, user.Email!),
                new(ClaimConstant.PhoneNumber, user.PhoneNumber!),
                new(ClaimConstant.Role, user.Role!)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SecretKey!)),
                SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                _jwtOption.Issuer,
                _jwtOption.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(7),
                credentials
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithm, KeySize);

            return String.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string userPassword, string password)
        {
            var element = userPassword.Split(Delimiter);
            var salt = Convert.FromBase64String(element[0]);
            var hash = Convert.FromBase64String(element[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithm, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
