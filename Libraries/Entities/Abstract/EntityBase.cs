using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    public class EntityBase
    {
        public DateTime DateTime { get; set; }

        public EntityBase()
        {
            DateTime = DateTime.UtcNow;
        }
    }
}
