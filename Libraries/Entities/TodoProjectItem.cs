using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TodoProjectItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TodoStatus Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        #region Single

        public long ProjectId { get; set; }

        public TodoProject TodoProject { get; set; }

        #endregion
    }
}
