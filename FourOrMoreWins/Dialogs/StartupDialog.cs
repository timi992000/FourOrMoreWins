using FourOrMoreWins.Client.ViewModels;
using FourOrMoreWins.Client.Views;
using MahApps.Metro.Controls;
using System;
using System.ComponentModel;
using System.Windows;

namespace FourOrMoreWins.Client.Dialogs
{
  public class StartupDialog : MetroWindow
  {
    private StartupDialogView _View;
    private MainWindowViewModel _ViewModel;
    public event EventHandler<bool> StartRequested;
    public StartupDialog(MainWindowViewModel viewModel)
    {
      _ViewModel = viewModel;
      __Init();
      SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
    }

    private void __Init()
    {
      Closing += __OnClosing;
      WindowStartupLocation = WindowStartupLocation.CenterScreen;
      _View = new StartupDialogView();
      _View.DataContext = _ViewModel;
      _View.StartGameRequested += __StartGameRequested;
      Title = "4 OR MORE Settings";
      Content = _View;
    }

    private void __StartGameRequested(object? sender, bool againstComputer)
    {
      if (sender != null && sender is StartupDialogView StartupView)
      {
				_ViewModel.AgainstComputer = againstComputer;
				_ViewModel.Execute_DrawGameField();
        StartRequested?.Invoke(this, againstComputer);
        Hide();
      }
    }

    private void __OnClosing(object? sender, CancelEventArgs e)
    {
      Application.Current.Shutdown();
    }
    public void ShowWindow()
    {
      Show();
    }

  }
}
