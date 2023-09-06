using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfOxyPlotGraph.ViewModels;
using WpfOxyPlotGraph.Views;

namespace WpfOxyPlotGraph
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      var mainView = new MainView
      {
        DataContext = new MainViewModel()
      };

      mainView.Show();
    }
  }
}
