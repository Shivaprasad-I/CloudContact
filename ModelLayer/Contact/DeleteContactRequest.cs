﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Contact
{
    public class DeleteContactRequest
    {
        public int Id { get; set; }
        public int UserId { get;set; }
        public bool IsSoftDelete { get; set; }
    }
}
