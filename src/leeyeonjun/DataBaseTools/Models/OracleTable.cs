using DataBaseTools.ViewModels;

namespace DataBaseTools.Models
{
    public class OracleTable
    {
        public string UserName { get; set; } = string.Empty; // 0
        public string TableName { get; set; } = string.Empty; // 1
        public decimal StatRowCount { get; set; } // 19
    }
}
