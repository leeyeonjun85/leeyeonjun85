using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDialogBox
{
  /// <summary>
  /// InputDialog.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class OkCancelDialog : Window
  {
    public OkCancelDialog(string title, string message)
    {
      InitializeComponent();

      this.Title = title;
      tbMessage.Text = message;
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }
  }
}
