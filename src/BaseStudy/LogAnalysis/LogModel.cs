using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysis
{
    public class LogModel
    {
        public string? Category { get; set; }
        public DateTime DateTime { get; set; }
        public string? Message { get; set; }
    }

}
