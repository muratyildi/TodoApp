using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Invite : EntityBase
    {

        public long Id { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

        #region Single
        public long FromUserId { get; set; }
        public User FromUser { get; set; }

        public long ToUserId { get; set; }
        public User ToUser { get; set; }

        public long ProjectId { get; set; }
        public TodoProject Project { get; set; }
        #endregion
    }
}
