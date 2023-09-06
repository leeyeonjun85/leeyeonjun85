using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AttachedPropertyBasic.Utils
{
  public class PasswordBoxHelper
  {
    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var passwordBox = d as PasswordBox;
      if (passwordBox == null) return;

      passwordBox.PasswordChanged -= PasswordChanged;

      string newString = (string)e.NewValue ?? string.Empty;
      if (newString != passwordBox.Password)
      {
        passwordBox.Password = newString;
      }

      passwordBox.PasswordChanged += PasswordChanged;
    }

    public static readonly DependencyProperty BoundPasswordProperty =
      DependencyProperty.RegisterAttached("BoundPassword", typeof(string),
        typeof(PasswordBoxHelper), new PropertyMetadata("<Default>", OnBoundPasswordChanged));

    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
      var passwordBox = (PasswordBox)sender;
      SetBoundPassword(passwordBox, passwordBox.Password);
    }

    public static string GetBoundPassword(DependencyObject obj)
    {
      return (string)obj.GetValue(BoundPasswordProperty);
    }

    public static void SetBoundPassword(DependencyObject obj, string value)
    {
      obj.SetValue(BoundPasswordProperty, value);
    }




  }
}
