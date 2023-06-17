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
    public event EventHandler StartRequested;
    public StartupDialog(MainWindowViewModel viewModel)
    {
      _ViewModel = viewModel;
      __Init();
    }

    private void __Init()
    {
      Closing += __OnClosing;
      //ResizeMode = ResizeMode.NoResize;
      WindowStartupLocation = WindowStartupLocation.CenterScreen;
      _View = new StartupDialogView();
      _View.DataContext = _ViewModel;
      _View.StartGameRequested += __StartGameRequested;
      Title = "4 OR MORE Startwindow";
      Content = _View;
    }

    private void __StartGameRequested(object? sender, EventArgs e)
    {
      if (sender != null && sender is StartupDialogView StartupView)
      {
        _ViewModel.DrawGameField();
        StartRequested?.Invoke(this, e);
        Hide();
      }
      else
        MessageBox.Show("Cant start new game, need a minimum of two players", "Cant start a game",
            MessageBoxButton.OK, MessageBoxImage.Information);
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
