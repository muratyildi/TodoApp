using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class ProjectUpdateModel
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
    
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectUpdateModel,Entities.TodoProject>();
        }
    }
}
