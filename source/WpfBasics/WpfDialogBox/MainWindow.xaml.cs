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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControls;

namespace WpfDialogBox
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnOkCancel_Click(object sender, RoutedEventArgs e)
    {
      var okCancelDialog = new OkCancelDialog(title: "This is Title", message: "Test Message");
      bool? result = okCancelDialog.ShowDialog();
      MessageBox.Show(result.ToString());
    }

    private void btnMessageBox_Click(object sender, RoutedEventArgs e)
    {
      var result = MessageBoxEx.Show("종료", "입력중인 데이터가 있습니다.\n어떻게 할까요?", "저장 후 종료", "취소");
      switch (result)
      {
        case MessageBoxExResult.Button1:
          MessageBox.Show("저장 후 종료 선택됨");
          break;
        case MessageBoxExResult.Button2:
          MessageBox.Show("저장 안하고 종료 선택됨");
          break;
        case MessageBoxExResult.Button3:
          MessageBox.Show("취소 선택됨");
          break;
      }      
    }

    private void btnInputBox_Click(object sender, RoutedEventArgs e)
    {
      string? result = InputBoxEx.Show("아무말 메세지", "아무말이나 적어보세요 ^^");
      if (result == null)
      {
        MessageBox.Show("취소됨");
      }
      else
      {
        MessageBox.Show(result);
      }
    }
  }
}
