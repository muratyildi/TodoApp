using AutoMapper;
using Entities;
using Entities.Enums;

namespace Todo.API.Models.Dtos
{
    public class GetProjectsDto
    {
        public List<ProjectModel> Projects { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItemCount { get; set; }

        public int TotalPageCount { get; set; }

        public GetProjectsDto()
        {
            Projects = new List<ProjectModel>();
        }
        public class ProjectModel
        {
            public long Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public long UserId { get; set; }

        }
    }

    public class GetProjectsDtoProfile : Profile
    {
        public GetProjectsDtoProfile()
        {
            CreateMap<TodoProject, GetProjectsDto.ProjectModel>();
        }
    }
}
