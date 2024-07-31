using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.EntityFramework;
using Entities;
using Infrastructure.Web.Constants;
using Infrastructure.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models;
using Todo.API.Models.Dtos;
using X.PagedList.Extensions;

namespace Todo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TaskController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ApiResult> Get(int page = 1, int size = 10)
        {
            try
            {
                var accountId = Convert.ToInt64(User.GetClaimValue(ClaimDefaults.AccountId));
                var account = await _dataContext.Accounts.Include(x => x.User).Include(x => x.User.TodoProjectUserMap).SingleOrDefaultAsync(x => x.Id == accountId);

                var query =  _dataContext.TodoProjectItems
                    .Where(x=>x.TodoProject.UserId==accountId)
                    .AsQueryable();


                var tasks =  query.ProjectTo<GetTasksDto.TaskModel>(_mapper.ConfigurationProvider).ToPagedList(page, size);

                var dto = new GetTasksDto
                {
                    Tasks = tasks.ToList(),
                    PageNumber = tasks.PageNumber,
                    PageSize = tasks.PageSize,
                    TotalItemCount = tasks.TotalItemCount,
                    TotalPageCount = tasks.PageCount
                };

                return new ApiResult(ApiStatusDefaults.Success, null, dto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ApiResult> Post(TaskCreateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new ApiResult(ApiStatusDefaults.ValidationError, "Tüm alanları giriniz");

                var task = _mapper.Map<TodoProjectItem>(model);

                await _dataContext.TodoProjectItems.AddAsync(task);
                await _dataContext.SaveChangesAsync();

                return new ApiResult(ApiStatusDefaults.Success, "ekleme başarılı",task);
            }
            catch (Exception e)
            {

                return new ApiResult(ApiStatusDefaults.Error, e.ToString());
            }
        }
    }
}
