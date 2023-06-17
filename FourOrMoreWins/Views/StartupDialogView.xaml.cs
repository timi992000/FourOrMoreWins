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
    public event EventHandler StartGameRequested;
    public StartupDialogView()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      StartGameRequested?.Invoke(this, e);
    }

  }
}
