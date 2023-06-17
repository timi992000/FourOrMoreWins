using FourOrMoreWins.Client.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.ComponentModel;
using System.Security.RightsManagement;
using System.Windows;

namespace FourOrMoreWins.Client.Dialogs
{
	public class StartupDialog : MetroWindow
	{
		private StartupView _View;
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
			Width = 100;
			Height = 200;
			ResizeMode = ResizeMode.NoResize;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			_View = new StartupDialogView();
			_View.DataContext = _ViewModel;
			_View.StartGameRequested += __StartGameRequested;
			Content = _View;
		}

		private void __OnClosing(object? sender, CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}

	}
}
