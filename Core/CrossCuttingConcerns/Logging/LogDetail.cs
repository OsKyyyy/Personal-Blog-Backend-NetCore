﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string User { get; set; }
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }

    }
}
