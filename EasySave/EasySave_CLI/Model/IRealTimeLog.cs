﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
{
    internal interface IRealTimeLog : ILog
    {
        bool State { get; set; }
        public int _totalFilesSize { get; set; }

    }
}
