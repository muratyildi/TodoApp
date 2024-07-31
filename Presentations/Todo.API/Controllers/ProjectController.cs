using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.EntityFramework;
using Entities;
using Infrastructure.Web.Constants;
using Infrastructure.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Abstract;
using Todo.API.Models;
using Todo.API.Models.Dtos;
using X.PagedList.Extensions;

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
            catch (Exception e)
            {
                return new ApiResult(ApiStatusDefaults.ValidationError, e.ToString());
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
            catch (Exception e)
            {

                return new ApiResult(ApiStatusDefaults.ValidationError, e.ToString());
            }
        }

        [HttpGet]
        public async Task<ApiResult> Get(int page = 1, int size = 10)
        {
            try
            {
                var accountId = Convert.ToInt64(User.GetClaimValue(ClaimDefaults.AccountId));
                var account = await _dataContext.Accounts.Include(x => x.User).Include(x => x.User.TodoProjectUserMap).SingleOrDefaultAsync(x => x.Id == accountId);

                var query = _dataContext.TodoProjects
                    .Where(x => x.UserId == accountId)
                    .AsQueryable();


                var project = query.ProjectTo<GetProjectsDto.ProjectModel>(_mapper.ConfigurationProvider).ToPagedList(page, size);

                var dto = new GetProjectsDto
                {
                    Projects = project.ToList(),
                    PageNumber = project.PageNumber,
                    PageSize = project.PageSize,
                    TotalItemCount = project.TotalItemCount,
                    TotalPageCount = project.PageCount
                };

                return new ApiResult(ApiStatusDefaults.Success, null, dto);
            }
            catch (Exception e)
            {

                return new ApiResult(ApiStatusDefaults.ValidationError, e.ToString());
            }
        }
    }
}
