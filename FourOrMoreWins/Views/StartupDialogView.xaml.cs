using System;
using System.Windows;
using System.Windows.Controls;

namespace FourOrMoreWins.Client.Views
{
  /// <summary>
  /// Interaction logic for StartupDialogView.xaml
  /// </summary>
  public partial class StartupDialogView : UserControl
  {
    public event EventHandler<bool> StartGameRequested;
    public StartupDialogView()
    {
      InitializeComponent();
    }

    private void __StartPlayerVSPlayer(object sender, RoutedEventArgs e)
    {
      StartGameRequested?.Invoke(this, false);
    }

		private void __StartPlayerVSComputer(object sender, RoutedEventArgs e)
		{
			StartGameRequested?.Invoke(this, true);
		}

	}
}
