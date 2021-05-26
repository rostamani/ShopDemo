using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Infrastructure
{
    public class NeedsPermissionAttribute:Attribute
    {
        public int Permission { get; set; }

        public NeedsPermissionAttribute(int permission)
        {
            Permission = permission;
        }
    }
}
