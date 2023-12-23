﻿using System.Windows.Input;

using App1.Contracts.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App1.ViewModels;

public class ShellViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private RelayCommand _goBackCommand;
    private RelayCommand _loadedCommand;
    private RelayCommand _unloadedCommand;

    public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

    public ShellViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    private void OnLoaded()
    {
        _navigationService.Navigated += OnNavigated;
    }

    private void OnUnloaded()
    {
        _navigationService.Navigated -= OnNavigated;
    }

    private bool CanGoBack()
        => _navigationService.CanGoBack;

    private void OnGoBack()
        => _navigationService.GoBack();

    private void OnNavigated(object sender, string viewModelName)
        => GoBackCommand.NotifyCanExecuteChanged();
}
