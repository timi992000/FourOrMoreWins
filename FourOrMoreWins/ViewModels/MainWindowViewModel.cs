using FourOrMoreWins.Core.BaseClasses;
using FourOrMoreWins.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FourOrMoreWins.Client.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private Player[] _Players = new Player[]
    {
    new Player{ PlayerBrush = Brushes.Yellow },
    new Player{ PlayerBrush = Brushes.Red },
    };
    private Queue<Player> _TurnQueue;
    private List<GameCell> _GameCells;
    public MainWindowViewModel()
    {
      NeedsToWinCount = 4;
      RowCount = 6;
      ColumnCount = 8;
    }

    public Player CurrentPlayer
    {
      get => Get<Player>();
      set => Set(value);
    }

    public int NeedsToWinCount
    {
      get => Get<int>();
      set => Set(value);
    }

    public int RowCount
    {
      get => Get<int>();
      set => Set(value);
    }

    public int ColumnCount
    {
      get => Get<int>();
      set => Set(value);
    }

    public object GameField
    {
      get => Get<object>();
      set => Set(value);
    }

    public void DrawGameField()
    {
      try
      {
        __DoDraw();
        __Enqueue();
      }
      catch (Exception ex)
      {
        ShowErrorMessage(ex);
      }
    }

    private void __Enqueue()
    {
      _TurnQueue = new Queue<Player>();
      _Players.ToList().ForEach(x => _TurnQueue.Enqueue(x));
      __NextPlayer();
    }

    private void __NextPlayer()
    {
      CurrentPlayer = _TurnQueue.Dequeue();
      _TurnQueue.Enqueue(CurrentPlayer);
      OnPropertyChanged(nameof(Player.PlayerBrush));
    }

    private void __DoDraw()
    {
      _GameCells = new List<GameCell>();
      int size = 50;
      int margin = 10;

      var grid = new Grid
      {
        Background = Brushes.Blue
      };

      for (int rowCounter = 0; rowCounter < RowCount; rowCounter++)
      {
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        for (int columnCounter = 0; columnCounter < ColumnCount; columnCounter++)
        {
          grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
          GameCell cell = __GetNewGameCell(size, margin, rowCounter, columnCounter);
          Grid.SetColumn(cell.Element, columnCounter);
          Grid.SetRow(cell.Element, rowCounter);
          grid.Children.Add(cell.Element);
        }
      }
      GameField = grid;
    }

    private GameCell __GetNewGameCell(int size, int margin, int rowCounter, int columnCounter)
    {
      var cell = new GameCell(rowCounter, columnCounter, size, margin);
      cell.Clicked += __CellClicked;
      _GameCells.Add(cell);
      return cell;
    }

    private void __CellClicked(object? sender, EventArgs e)
    {
      if (sender is GameCell cell && !cell.Locked)
      {
        cell = __GetBottomCellToSet(cell);
        if (cell == null)
          return;
        cell.SetPlayer(CurrentPlayer);
        __NextPlayer();
      }
    }

    private GameCell __GetBottomCellToSet(GameCell cell)
    {
      //Get all gamecells from clicked column which are not locked
      var gameCellsFromColumn = _GameCells.Where(c => c.Column == cell.Column && !c.Locked).ToList();

      //All gamecells have player assignment - do nothing on click
      if (!gameCellsFromColumn.Any())
        return null;

      return gameCellsFromColumn.OrderByDescending(c => c.Row).FirstOrDefault();
    }
  }
}