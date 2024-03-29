﻿using System.Globalization;
using System.Threading;
using ShowMeTheXAML;

namespace MaterialDesign3Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal static string? StartupItem { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        if (e.Args.Length > 0)
        {
            StartupItem = e.Args[0];
        }

        //This is an alternate way to initialize MaterialDesignInXAML if you don't use the MaterialDesignResourceDictionary in App.xaml
        //Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepPurple];
        //Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
        //ITheme theme = Theme.Create(new MaterialDesignLightTheme(), primaryColor, accentColor);
        //Resources.SetTheme(theme);


        //Illustration of setting culture info fully in WPF:

        //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        Thread.CurrentThread.CurrentCulture = new CultureInfo("ko-KR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ko-KR");

        //Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));


        XamlDisplay.Init();

        // test setup for Persian culture settings
        /*System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-Ir");
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa-Ir");
        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));*/

        base.OnStartup(e);
    }
}
