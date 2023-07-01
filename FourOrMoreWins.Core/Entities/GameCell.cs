using MahApps.Metro.Controls;
using MahApps.Metro.Converters;
using System;
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
    public event EventHandler Clicked;
    public GameCell(int Row, int Column, int Size, int Margin)
    {
      row = Row;
      column = Column;
      size = Size;
      margin = Margin;
      __CreateButton();
    }
    public GameCellButton Element
    { get; set; }

    public void SetPlayer(Player player)
    {
      var btn = Element.GetButton();
      var parent = btn.TryFindParent<UserControl>();
      parent.Resources["GameCellBackground"] = player.PlayerBrush;
    }

    private void __CreateButton()
    {
      Element = new GameCellButton();
      var btn = Element.GetButton();
      btn.Margin = new Thickness(10);
      btn.Width = size;
      btn.Height = size;
      btn.Click += (sender, e) => { Clicked?.Invoke(this, e); };
    }


  }
}
