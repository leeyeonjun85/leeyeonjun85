using WpfControls.ViewModels;
using WpfControls.Views;

namespace WpfControls
{
  public class InputBoxEx
  {
    public static string? Show(string title, string prompt, string defaultInputMessage = "")
    {
      InputBoxViewModel viewModel = new InputBoxViewModel(title, prompt, defaultInputMessage);
      InputBoxView view = new InputBoxView
      {
        DataContext = viewModel
      };

      return view.ShowDialog() ?? false
        ? viewModel.Response
        : null;
    }
  }
}
