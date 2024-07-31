using AutoMapper;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class AccountCreateModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Username { get; set; }
    }

    public class AccountCreateModelProfile : Profile
    {
        public AccountCreateModelProfile()
        {
            CreateMap<AccountCreateModel, Account>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
                {
                    UserName = src.Username,
                }));
        }
    }
}
