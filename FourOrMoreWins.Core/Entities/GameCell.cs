using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FourOrMoreWins.Core.Entities
{
  public class GameCell
  {
    public bool Locked { get; private set; }
    public int Row { get; private set; }
    public int Column { get; private set; }
    private int _Size;
    private int _Margin;
    public event EventHandler Clicked;
    public GameCell(int Row, int Column, int Size, int Margin)
    {
      this.Row = Row;
      this.Column = Column;
      _Size = Size;
      _Margin = Margin;
      __CreateButton();
    }
    public GameCellButton Element
    { get; set; }

    public void SetPlayer(Player player)
    {
      var btn = Element.GetButton();
      var parent = btn.TryFindParent<UserControl>();
      parent.Resources["GameCellBackground"] = player.PlayerBrush;
      Locked = true;
    }

    private void __CreateButton()
    {
      Element = new GameCellButton();
      var btn = Element.GetButton();
      btn.Margin = new Thickness(10);
      btn.Width = _Size;
      btn.Height = _Size;
      btn.Click += (sender, e) => { Clicked?.Invoke(this, e); };
    }


  }
}
