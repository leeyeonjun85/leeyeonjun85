using DataBaseTools.ViewModels;

namespace DataBaseTools.Models
{
    public class SubData
    {
        public string StringData { get; set; } = string.Empty;
        public int IntData { get; set; }
        public AppData AppData { get; set; } = new();
    }
}
