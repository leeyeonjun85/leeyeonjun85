using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfOxyPlotGraph.Models
{
  public class TestScore
  {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int KorScore { get; set; }
    public int MathScore { get; set; }
    public int EngScore { get; set; }
    public int StudentId { get; set; }
  }
}
