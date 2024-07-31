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
        public long AccountId { get; set; }

        public string UserName { get; set; }

        #region Single

        public Account Account { get; set; }

        #endregion

        #region Collection 

        public List<TodoProjectUserMap> TodoProjectUserMap { get; set; }

        public List<Invite> SentInvites { get; set; } // Gönderilen davet kodları

        public List<Invite> ReceivedInvites { get; set; } // Alınan davet kodları

        #endregion
    }
}
