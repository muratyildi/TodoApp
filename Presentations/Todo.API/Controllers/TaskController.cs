using AutoMapper;
using Data.EntityFramework;
using Entities;
using Infrastructure.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using Todo.API.Models;

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
