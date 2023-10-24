using DataBaseTools.ViewModels;

namespace DataBaseTools.Models
{
    public class OracleTable
    {
        public string OWERNER { get; set; } = string.Empty; // 0
        public string TABLE_NAME { get; set; } = string.Empty; // 1
        public string TABLESPACE_NAME { get; set; } = string.Empty; // 1
        public string CLUSTER_NAME { get; set; } = string.Empty; // 1
        public string IOT_NAME { get; set; } = string.Empty; // 1
        public string STATUS { get; set; } = string.Empty; // 1
        public string PCT_FREE { get; set; } = string.Empty; // 1
        public string PCT_USED { get; set; } = string.Empty; // 1
        public string INI_TRANS { get; set; } = string.Empty; // 1
        public string MAX_TRANS { get; set; } = string.Empty; // 1
        public string INITIAL_EXTENT { get; set; } = string.Empty; // 1
        public string NEXT_EXTENT { get; set; } = string.Empty; // 1
        public decimal StatRowCount { get; set; } // 19
    }
}
