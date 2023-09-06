using ControlTemplateBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBase;

namespace ControlTemplateBasic
{
  public class MainViewModel : ViewModelBase
  {
    public MainViewModel()
    {
      Items = ComboItem.Items;
    }

    public List<ComboItem> Items { get; set; }
  }
}
