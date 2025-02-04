﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TodoProjectUserMap:EntityBase
    {
        public long UserId { get; set; }

        public User User { get; set; }

        public long TodoProjectId { get; set; }

        public TodoProject TodoProject { get; set; }
    }
}
