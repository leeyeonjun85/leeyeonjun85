#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System.Windows.Media;
using DataBaseTools.ViewModels;
using MaterialDesignThemes.Wpf;

namespace DataBaseTools.Services
{
    public class Utiles
    {
        public AppData InitApp(AppData AppData)
        {
            // Color Theme
            Color primaryColor = Color.FromArgb(255, 0, 31, 158);
            Color secondaryColor = Color.FromArgb(255, 34, 158, 0);

            PaletteHelper paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(primaryColor);
            theme.SetSecondaryColor(secondaryColor);
            paletteHelper.SetTheme(theme);

            return AppData;
        }
    }
}
