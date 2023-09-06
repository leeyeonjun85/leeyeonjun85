using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBase;

namespace DataTriggerBasic
{
  public class MainViewModel : ViewModelBase
  {
    private string textProperty = "";

    public MainViewModel() { }

    public string TextProperty
    {
      get => textProperty; set
      {
        textProperty = value;
        OnPropertyChanged();
      }
    }
  }
}
