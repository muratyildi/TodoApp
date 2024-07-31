using AutoMapper;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class ProjectCreateModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<long> UserIds { get; set; }
    }

    public class ProjectUpdateModelProfile:Profile
    {
        public ProjectUpdateModelProfile()
        {
            CreateMap<ProjectCreateModel, TodoProject>();

            CreateMap<long, TodoProjectUserMap>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src));
        }
    }
}
