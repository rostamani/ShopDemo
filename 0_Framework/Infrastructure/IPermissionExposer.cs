using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Infrastructure
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }

    
}
