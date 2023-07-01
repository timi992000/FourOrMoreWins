using System.Windows.Controls;

namespace FourOrMoreWins.Core.Entities
{
  /// <summary>
  /// Interaction logic for GameCellButton.xaml
  /// </summary>
  public partial class GameCellButton : UserControl
  {
    public GameCellButton()
    {
      InitializeComponent();
    }
    internal Button GetButton()
    {
      return Btn;
    }
  }
}
