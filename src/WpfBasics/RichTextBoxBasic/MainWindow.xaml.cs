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
using RichTextBoxBasic.Utils;

namespace RichTextBoxBasic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //// TextRange를 이용한 방법
            //TextRange textRange = new TextRange(richTextBox1.Document.ContentStart, richTextBox1.Document.ContentEnd);
            //textRange.Text = "안녕하세요. 이연준입니다.";

            //// TextRange를 이용하여 속성 추가하기
            //textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            //textRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.HotPink));
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            //// TextRange를 이용하여 속성 초기화하기
            //TextRange textRange = richTextBox1.Selection;
            //var fontWeightProperty = textRange.GetPropertyValue(TextElement.FontWeightProperty);
            //if (fontWeightProperty is FontWeight fontWeight && fontWeight == FontWeights.Bold)
            //{
            //    textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            //}

            //var foregroundProperty = textRange.GetPropertyValue(TextElement.ForegroundProperty);
            //if (foregroundProperty is SolidColorBrush colorBrush && colorBrush.Color == Colors.HotPink)
            //{
            //    textRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
            //}

            //// Paragraph를 이용한 방법
            //Paragraph paragraph = new Paragraph();
            //Run run1 = new Run("안녕하세요.");
            //Run run2 = new Run("이연준") { Foreground = Brushes.HotPink, FontWeight = FontWeights.Bold };
            //Run run3 = new Run("입니다.");
            //paragraph.Inlines.Add(run1);
            //paragraph.Inlines.Add(run2);
            //paragraph.Inlines.Add(run3);

            //FlowDocument flowDocument = new FlowDocument();
            //flowDocument.Blocks.Add(paragraph);

            //richTextBox1.Document = flowDocument;
        }
        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionBold();
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionItalic();
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionUnderline();
        }

        private void Strikethrough_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionStrikethrough();
        }

        private void FontSize_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionFontSize(30);
        }

        private void FontFamily_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionFontFamily(new FontFamily("돋움"));
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionColor(Color.FromArgb(255, 255, 0, 0));
        }

        private void RtfExport_Click(object sender, RoutedEventArgs e)
        {
            richTextBox2.SetText(richTextBox1.GetRtf());
        }

        private void RtfImport_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetRtf(richTextBox2.GetText());
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.SetSelectionImage(((BitmapSource)img.Source).ToBitmapImage());
        }

        private void CaretBrush_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.CaretBrush = Brushes.Red;
        }
    }
}
