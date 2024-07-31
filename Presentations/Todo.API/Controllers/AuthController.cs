using Data.EntityFramework;
using Infrastructure.Web.Constants;
using Infrastructure.Web.Extensions;
using JwtManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Todo.API.Models;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IJwtHandler _jwtHandler;

        public AuthController(DataContext dataContext, IJwtHandler jwtHandler)
        {
            _dataContext = dataContext;
            _jwtHandler = jwtHandler;
        }
        [HttpPost("login")]
        public async Task<ApiResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new ApiResult(ApiStatusDefaults.ValidationError, "Lütfen tüm bilgileri eksiksiz giriniz.");

                var user = await _dataContext.Users
                    .Include(x => x.Account)
                    .SingleOrDefaultAsync(x => x.Account.Email == model.Email && x.Account.Password == model.Password);

                if (user == null)
                    return new("not-exist-error", "Kullanıcı bulunamadı.");

                #region Token Generate

                var claims = new List<Claim>
                {
                    new(ClaimDefaults.AccountId, user.Account.Id.ToString()),
                    new(ClaimDefaults.Email, user.Account.Email),
                    new(ClaimTypes.Role, "user")
                };

   

                var jwt = _jwtHandler.Create(claims);

                #endregion

                return new(ApiStatusDefaults.Success, null, new
                {
                    user = new
                    {
                        fullName = user.Account.FullName,
                        email = user.Account?.Email,
                        accuntId = user.Account.Id
                    },
                    token = jwt.Token,
                    expires = jwt.Expires.UnixTimeStampToDateTime()
                });

            }
            catch (Exception e)
            {
                return new(ApiStatusDefaults.Error, e.ToString());
            }
        }
    }
}
