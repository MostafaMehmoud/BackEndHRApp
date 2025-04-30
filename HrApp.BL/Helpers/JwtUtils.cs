
using HrApp.BL.Interfaces.Helpers;
using HrApp.DAL.Configratins;
using HrApp.DAL.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HrApp.API.Helpers;

public class JwtUtils : IJwtUtils
{
    private readonly AppSettingsConfig _appSettings;

    public JwtUtils( IOptions<AppSettingsConfig> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateJwtToken(GetUserByUsernameOrEmpIdOutputDto user)
    {
        // generate token that is valid for 15 minutes
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { 
                new Claim("empId", user.EmployeeId),
                new Claim("isAdmin", user.IsAdmin.ToString() )}),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public Tuple<int?,bool> ValidateJwtToken(string token)
    {
        if (token == null)
            return new(null,false);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var employeeId = int.Parse(jwtToken.Claims.First(x => x.Type == "empId").Value);
            var employeeIsAdmin = bool.Parse(jwtToken.Claims.First(x => x.Type == "isAdmin").Value);

            // return employee id from JWT token if validation successful
            return new( employeeId, employeeIsAdmin);
        }
        catch
        {
            // return null if validation fails
            return new(null, false);
        }
    }


}