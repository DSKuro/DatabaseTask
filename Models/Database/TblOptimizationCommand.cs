using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblOptimizationCommand
    {
        public string Command { get; set; } = null!;
        public string? Result { get; set; }
    }
}
