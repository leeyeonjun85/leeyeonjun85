using System.Configuration;
using System.Windows.Media;


namespace MyUtiles.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        public string WindowTitle { get; } = $"이연준의 DB Tool - {ConfigurationManager.AppSettings["Version"]}({ConfigurationManager.AppSettings["LastUpdateDate"]})";
        public Color ColorPrimary { get; } = Colors.MidnightBlue;
        public Color ColorSecondary { get; } = Colors.LimeGreen;

    }
}
