using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBase;

namespace DataTemplateBasic
{
  public class MainViewModel : ViewModelBase
  {
    private List<Student> students = default!;

    public MainViewModel()
    {
      Students = Student.Students;
      CurrentViewModel = new TestViewModel();
    }

    public List<Student> Students { get => students; set { students = value; OnPropertyChanged(); } }
    public ViewModelBase CurrentViewModel { get; set; }
  }
}
