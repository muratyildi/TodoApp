using AutoMapper;
using Entities;
using Entities.Enums;

namespace Todo.API.Models
{
    public class TaskCreateModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public TodoStatus Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long ProjectId { get; set; }
    }

    public class TaskCreateModelProfile : Profile
    {
        public TaskCreateModelProfile()
        {
            CreateMap<TaskCreateModel, TodoProjectItem>();
        }
    }
}
