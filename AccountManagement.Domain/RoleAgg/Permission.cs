﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public long Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }

        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        protected Permission()
        {
            
        }

        public Permission(int code)
        {
            Code = code;
        }

        public Permission(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
