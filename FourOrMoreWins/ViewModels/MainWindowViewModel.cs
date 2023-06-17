using FourOrMoreWins.Core.BaseClasses;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
      int verticalGAP = 10;
      int horizontalGAP = 10;
      int CircleDiameter = 50;
      var CircleBrush = Brushes.White;

      var canvas = new Canvas
      {
        Background = Brushes.Blue
      };

      var neededHeight = RowCount * (CircleDiameter + verticalGAP) - verticalGAP;
      var neededWidth = ColumnCount * (CircleDiameter + horizontalGAP) - horizontalGAP;
      canvas.Height = neededHeight;
      canvas.Width = neededWidth;

      for (int rowCounter = 0; rowCounter < RowCount; rowCounter++)
      {
        for (int columnCounter = 0; columnCounter < ColumnCount; columnCounter++)
        {
          var ellipse = new Ellipse
          {
            Width = CircleDiameter,
            Height = CircleDiameter,
            Fill = CircleBrush
          };
          Canvas.SetLeft(ellipse, columnCounter * CircleDiameter + (verticalGAP * columnCounter));
          Canvas.SetTop(ellipse, rowCounter * CircleDiameter + (horizontalGAP * rowCounter));
          canvas.Children.Add(ellipse);
        }
      }


      GameField = canvas;
    }
  }
}
