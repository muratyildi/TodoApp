using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TodoProject:EntityBase
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long UserId { get; set; }

        #region Collection 

        public List<TodoProjectUserMap> TodoProjectUserMap { get; set; }

        public List<TodoProjectItem> TodoProjectItems { get; set; }

        public List<Invite> Invites { get; set; }

        #endregion
    }
}
