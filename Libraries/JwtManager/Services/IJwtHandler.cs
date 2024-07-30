using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtManager.Services
{
    public interface IJwtHandler
    {
        TokenValidationParameters Parameters { get; }

        Jwt Create(List<Claim> claims);
    }
}
