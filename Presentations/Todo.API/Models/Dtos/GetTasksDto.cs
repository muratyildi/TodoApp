using AutoMapper;
using Entities;
using Entities.Enums;

namespace Todo.API.Models.Dtos
{
    public class GetTasksDto
    {
        public List<TaskModel> Tasks { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItemCount { get; set; }

        public int TotalPageCount { get; set; }

        public GetTasksDto()
        {
            Tasks = new List<TaskModel>();
        }
        public class TaskModel
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public TodoStatus Status { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public long ProjectId { get; set; }

        }
    }
    public class GetTasksDtooProfile : Profile
    {
        public GetTasksDtooProfile()
        {
            CreateMap<TodoProjectItem, GetTasksDto.TaskModel>();
        }
    }
}
