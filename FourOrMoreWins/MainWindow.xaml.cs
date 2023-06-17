using FourOrMoreWins.Client.Dialogs;
using FourOrMoreWins.Client.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.ComponentModel;

namespace FourOrMoreWins
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		private MainWindowViewModel _ViewModel;
		private StartupDialog _Startup;
		public MainWindow()
		{
			InitializeComponent();
			_ViewModel = new MainWindowViewModel();
			DataContext = _ViewModel;
			__AfterInit();
		}

		private void __AfterInit()
		{
			Closing += __Closing;
		}

		private void __Closing(object? sender, CancelEventArgs e)
		{
			e.Cancel = true;
			Hide();
			_Startup.ShowWindow();
		}
	}
}
