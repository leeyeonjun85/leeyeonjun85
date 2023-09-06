using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBase;

namespace AttachedPropertyBasic.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    private string _inputText = string.Empty;
    private string _outputText = string.Empty;
    private string _inputPassword = string.Empty;
    private string _outputPassword = string.Empty;

    public string InputText
    {
      get => _inputText; set
      {
        _inputText = value;
        OnPropertyChanged();
        OutputText = _inputText;
        InputPassword = _inputText;
      }
    }
    public string OutputText
    {
      get => _outputText; set
      {
        _outputText = value;
        OnPropertyChanged();
      }
    }
    public string InputPassword
    {
      get => _inputPassword; set
      {
        _inputPassword = value;
        OnPropertyChanged();
        OutputPassword = _inputPassword;
      }
    }
    public string OutputPassword
    {
      get => _outputPassword; set
      {
        _outputPassword = value;
        OnPropertyChanged();
      }
    }
  }
}
