using FourOrMoreWins.Core.BaseClasses;
using FourOrMoreWins.Core.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FourOrMoreWins.Client.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    public MainWindowViewModel()
    {
      NeedsToWinCount = 4;
      RowCount = 6;
      ColumnCount = 8;
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
      }
      catch (Exception ex)
      {
        ShowErrorMessage(ex);
      }
    }

    private void __DoDraw()
    {

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
          var cell = new GameCell(rowCounter, columnCounter, size, margin);
          Grid.SetColumn(cell.Element, columnCounter);
          Grid.SetRow(cell.Element, rowCounter);
          grid.Children.Add(cell.Element);
        }
      }
      GameField = grid;
    }
  }
}