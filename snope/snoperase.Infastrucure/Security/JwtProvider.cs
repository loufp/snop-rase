using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using snoperase.Application.Interface;
using snoperase.Domain.Entites;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace snoperase.Application.Security;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration) => _configuration = configuration;

    public string GenerateJwt(User user)
    {
        // 1) Ключ для подписи. HS256 требует >= 32 байт, поэтому в конфиге строка >= 32 символов.
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        // 2) Алгоритм подписи (HMAC + SHA256).
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3) Полезная нагрузка (claims) — то, что «зашито» внутрь токена.
        //    ВАЖНО: имена claims запоминаем — по ним потом будем читать данные в /me.
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),  // идентификатор юзера
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("username", user.Username)
        };
        // 4) Сам токен: кто выдал (issuer), для кого (audience), claims, срок жизни, подпись.
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),   // токен живёт 1 час
            signingCredentials: creds);

        // 5) Превращаем объект в строку вида "eyJhbGciOi..." — её и отдаём клиенту.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}