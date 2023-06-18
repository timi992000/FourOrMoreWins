using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
