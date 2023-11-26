#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace MyUtiles.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        public WindowMainViewModel()
        { }

        [RelayCommand]
        private void BtnOkClick(object sender)
        {

            try
            {
                StreamWriter stremWriter = new ("C:\\yeonjunsCode\\EdcoreWorks\\1Working\\Vision\\db\\temp2.txt");


                string text = string.Empty;

                string[] lines = File.ReadAllLines("C:\\yeonjunsCode\\EdcoreWorks\\1Working\\Vision\\db\\temp1.txt");


                foreach (var line in lines)
                {

                    int idx1 = line.IndexOf("\t\"") + 2;
                    int idx2 = line[idx1..].IndexOf("\"") + 2;

                    if (idx1 is not 1)
                    {
                        string reline = Regex.Replace(line, line[idx1..idx2], line[idx1..idx2].ToLower());
                        text += Environment.NewLine + reline;
                    }
                    else
                    {
                        text += Environment.NewLine + line;
                    }
                }

                stremWriter.Write(text[2..]);
                stremWriter.Close();
            }
            catch (System.Exception)
            {
            } 
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.logger.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

            App.logger.LogInformation("프로그램이 종료되었습니다.");

        }
    }
}
