﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using DataBaseTools.Models;
using DataBaseTools.ViewModels;
using DataBaseTools.Views;

namespace DataBaseTools.Services
{
    public class ViewService : IViewService
    {
        public void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(TViewModel))!;
            Window view = (Window)Ioc.Default.GetService(typeof(TView))!;

            viewModel.SetWindow(view);

            if (parameter != null && viewModel is IParameterReceiver parameterReceiver)
            {
                parameterReceiver.ReceiveParameter(parameter);
            }

            view.DataContext = viewModel;
            view.Show();
        }

        private bool ActivateView<TView>() where TView : Window
        {
            IEnumerable<Window> windows = Application.Current.Windows.OfType<TView>();
            if (windows.Any())
            {
                windows.ElementAt(0).Activate();
                return true;
            }
            return false;
        }

        public void ShowMainView()
        {
            ShowView<MainView, MainViewModel>();
        }

        public void ShowSubView(SubData subData)
        {
            if (!ActivateView<SubView>())
            {
                ShowView<SubView, SubViewModel>(subData);
            }
        }

        public void ShowMongoDbView()
        {
            ShowView<MongoDbView, MongoDbViewModel>();
        }

        public void ShowFireBaseView()
        {
            ShowView<FireBaseView, FireBaseViewModel>();
        }

        public void ShowSeojungriOracleView()
        {
            ShowView<SeojungriOracleView, SeojungriOracleViewModel>();
        }

        public void ShowSQLiteView()
        {
            ShowView<SQLiteView, SQLiteViewModel>();
        }
    }
}
