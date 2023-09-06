using System;
using System.Collections.Generic;
using System.Windows;
using Wpf_Chat.Services;

namespace Wpf_Chat.Views
{
    /// <summary>
    /// SubView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SubView : Window
    {
        public SubView()
        {
            InitializeComponent();

            //SignalRControl signalR = new SignalRControl();
            //lstbxChat.DataContext = signalR;
        }
    }
}
