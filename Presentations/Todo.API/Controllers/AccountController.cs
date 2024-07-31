using AutoMapper;
using Data.EntityFramework;
using Entities;
using Infrastructure.Web.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models;

namespace Todo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public AccountController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ApiResult> Post(AccountCreateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new ApiResult(ApiStatusDefaults.ValidationError, "Lütfen tüm bilgileri eksiksiz giriniz.");

                var account = _mapper.Map<Account>(model);

                var emailIsExist = await _dataContext.Accounts.AnyAsync(x => x.Email == model.Email);
                if (emailIsExist)
                    return new ApiResult("email-exist", "Girdiğiniz e-posta zaten kullanılmaktadır.");

                await _dataContext.Accounts.AddAsync(account);
                await _dataContext.SaveChangesAsync();


                return new ApiResult(ApiStatusDefaults.Success, "", account.Id);
            }
            catch (Exception e)
            {
                return new ApiResult(ApiStatusDefaults.Error, e.ToString());
            }
        }
    }
}
