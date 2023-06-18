using MahApps.Metro.Controls;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FourOrMoreWins.Core.Entities
{
  public class GameCell
  {
    private int row;
    private int column;
    private int size;
    private int margin;
    public GameCell(int Row, int Column, int Size, int Margin)
    {
      row = Row;
      column = Column;
      size = Size;
      margin = Margin;
      __CreateButton();
    }

    private void __CreateButton()
    {
      Element = new GameCellButton();
      var btn = Element.GetButton();
      btn.Content = $"{row} || {column}";
      btn.SetBinding(Button.CommandProperty, "[CellClicked]");
      btn.Margin = new Thickness(10);
      btn.Width = size;
      btn.Height = size;
    }

    public GameCellButton Element
    { get; set; }

  }
}
