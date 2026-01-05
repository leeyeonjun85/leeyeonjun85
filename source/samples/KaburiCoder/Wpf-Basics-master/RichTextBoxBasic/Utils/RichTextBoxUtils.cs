using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RichTextBoxBasic.Utils
{
  public static class RichTextBoxUtils
  {
    #region Private Methods
    private static void SetPropertyValue(RichTextBox richTextBox, DependencyProperty property, object value)
    {
      TextRange selection = richTextBox.Selection;
      if (!selection.IsEmpty)
      {
        selection.ApplyPropertyValue(property, value);
      }
    }

    private static void TogglePropertyValue(RichTextBox richTextBox, DependencyProperty property, object value, object clearValue)
    {
      TextRange selection = richTextBox.Selection;
      if (!selection.IsEmpty)
      {
        object currentValue = selection.GetPropertyValue(property);
        if (currentValue.GetHashCode() == value.GetHashCode())
        {
          selection.ApplyPropertyValue(property, clearValue);
        }
        else
        {
          selection.ApplyPropertyValue(property, value);
        }
      }
    }

    private static void ToggleTextDecorationsPropertyValue(RichTextBox richTextBox, TextDecorationLocation textDecorationLocation, object value)
    {
      TextRange selection = richTextBox.Selection;
      if (!selection.IsEmpty)
      {
        object currentValue = selection.GetPropertyValue(Inline.TextDecorationsProperty);
        if (currentValue is TextDecorationCollection decorations
          && decorations.Any(d => d.Location == textDecorationLocation))
        {
          selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }
        else
        {
          selection.ApplyPropertyValue(Inline.TextDecorationsProperty, value);
        }
      }
    }

    private static void LoadRtfToTextRange(TextRange textRange, string rtf)
    {
      using MemoryStream ms = new(Encoding.UTF8.GetBytes(rtf));
      textRange.Load(ms, DataFormats.Rtf);
    }

    private static string GetRtfByTextRange(TextRange textRange)
    {
      using MemoryStream ms = new();
      textRange.Save(ms, DataFormats.Rtf);
      return Encoding.UTF8.GetString(ms.ToArray());
    }
    #endregion

    #region Public Methods
    public static void SetSelectionBold(this RichTextBox richTextBox)
    {
      TogglePropertyValue(richTextBox, TextElement.FontWeightProperty, FontWeights.Bold, FontWeights.Normal);
    }

    public static void SetSelectionItalic(this RichTextBox richTextBox)
    {
      TogglePropertyValue(richTextBox, TextElement.FontStyleProperty, FontStyles.Italic, FontStyles.Normal);
    }

    public static void SetSelectionUnderline(this RichTextBox richTextBox)
    {
      ToggleTextDecorationsPropertyValue(richTextBox, TextDecorationLocation.Underline, TextDecorations.Underline);
    }

    public static void SetSelectionStrikethrough(this RichTextBox richTextBox)
    {
      ToggleTextDecorationsPropertyValue(richTextBox, TextDecorationLocation.Strikethrough, TextDecorations.Strikethrough);
    }

    public static void SetSelectionFontSize(this RichTextBox richTextBox, double size)
    {
      SetPropertyValue(richTextBox, TextElement.FontSizeProperty, size);
    }

    public static void SetSelectionFontFamily(this RichTextBox richTextBox, FontFamily fontFamily)
    {
      SetPropertyValue(richTextBox, TextElement.FontFamilyProperty, fontFamily);
    }

    public static void SetRtf(this RichTextBox richTextBox, string rtf)
    {
      TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
      LoadRtfToTextRange(textRange, rtf);
    }

    public static string GetRtf(this RichTextBox richTextBox)
    {
      TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
      return GetRtfByTextRange(textRange);
    }

    public static void SetSelectionRtf(this RichTextBox richTextBox, string rtf)
    {
      LoadRtfToTextRange(richTextBox.Selection, rtf);
    }

    public static string GetSelectionRtf(this RichTextBox richTextBox)
    {
      return GetRtfByTextRange(richTextBox.Selection);
    }

    public static void SetText(this RichTextBox richTextBox, string text)
    {
      TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
      textRange.Text = text;
    }

    public static string GetText(this RichTextBox richTextBox)
    {
      TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
      return textRange.Text;
    }

    public static void SetSelectionColor(this RichTextBox richTextBox, Color color)
    {
      SetPropertyValue(richTextBox, TextElement.ForegroundProperty, new SolidColorBrush(color));
    }

    public static void SetSelectionImage(this RichTextBox richTextBox, BitmapImage image)
    {
      if (image.Width > 0 && image.Height > 0)
      {
        InlineUIContainer inline = new InlineUIContainer(new Image { Source = image, Width = image.Width, Height = image.Height });
        TextPointer tp = richTextBox.CaretPosition;
        tp.Paragraph.Inlines.Add(inline);
      }
    }

    public static void SetSelectionImage(this RichTextBox richTextBox, string imagePath)
    {
      if (File.Exists(imagePath))
      {
        BitmapImage image = new BitmapImage(new Uri(imagePath));
        richTextBox.SetSelectionImage(image);
      }
    }
    #endregion
  }
}
