using AutoMapper;
using Data.EntityFramework;
using Entities;
using Infrastructure.Web.Constants;
using Infrastructure.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Todo.API.Models;

namespace Todo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IInviteCodeGenerator _inviteCodeGenerator;

        public ProjectController(DataContext dataContext,IMapper mapper, IInviteCodeGenerator inviteCodeGenerator)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _inviteCodeGenerator = inviteCodeGenerator;
        }

        [HttpPost]
        public async Task<ApiResult> Post(ProjectCreateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new ApiResult(ApiStatusDefaults.ValidationError, "tüm alanları giriniz");

                var accountId = Convert.ToInt64(User.GetClaimValue(ClaimDefaults.AccountId));

                var project = _mapper.Map<TodoProject>(model);
                project.UserId= accountId;

                await _dataContext.TodoProjects.AddAsync(project);
                await _dataContext.SaveChangesAsync();

                var projectUserMaps = _mapper.Map<List<TodoProjectUserMap>>(model.UserIds);
                foreach (var projectUserMap in projectUserMaps)
                {
                    projectUserMap.TodoProjectId = project.Id;
                }

                await _dataContext.TodoProjectUserMaps.AddRangeAsync(projectUserMaps);
                await _dataContext.SaveChangesAsync();

                return new ApiResult(ApiStatusDefaults.Success);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("CreateAndSendInviteLink")]
        public async Task<ApiResult> CreateAndSendInviteLink(CreateAndSendInviteLinkModel model)
        {
            try
            {
                var accountId = Convert.ToInt64(User.GetClaimValue(ClaimDefaults.AccountId));

                #region Generate Invite Code
                var code= _inviteCodeGenerator.GenerateCode();

                return new ApiResult(ApiStatusDefaults.Success, code);
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
