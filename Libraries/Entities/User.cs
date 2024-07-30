using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User:EntityBase
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        #region Single

        public Account Account { get; set; }

        #endregion

        #region Collection 

        public List<TodoProjectUserMap> TodoProjectUserMap { get; set; }

        #endregion
    }
}
