using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class CreateAndSendInviteLinkModel
    {
        [Required]
        public long FromUserId { get; set; }

        [Required]
        public long ToUserId { get; set; }

        [Required]
        public long ProjectId { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
