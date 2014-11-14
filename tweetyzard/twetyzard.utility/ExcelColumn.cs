using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tweetyzard.utility
{
    public class ExcelColumn
    {
        public string ColumnName { get; set; }
        public string ExcelColumnName { get; set; }
        public uint Index { get; set; }
        public double Width { get; set; }
    }
}
