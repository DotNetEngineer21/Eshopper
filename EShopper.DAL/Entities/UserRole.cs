﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DAL.Entities
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
