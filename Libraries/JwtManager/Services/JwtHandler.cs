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

namespace JwtManager.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _settings;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
        private SecurityKey _issuerSigningKey;
        private SigningCredentials _signingCredentials;
        private JwtHeader _jwtHeader;
        public TokenValidationParameters Parameters { get; private set; }

        public JwtHandler(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;

            if (_settings.UseRsa)
                InitializeRsa();
            else
                InitializeHmac();

            InitializeJwtParameters();
        }

        private void InitializeRsa()
        {
            var publicRsa = RSA.Create();
            var publicKeyXml = File.ReadAllText(_settings.RsaPublicKeyXml);
            publicRsa.FromXmlString(publicKeyXml);
            _issuerSigningKey = new RsaSecurityKey(publicRsa);


            if (string.IsNullOrWhiteSpace(_settings.RsaPrivateKeyXml))
                return;

            var privateRsa = RSA.Create();
            var privateKeyXml = File.ReadAllText(_settings.RsaPrivateKeyXml);
            privateRsa.FromXmlString(privateKeyXml);
            var privateKey = new RsaSecurityKey(privateRsa);
            _signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256) { CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false } };

        }

        private void InitializeHmac()
        {
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.HmacSecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        private void InitializeJwtParameters()
        {
            _jwtHeader = new JwtHeader(_signingCredentials);
            Parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _settings.Issuer,
                IssuerSigningKey = _issuerSigningKey
            };
        }

        public Jwt Create(List<Claim> claims)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddDays(_settings.ExpiryDays);
            var centuryBegin = new DateTime(1970, 1, 1);
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var issuer = _settings.Issuer ?? string.Empty;

            var payload = new JwtPayload
            {
                {"iss", issuer},
                {"iat", now},
                {"nbf", now},
                {"exp", exp},
                {"jti", Guid.NewGuid().ToString("N")}
            };

            payload.AddClaims(claims);

            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new Jwt
            {
                Token = token,
                Expires = exp
            };
        }
    }
}
