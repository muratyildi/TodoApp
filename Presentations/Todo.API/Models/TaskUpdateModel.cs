using AutoMapper;
using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class TaskUpdateModel
    {
        [Required]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TodoStatus Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class TaskUpdateModelProfile : Profile
    {
        public TaskUpdateModelProfile()
        {
            CreateMap<TaskUpdateModel, Entities.TodoProjectItem>();
        }
    }
}
