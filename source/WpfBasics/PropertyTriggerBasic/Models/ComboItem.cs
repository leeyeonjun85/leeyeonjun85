using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PropertyTriggerBasic.Models
{
  public class ComboItem
  {
    public string Value { get; set; } = default!;
    public Brush Brush { get; set; } = default!;

    public override string ToString()
    {
      return $"Value: {Value}, Brush: {Brush}";
    }

    public static List<ComboItem> Items => new()
    {
      new ComboItem { Value = "빨간색", Brush = Brushes.Red },
      new ComboItem { Value = "파란색", Brush = Brushes.Blue },
      new ComboItem { Value = "초록색", Brush = Brushes.Green },
    };
  }
}
