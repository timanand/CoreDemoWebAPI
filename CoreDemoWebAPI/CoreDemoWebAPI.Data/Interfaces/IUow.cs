﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDemoWebAPI.Data
{
    public interface IUow
    {
        IStaffRepository StaffRepository { get; }
    }


}
