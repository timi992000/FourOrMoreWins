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
			SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
			Closing += __Closing;
			_ViewModel = new MainWindowViewModel(this);
			DataContext = _ViewModel;
			_Startup = new StartupDialog(_ViewModel);
			_Startup.StartRequested += __StartRequested;
			_Startup.ShowWindow();
			Hide();
		}

		private void __StartRequested(object? sender, EventArgs e)
		{
			Show();
		}

		private void __Closing(object? sender, CancelEventArgs e)
		{
			e.Cancel = true;
			Hide();
			_Startup.ShowWindow();
		}
	}
}
